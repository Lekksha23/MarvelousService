using Dapper;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Interfaces;
using Microsoft.Data.SqlClient;
using NLog;
using System.Data;

namespace MarvelousService.DataLayer.Repositories
{
    public class ServiceToLeadRepository : IServiceToLeadRepository
    {
        private const string _serviceAddProcedure = "dbo.ServiceToLead_Insert";
        private const string _serviceGetByLeadIdProcedure = "dbo.ServiceToLead_SelectByLead";
        private const string _serviceGetByIdProcedure = "dbo.ServiceToLead_SelectById";
        private static Logger _logger;


        public IDbConnection _connection;

        public ServiceToLeadRepository(IDbConnection dbConnection)
        {
            _connection = dbConnection;

            _logger = LogManager.GetCurrentClassLogger();
        }

        public IDbConnection Connection => new SqlConnection(_connection.ConnectionString);

        public int AddServiceToLead(ServiceToLead serviceToLead)
        {

            _logger.Debug("Подключение к базе данных");

            using IDbConnection connection = Connection;

            _logger.Debug("Подключение к базе данных произведено");

            var newServiceToLead =  connection.QueryFirstOrDefault<int>(_serviceAddProcedure,
                new
                {
                    serviceToLead.Type,
                    serviceToLead.Period,
                    serviceToLead.Price,
                    serviceToLead.Status,
                    serviceToLead.LeadId,
                    serviceToLead.ServiceId,
                    serviceToLead.TransactionId                   
                },
                commandType: CommandType.StoredProcedure);
            _logger.Debug("Услуга добавлена в базу данных");

            return newServiceToLead;


        }

        public List<ServiceToLead> GetByLeadId(int id)
        {
            _logger.Debug("Подключение к базе данных");

            using IDbConnection connection = Connection;

            _logger.Debug("Подключение к базе данных произведено");

            var listServiceToLead =  connection.Query<ServiceToLead>(_serviceGetByLeadIdProcedure,new { LeadId = id },commandType: CommandType.StoredProcedure)
                .ToList();

            _logger.Debug("Услуги по LeadId получены");

            return listServiceToLead;
        }

        public ServiceToLead GetServiceToLeadById(int id)
        {
            _logger.Debug("Подключение к базе данных");

            using IDbConnection connection = Connection;

            _logger.Debug("Подключение к базе данных произведено");

            var service = connection.QuerySingle<ServiceToLead>(_serviceGetByIdProcedure, new { Id = id },commandType: CommandType.StoredProcedure);

            _logger.Debug($"Услуга под id = {id} получена");

            return service;
        }
    }
}
