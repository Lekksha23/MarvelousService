using Dapper;
using MarvelousService.DataLayer.Configuration;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog;
using System.Data;

namespace MarvelousService.DataLayer.Repositories
{
    public class ServiceRepository : BaseRepository, IServiceRepository
    {
        private const string _serviceAddProcedure = "dbo.Service_Insert";
        private const string _serviceGetByIdProcedure = "dbo.Service_SelectById";
        private const string _serviceUpdateProcedure = "dbo.Service_Update";
        private const string _serviceSoftDeleteProcedure = "dbo.Service_SoftDelete";
        private readonly ILogger<ServiceRepository> _logger;


        public ServiceRepository(IOptions<DbConfiguration> options, ILogger<ServiceRepository> logger) : base(options)
        {
            _logger = logger;
        }


        public int AddService(Service service)
        {
            _logger.LogInformation("Подключение к базе данных");

            using IDbConnection connection = ProvideConnection();

            _logger.LogInformation("Подключение к базе данных произведено");

            var id = connection.QueryFirstOrDefault<int>(_serviceAddProcedure,
                new
                {
                    service.Name,
                    service.OneTimePrice,
                    service.Description
                },
                commandType: CommandType.StoredProcedure);

            _logger.LogInformation("Услуга добавлена в базу данных"+ service.Name);

            return id;  
        }

        public Service GetServiceById(int id)
        {
            _logger.LogInformation("Запрашиваем услугу по id");
            _logger.LogInformation("Подключение к базе данных");

            using IDbConnection connection = ProvideConnection();

            _logger.LogInformation("Подключение к базе данных произведено");

            var service = connection.QueryFirstOrDefault<Service>(_serviceGetByIdProcedure, 
                new { Id = id }, 
                commandType: CommandType.StoredProcedure);

            _logger.LogInformation("Выборка прошла успешно выбрана услуга с id - " + id);

            return service;
        }

        public void SoftDelete(int id, Service service)
        {
            _logger.LogInformation("Подключение к базе данных");

            using IDbConnection connection = ProvideConnection();

            _logger.LogInformation("Подключение к базе данных произведено");

            var newService = connection.QueryFirstOrDefault<Service>(_serviceSoftDeleteProcedure,
                new{IsDeleted = service.IsDeleted},
                commandType: CommandType.StoredProcedure);

            _logger.LogInformation($"Услуга - {service.Name} сменила статус на 'Удалена' в базе данных");

        }

        public void UpdateService(int id, Service service)
        {
            _logger.LogInformation("Подключение к базе данных");

            using IDbConnection connection = ProvideConnection();

            _logger.LogInformation("Подключение к базе данных произведено");

            connection.QueryFirstOrDefault(_serviceUpdateProcedure,
                new 
                {
                    service.Name,
                    service.OneTimePrice,
                    service.Description,
                },
                commandType: CommandType.StoredProcedure);

            _logger.LogInformation($"Услуга- {service.Name}, изменена в базе данных");

        }
    }
}
