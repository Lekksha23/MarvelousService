using Dapper;
using MarvelousService.DataLayer.Configuration;
using MarvelousService.DataLayer.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;

namespace MarvelousService.DataLayer.Repositories
{
    public class ServicePaymentRepository : BaseRepository
    {
        private const string _insertProcedure = "dbo.ServicePayment_Insert";
        private const string _selectByTransactionIdProcedure = "dbo.ServicePayment_SelectByTransactionId";
        private readonly ILogger<ServicePaymentRepository> _logger;

        public ServicePaymentRepository(IOptions<DbConfiguration> options, ILogger<ServicePaymentRepository> logger) : base(options)
        {
            _logger = logger;
        }

        public int AddServicePayment(ServicePayment servicePayment)
        {
            _logger.LogInformation("Подключение к базе данных.");
            using IDbConnection connection = ProvideConnection();
            _logger.LogInformation("Произведено подключение к базе данных.");

            var id = connection.QueryFirstOrDefault<int>(
                    _insertProcedure,
                    new
                    {
                        servicePayment.ServiceToLeadId,
                        servicePayment.TransactionId
                    },
                    commandType: CommandType.StoredProcedure
                );
            _logger.LogDebug($"Оплата услуги добавлена в базу данных.");
            return id;
        }

        public ServicePayment GetServicePaymentByTransactionId(int id)
        {
            _logger.LogDebug("Подключение к базе данных.");
            using IDbConnection connection = ProvideConnection();
            _logger.LogDebug("Произведено подключение к базе данных.");

            var servicePayment = connection
                .Query<ServicePayment, ServiceToLead, ServicePayment>(
                _selectByTransactionIdProcedure,
                (servicePayment, serviceToLead) =>
                {
                    //servicePayment.TransactionId = serviceToLead;
                    return servicePayment;
                },
                new { Id = id },
                splitOn: "Id",
                commandType: CommandType.StoredProcedure).FirstOrDefault();
            _logger.LogDebug($"Информация об услуге с транзакцией = {id} была возвращена.");

            return servicePayment;
        }
    }
}
