using QLDT.Dtos.Response;

namespace QLDT.Service
{
    public interface IDashboardSer
    {
        Task<DashboardRes> GetSummaryAsync();
    }
}
