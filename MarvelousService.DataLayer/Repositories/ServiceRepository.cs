using Dapper;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using NLog;
using System.Data;

namespace MarvelousService.DataLayer.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private const string _serviceAddProcedure = "dbo.Service_Insert";
        private const string _serviceGetByIdProcedure = "dbo.Service_SelectById";
        private const string _serviseUpdateProcedure = "dbo.Service_Update";
        private const string _serviceSoftDeletedProcedure = "dbo.Service_SoftDeleted";
        private static Logger _logger;

        public IDbConnection _connection;

        public ServiceRepository(IDbConnection dbConnection)
        {
            _connection = dbConnection;

            _logger = LogManager.GetCurrentClassLogger();
        }

        public IDbConnection Connection => new SqlConnection(_connection.ConnectionString);

        public int AddService(Service service)
        {
            _logger.Debug("Подключение к базе данных");

            using IDbConnection connection = Connection;

            _logger.Debug("Подключение к базе данных произведено");

            var newService = connection.QueryFirstOrDefault<Service>(_serviceAddProcedure,
                new
                {
                    service.Name,
                    service.OneTimePrice,
                    service.Description,
                    service.IsDeleted
                },
                commandType: CommandType.StoredProcedure);
            _logger.Debug("Услуга добавлена в базу данных");

            return newService.Id;
        }

        public Service GetServiceById(int id)
        {
            _logger.Debug("Подключение к базе данных");

            using IDbConnection connection = Connection;

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

            using IDbConnection connection = Connection;

            _logger.Debug("Подключение к базе данных произведено");

            var newService = connection.QueryFirstOrDefault<Service>(_serviceSoftDeletedProcedure,
                new{IsDeleted = service.IsDeleted},
                commandType: CommandType.StoredProcedure);

            _logger.Debug("Услуга сменила статус на удалена в базе данных");

            return newService;
        }

        public Service UpdateService(Service service)
        {
            _logger.Debug("Подключение к базе данных");

            using IDbConnection connection = Connection;

            _logger.Debug("Подключение к базе данных произведено");

            var newService = connection.QueryFirstOrDefault<Service>(_serviseUpdateProcedure,
                new 
                {
                    Name =  service.Name,
                    OneTimePrice =  service.OneTimePrice,
                    Description = service.Description,
                },
                commandType: CommandType.StoredProcedure);

            _logger.Debug("Услуга изменена в базе данных");

            return newService;
        }
    }
}
