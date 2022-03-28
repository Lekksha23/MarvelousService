using Dapper;
using MarvelousService.DataLayer.Configuration;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Enums;
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
        private const string _updateStatusProcedure = "dbo.ServiceToLead_SelectById";
        private readonly ILogger<ServiceToLeadRepository> _logger;

        public ServiceToLeadRepository(IOptions<DbConfiguration> options, ILogger<ServiceToLeadRepository> logger) : base(options)
        {
            _logger = logger;
        }

        public async Task<int> AddServiceToLead(ServiceToLead serviceToLead)
        {
            _logger.LogInformation("Подключение к базе данных");
            using IDbConnection connection = ProvideConnection();
            _logger.LogInformation("Подключение к базе данных произведено");

            var id = await connection.QueryFirstOrDefaultAsync<int>(_serviceAddProcedure,
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
            return id;
        }

        public async Task<List<ServiceToLead>> GetByLeadId(int id)
        {
            _logger.LogInformation("Запрашиваем id");
            _logger.LogInformation("Подключение к базе данных");
            using IDbConnection connection = ProvideConnection();
            _logger.LogInformation("Подключение к базе данных произведено");

            var listServiceToLead = await connection.QueryAsync<ServiceToLead>(
                _serviceGetByLeadIdProcedure,
                new { LeadId = id },
                commandType: CommandType.StoredProcedure);

            _logger.LogInformation("Услуги по LeadId  получены");
            return listServiceToLead.ToList();
        }

        public async Task<List<ServiceToLead>> GetServiceToLeadById(int id)
        {
            _logger.LogInformation("Подключение к базе данных");
            using IDbConnection connection = ProvideConnection();
            _logger.LogInformation("Подключение к базе данных произведено");

            var service = await connection.QueryAsync<ServiceToLead>(
                _serviceGetByIdProcedure,
                new { Id = id },
                commandType: CommandType.StoredProcedure);

            _logger.LogInformation($"Услуга под id = {id} получена");
            return service.ToList();
        }

        public async void UpdateStatusById(int id, Status status)
        {
            _logger.LogInformation("Connecting to the MarvelousService.DB");
            using IDbConnection connection = ProvideConnection();
            _logger.LogInformation("Connection succedded");

            await connection.QueryAsync(
                _updateStatusProcedure,
                new 
                {
                    id,
                    status
                },
                commandType: CommandType.StoredProcedure);

            _logger.LogInformation($"Service with id = {id} was updated");
        }
    }
}
