using Dapper;
using MarvelousService.DataLayer.Configuration;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;

namespace MarvelousService.DataLayer.Repositories
{
    public class ServiceToLeadRepository : BaseRepository, IServiceToLeadRepository
    {
        private const string _insertProcedure = "dbo.ServiceToLead_Insert";
        private const string _selectByLeadIdProcedure = "dbo.ServiceToLead_SelectByLead";
        private const string _selectByIdProcedure = "dbo.ServiceToLead_SelectById";
        private const string _updateStatusProcedure = "dbo.ServiceToLead_SelectById";
        private readonly ILogger<ServiceToLeadRepository> _logger;

        public ServiceToLeadRepository(IOptions<DbConfiguration> options, ILogger<ServiceToLeadRepository> logger) : base(options)
        {
            _logger = logger;
        }

        public async Task<int> AddServiceToLead(ServiceToLead serviceToLead)
        {
            _logger.LogInformation("Connecting to the MarvelousService.DB");
            using IDbConnection connection = ProvideConnection();
            _logger.LogInformation("Connection succedded");

            var id = await connection.QueryFirstOrDefaultAsync<int>(_insertProcedure,
                new
                {
                    serviceToLead.Period,
                    serviceToLead.Price,
                    serviceToLead.Status,
                    serviceToLead.LeadId,
                    serviceToLead.ServiceId,
                },
                commandType: CommandType.StoredProcedure);

            _logger.LogInformation($"Service {id} was added to Lead {serviceToLead.LeadId}");
            return id;
        }

        public async Task<List<ServiceToLead>> GetByLeadId(int id)
        {
            _logger.LogInformation("Connecting to the MarvelousService.DB");
            using IDbConnection connection = ProvideConnection();
            _logger.LogInformation("Connection succedded");

            var listServiceToLead = await connection.QueryAsync<ServiceToLead, Service, ServiceToLead>(
                _selectByLeadIdProcedure,
                 (serviceToLead, service) =>
                 {
                     serviceToLead.ServiceId = service;
                     return serviceToLead;
                 },
                new
                { Id = id },
                splitOn: "Id",
                commandType: CommandType.StoredProcedure);

            _logger.LogInformation($"Services for Lead with id {id} were received");
            return listServiceToLead.ToList();
        }

        public async Task<ServiceToLead> GetServiceToLeadById(int id)
        {
            _logger.LogInformation("Connecting to the MarvelousService.DB");
            using IDbConnection connection = ProvideConnection();
            _logger.LogInformation("Connection succedded");

            var service = connection.QueryAsync<ServiceToLead, Service, ServiceToLead>(
                _selectByIdProcedure,
                 (serviceToLead, service) =>
                 {
                     serviceToLead.ServiceId = service;
                     return serviceToLead;
                 },
                new
                { Id = id },
                splitOn: "Id",
                commandType: CommandType.StoredProcedure)
                .Result
                .FirstOrDefault();

            _logger.LogInformation($"Service with id {id} received");
            return service;
        }

        public async void UpdateStatusById(int id, Status status)
        {
            _logger.LogInformation("Connecting to the MarvelousService.DB");
            using IDbConnection connection = ProvideConnection();
            _logger.LogInformation("Connection succedded");

            await connection.ExecuteAsync(
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
