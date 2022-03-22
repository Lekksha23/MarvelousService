using Dapper;
using MarvelousService.DataLayer.Configuration;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;

namespace MarvelousService.DataLayer.Repositories
{
    public class ServiceRepository : BaseRepository, IServiceRepository
    {
        private const string _serviceAddProcedure = "dbo.Service_Insert";
        private const string _serviceGetByIdProcedure = "dbo.Service_SelectById";
        private const string _serviceUpdateProcedure = "dbo.Service_Update";
        private const string _serviceSoftDeleteProcedure = "dbo.Service_SoftDelete";
        private const string _serviceGetTrancactionByServiceToLead = "dbo.ServicePayment_SelectByServiseToLeadId";

        private readonly ILogger<ServiceRepository> _logger;

        public ServiceRepository(IOptions<DbConfiguration> options, ILogger<ServiceRepository> logger) : base(options)
        {
            _logger = logger;
        }

        public async Task<int> AddService(Service service)
        {
            _logger.LogInformation("Подключение к базе данных");
            using IDbConnection connection = ProvideConnection();
            _logger.LogInformation("Подключение к базе данных произведено");

            var id = await connection.QueryFirstOrDefaultAsync<int>(_serviceAddProcedure,
                new
                {
                    service.Name,
                    service.Price,
                    service.Description
                },
                commandType: CommandType.StoredProcedure) ;

            _logger.LogInformation($"Услуга {service.Name} добавлена в базу данных");
            return id;  
        }

        public async Task<Service> GetServiceById(int id)
        {
            _logger.LogInformation("Запрашиваем услугу по id");
            _logger.LogInformation("Подключение к базе данных");
            using IDbConnection connection = ProvideConnection();
            _logger.LogInformation("Подключение к базе данных произведено");

            var service = await connection.QueryFirstOrDefaultAsync<Service>(_serviceGetByIdProcedure, 
                new { Id = id }, 
                commandType: CommandType.StoredProcedure);

            _logger.LogInformation("Выборка прошла успешно выбрана услуга с id - " + id);
            return service;
        }

        public async Task<ServicePayment> GetTransactionByServiceToleadId(int id)
        {
            _logger.LogInformation("Запрашиваем транзакция по id");
            _logger.LogInformation("Подключение к базе данных");
            using IDbConnection connection = ProvideConnection();
            _logger.LogInformation("Подключение к базе данных произведено");

            var service = await connection.QueryFirstOrDefaultAsync<ServicePayment>(_serviceGetTrancactionByServiceToLead,
                new { ServiceToLeadId = id },
                commandType: CommandType.StoredProcedure);

            _logger.LogInformation("Выборка прошла успешно выбрана транзакция с id - " + id);

            return service;
        }

        public async Task SoftDelete(int id, Service service)
        {
            _logger.LogInformation("Подключение к базе данных");

            using IDbConnection connection = ProvideConnection();

            _logger.LogInformation("Подключение к базе данных произведено");

            var newService = await connection.QueryFirstOrDefaultAsync<Service>(_serviceSoftDeleteProcedure,
                new{IsDeleted = service.IsDeleted},
                commandType: CommandType.StoredProcedure);

            _logger.LogInformation($"Услуга - {service.Name} сменила статус на 'Удалена' в базе данных");
        }

        public async Task UpdateService(int id, Service service)
        {
            _logger.LogInformation("Подключение к базе данных");
            using IDbConnection connection = ProvideConnection();
            _logger.LogInformation("Подключение к базе данных произведено");

            connection.QueryFirstOrDefault(_serviceUpdateProcedure,
                new 
                {
                    service.Name,
                    service.Price,
                    service.Description,
                },
                commandType: CommandType.StoredProcedure);

            _logger.LogInformation($"Услуга- {service.Name}, изменена в базе данных");
        }
    }
}
