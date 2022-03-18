using Dapper;
using MarvelousService.DataLayer.Configuration;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;

namespace MarvelousService.DataLayer.Repositories
{
    public class ServiceToLeadRepository : BaseRepository, IServiceToLeadRepository
    {
        private const string _serviceAddProcedure = "dbo.ServiceToLead_Insert";
        private const string _serviceGetByLeadIdProcedure = "dbo.ServiceToLead_SelectByLead";
        private const string _serviceGetByIdProcedure = "dbo.ServiceToLead_SelectById";
        private readonly ILogger<ServiceRepository> _logger;

        public ServiceToLeadRepository(IOptions<DbConfiguration> options, ILogger<ServiceRepository> logger) : base(options)
        {
            _logger = logger;
        }

        public int AddServiceToLead(ServiceToLead serviceToLead)
        {
            _logger.LogInformation("Подключение к базе данных");

            using IDbConnection connection = ProvideConnection();

            _logger.LogInformation("Подключение к базе данных произведено");

            var newServiceToLead =  connection.QueryFirstOrDefault<int>(_serviceAddProcedure,
                new
                {
                    serviceToLead.Period,
                    serviceToLead.Price,
                    serviceToLead.Status,
                    serviceToLead.LeadId,
                    serviceToLead.ServiceId,                   
                },
                commandType: CommandType.StoredProcedure);

            _logger.LogInformation($"Услуга  добавлена в базу данных");

            return newServiceToLead;
        }

        public List<ServiceToLead> GetByLeadId(int id)
        {
            _logger.LogInformation("Запрашиваем id");
            _logger.LogInformation("Подключение к базе данных");

            using IDbConnection connection = ProvideConnection();

            _logger.LogInformation("Подключение к базе данных произведено");

            var listServiceToLead =  connection.Query<ServiceToLead>(_serviceGetByLeadIdProcedure,new { LeadId = id },commandType: CommandType.StoredProcedure)
                .ToList();

            _logger.LogInformation("Услуги по LeadId  получены");
            return listServiceToLead;
        }

        public ServiceToLead GetServiceToLeadById(int id)
        {
            _logger.LogInformation("Подключение к базе данных");

            using IDbConnection connection = ProvideConnection();

            _logger.LogInformation("Подключение к базе данных произведено");

            var service = connection.QuerySingle<ServiceToLead>(_serviceGetByIdProcedure, new { Id = id },commandType: CommandType.StoredProcedure);

            _logger.LogInformation($"Услуга под id = {id} получена");

            return service;
        }
    }
}
