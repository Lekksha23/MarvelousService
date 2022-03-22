using Dapper;
using MarvelousService.DataLayer.Configuration;
using MarvelousService.DataLayer.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Data;

namespace MarvelousService.DataLayer.Repositories
{
    public class ServicePaymentRepository : BaseRepository, IServicePaymentRepository
    {
        private const string _insertProcedure = "dbo.ServicePayment_Insert";
        private const string _selectByServiceToLeadProcedure = "dbo.ServicePayment_SelectByServiceToLead";
        private readonly ILogger<ServicePaymentRepository> _logger;

        public ServicePaymentRepository(IOptions<DbConfiguration> options, ILogger<ServicePaymentRepository> logger) : base(options)
        {
            _logger = logger;
        }

        public async Task<int> AddServicePayment(ServicePayment servicePayment)
        {
            _logger.LogInformation("Подключение к базе данных.");
            using IDbConnection connection = ProvideConnection();
            _logger.LogInformation("Произведено подключение к базе данных.");

            var id = await connection.QueryFirstOrDefaultAsync<int>(
                    _insertProcedure,
                    new
                    {
                        servicePayment.ServiceToLeadId,
                        servicePayment.TransactionId
                    },
                    commandType: CommandType.StoredProcedure
                );
            _logger.LogInformation($"Оплата услуги добавлена в базу данных.");
            return id;
        }

        public async Task<List<ServicePayment>> GetServicePaymentsByServiceToLeadId(int id)
        {
            _logger.LogInformation("Подключение к базе данных.");
            using IDbConnection connection = ProvideConnection();
            _logger.LogInformation("Произведено подключение к базе данных.");

            var servicePayments = await connection
                .QueryAsync<ServicePayment, ServiceToLead, ServicePayment>(
                _selectByServiceToLeadProcedure,
                (servicePayment, serviceToLead) =>
                {
                    servicePayment.ServiceToLeadId = serviceToLead;
                    return servicePayment;
                },
                new { Id = id },
                splitOn: "ServiceToLeadId",
                commandType: CommandType.StoredProcedure);

            _logger.LogInformation($"Информация о подписке/разовом платеже с = {id} была возвращена.");
            return servicePayments.ToList();
        }
    }
}
