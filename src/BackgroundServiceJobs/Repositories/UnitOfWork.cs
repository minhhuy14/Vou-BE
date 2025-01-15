using PaymentService.Data.Contexts;
using PaymentService.Repositories.Interfaces;
using Shared.Repositories;

namespace PaymentService.Repositories;

public class UnitOfWork : GenericUnitOfWork<EventDbContext>, IUnitOfWork
{
    public UnitOfWork(EventDbContext dbContext,
        IQuizSessionRepository quizSessionRepository,
        IFavoriteEventRepository favoriteEventRepository,
        IEventRepository eventRepository,
        INotificationRepository notificationRepository
    ) : base(dbContext)
    {
        QuizSessions = quizSessionRepository;
        FavoriteEvents = favoriteEventRepository;
        Events = eventRepository;
        Notifications = notificationRepository;
    }

    public IQuizSessionRepository QuizSessions { get; set; }
    public IFavoriteEventRepository FavoriteEvents { get; set; }
    public INotificationRepository Notifications { get; set; }
    public IEventRepository Events { get; set; }
}