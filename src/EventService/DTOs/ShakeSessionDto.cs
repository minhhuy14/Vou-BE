namespace EventService.DTOs;

public class ShakeSessionDto
{
    public VoucherDto? Voucher { get; set; }
    public long Price { get; set; }
    public int WinRate { get; set; }
    public int AverageDiamond { get; set; }
}