using Dapper;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Interfaces;
using Microsoft.Data.SqlClient;
using NLog;
using System.Data;

namespace MarvelousService.DataLayer.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private const string _serviceAddProcedure = "dbo.Service_Insert";
        private const string _serviceGetByLeadIdProcedure = "dbo.Service_SelectByLead";
        private const string _serviceGetByIdProcedure = "dbo.Service_SelectById";
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

            var newService =  connection.QueryFirstOrDefault<int>(_serviceAddProcedure,
                new
                {
                    service.Name,
                    service.Type,
                    service.Period,
                    service.Description,
                    service.Price,                  
                    service.Status,
                    service.LeadId,
                    service.TransactionId                   
                },
                commandType: CommandType.StoredProcedure
            );
            _logger.Debug("Услуга добавлена в базу данных");

            return newService;


        }

        public List<Service> GetByLeadId(int id)
        {
            _logger.Debug("Подключение к базе данных");

            using IDbConnection connection = Connection;

            _logger.Debug("Подключение к базе данных произведено");

            var listService =  connection.Query<Service>(_serviceGetByLeadIdProcedure,new { LeadId = id },commandType: CommandType.StoredProcedure)
                .ToList();

            _logger.Debug("Услуги по LeadId получены");

            return listService;
        }

        public Service GetServiceById(int id)
        {
            _logger.Debug("Подключение к базе данных");

            using IDbConnection connection = Connection;

            _logger.Debug("Подключение к базе данных произведено");

            var service = connection.QuerySingle<Service>(_serviceGetByIdProcedure, new { Id = id },commandType: CommandType.StoredProcedure);

            _logger.Debug($"Услуга под id = {id} получена");

            return service;
        }
    }
}
