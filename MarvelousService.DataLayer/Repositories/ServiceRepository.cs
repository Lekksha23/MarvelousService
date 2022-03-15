using Dapper;
using MarvelousService.DataLayer.Configuration;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using NLog;
using System.Data;

namespace MarvelousService.DataLayer.Repositories
{
    public class ServiceRepository : BaseRepository, IServiceRepository
    {
        private const string _serviceAddProcedure = "dbo.Service_Insert";
        private const string _serviceGetByIdProcedure = "dbo.Service_SelectById";
        private const string _serviseUpdateProcedure = "dbo.Service_Update";
        private const string _serviceSoftDeletedProcedure = "dbo.Service_SoftDeleted";
        private static Logger _logger;

        public ServiceRepository(IOptions<DbConfiguration> options) : base(options)
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public int AddService(Service service)
        {
            _logger.Debug("Подключение к базе данных");
            using IDbConnection connection = ProvideConnection();
            _logger.Debug("Подключение к базе данных произведено");

            var newService = connection.QueryFirstOrDefault<int>(_serviceAddProcedure,
                new
                {
                    Name =  service.Name,
                    OneTimePrice = service.OneTimePrice,
                    Description = service.Description,
                    IsDeleted = service.IsDeleted
                },
                commandType: CommandType.StoredProcedure);
            _logger.Debug("Услуга добавлена в базу данных");
            return newService;
        }

        public Service GetServiceById(int id)
        {
            _logger.Debug("Подключение к базе данных");
            using IDbConnection connection = ProvideConnection();
            _logger.Debug("Подключение к базе данных произведено");

            var listService = connection.QueryFirstOrDefault<Service>(_serviceGetByIdProcedure, 
                new { Id = id }, 
                commandType: CommandType.StoredProcedure);

            _logger.Debug("Услуги по Id получены");
            return listService;
        }

        public Service SoftDeleted(Service service)
        {
            _logger.Debug("Подключение к базе данных");
            using IDbConnection connection = ProvideConnection();
            _logger.Debug("Подключение к базе данных произведено");

            var newService = connection.QueryFirstOrDefault<Service>(_serviceSoftDeletedProcedure,
                new{IsDeleted = service.IsDeleted},
                commandType: CommandType.StoredProcedure);

            _logger.Debug("Услуга сменила статус на удалена в базе данных");

            return newService;
        }

        public int UpdateService(Service service)
        {
            _logger.Debug("Подключение к базе данных");
            using IDbConnection connection = ProvideConnection();
            _logger.Debug("Подключение к базе данных произведено");

            var newService = connection.QueryFirstOrDefault<Service>(_serviseUpdateProcedure,
                new 
                {
                    service.Name,
                    service.OneTimePrice,
                    service.Description,
                },
                commandType: CommandType.StoredProcedure);

            _logger.Debug("Услуга изменена в базе данных");
            return newService.Id;
        }
    }
}
