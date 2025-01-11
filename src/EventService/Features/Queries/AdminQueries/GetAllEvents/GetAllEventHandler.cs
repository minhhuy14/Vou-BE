using EventService.DTOs;
using EventService.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Response;

namespace EventService.Features.Queries.AdminQueries.GetAllEvents;

public class GetAllEventHandler : IRequestHandler<GetAllEventsQuery, BaseResponse<GetAllEventQueryResponse>>
{
    private readonly ILogger<GetAllEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public GetAllEventHandler(ILogger<GetAllEventHandler> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task<BaseResponse<GetAllEventQueryResponse>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
    {
        const string methodName = $"{nameof(GetAllEventHandler)}.{nameof(Handle)} =>";
        _logger.LogInformation(methodName);
        var response = new BaseResponse<GetAllEventQueryResponse>();

        try
        {
            var events = await
            (
                from event_ in _unitOfWork.Events.GetAll()
                join counterPart in _unitOfWork.CounterParts.GetAll()
                    on event_.CreatedBy equals counterPart.Id
                where !event_.IsDeleted
                select new EventDto
                {
                    Id = event_.Id,
                    Name = event_.Name,
                    Description = event_.Description,
                    ImageUrl = event_.ImageUrl,
                    StartDate = event_.StartDate,
                    EndDate = event_.EndDate,
                    Status = event_.Status,
                    CreatedDate = event_.CreatedDate,
                    CounterPart = new CounterPartDto
                    {
                        Id = counterPart.Id,
                    }
                }
            )
            .AsNoTracking()
            .ToListAsync(cancellationToken);

            var responseData = new GetAllEventQueryResponse { Events = events };
            response.ToSuccessResponse(responseData);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"{methodName} Has error: {e.Message}");
            response.ToInternalErrorResponse();
        }

        return response;
    }
}