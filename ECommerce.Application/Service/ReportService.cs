using AutoMapper;
using ECommerce.Application.Interfaces.IRepositories;
using ECommerce.Application.Interfaces.IServices;

namespace ECommerce.Application.Service
{
    public class ReportService(IUnitOfWork unitOfWork, IMapper mapper) : IReportService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;
        //public async Task<double> GetTotalAmount(string? productId, int? month, int? year)
        //{
        //    string sqlQuery = "select sum(";
        //    double totalAmount = await _unitOfWork.ExecuteQueryAsync(sqlQuery);
        //}
    }
}
