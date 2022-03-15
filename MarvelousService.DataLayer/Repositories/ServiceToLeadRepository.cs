using Dapper;
using MarvelousService.DataLayer.Configuration;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Interfaces;
using Microsoft.Extensions.Options;
using NLog;
using System.Data;

namespace MarvelousService.DataLayer.Repositories
{
    public class ServiceToLeadRepository : BaseRepository, IServiceToLeadRepository
    {
        private const string _serviceAddProcedure = "dbo.ServiceToLead_Insert";
        private const string _serviceGetByLeadIdProcedure = "dbo.ServiceToLead_SelectByLead";
        private const string _serviceGetByIdProcedure = "dbo.ServiceToLead_SelectById";
        private const string _periodProcedure = "dbo.Period_Insert";
        private static Logger _logger;

        public ServiceToLeadRepository(IOptions<DbConfiguration> options) : base(options)
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public int AddPeriod(Period period)
        {
            _logger.Debug("Подключение к базе данных");

            using IDbConnection connection = ProvideConnection();

            _logger.Debug("Подключение к базе данных произведено");

            var newPeriod = connection.QueryFirstOrDefault<int>(_periodProcedure,
                new{period.Season},
                commandType: CommandType.StoredProcedure);

            _logger.Debug("Периуд добавлена в базу данных");

            return newPeriod;
        }

        public int AddServiceToLead(ServiceToLead serviceToLead)
        {
            _logger.Debug("Подключение к базе данных");
            using IDbConnection connection = ProvideConnection();
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
                },
                commandType: CommandType.StoredProcedure);

            _logger.Debug("Услуга добавлена в базу данных");
            return newServiceToLead;
        }

        public List<ServiceToLead> GetByLeadId(int id)
        {
            _logger.Debug("Подключение к базе данных");
            using IDbConnection connection = ProvideConnection();
            _logger.Debug("Подключение к базе данных произведено");

            var listServiceToLead =  connection.Query<ServiceToLead>(_serviceGetByLeadIdProcedure,new { LeadId = id },commandType: CommandType.StoredProcedure)
                .ToList();

            _logger.Debug("Услуги по LeadId получены");
            return listServiceToLead;
        }

        public ServiceToLead GetServiceToLeadById(int id)
        {
            _logger.Debug("Подключение к базе данных");
            using IDbConnection connection = ProvideConnection();
            _logger.Debug("Подключение к базе данных произведено");

            var service = connection.QuerySingle<ServiceToLead>(_serviceGetByIdProcedure, new { Id = id },commandType: CommandType.StoredProcedure);

            _logger.Debug($"Услуга под id = {id} получена");
            return service;
        }
    }
}
