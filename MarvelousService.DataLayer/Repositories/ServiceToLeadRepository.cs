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

        public async Task<long> AddServiceToLead(ServiceToLead serviceToLead)
        {
            _logger.LogInformation("Подключение к базе данных");

            using IDbConnection connection = ProvideConnection();

            _logger.LogInformation("Подключение к базе данных произведено");

            var newServiceToLead = await connection.QueryFirstOrDefaultAsync<long>(_serviceAddProcedure,
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

        public async Task<List<ServiceToLead>> GetByLeadId(long id)
        {
            _logger.LogInformation("Запрашиваем id");
            _logger.LogInformation("Подключение к базе данных");

            using IDbConnection connection = ProvideConnection();

            _logger.LogInformation("Подключение к базе данных произведено");

            var listServiceToLead = (await connection.QueryAsync<ServiceToLead>(_serviceGetByLeadIdProcedure,new { LeadId = id },
                commandType: CommandType.StoredProcedure))
                .ToList();

            _logger.LogInformation("Услуги по LeadId  получены");
            return listServiceToLead;
        }

        public async Task<ServiceToLead> GetServiceToLeadById(long id)
        {
            _logger.LogInformation("Подключение к базе данных");

            using IDbConnection connection = ProvideConnection();

            _logger.LogInformation("Подключение к базе данных произведено");

            var service = await connection.QuerySingleAsync<ServiceToLead>(_serviceGetByIdProcedure, new { Id = id },
                commandType: CommandType.StoredProcedure);

            _logger.LogInformation($"Услуга под id = {id} получена");

            return service;
        }
    }
}
