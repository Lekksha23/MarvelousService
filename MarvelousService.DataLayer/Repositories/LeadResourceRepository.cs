using Dapper;
using MarvelousService.DataLayer.Configuration;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;

namespace MarvelousService.DataLayer.Repositories
{
    public class LeadResourceRepository : BaseRepository, ILeadResourceRepository
    {
        private const string _selectByPayDateProcedure = "dbo.LeadResource_SelectByPayDate";
        private const string _selectByLeadIdProcedure = "dbo.LeadResource_SelectByLead";
        private const string _updateStatusProcedure = "dbo.LeadResource_UpdateStatus";
        private const string _selectByIdProcedure = "dbo.LeadResource_SelectById";
        private const string _insertProcedure = "dbo.LeadResource_Insert";
        private readonly ILogger<LeadResourceRepository> _logger;

        public LeadResourceRepository(IOptions<DbConfiguration> options, ILogger<LeadResourceRepository> logger) : base(options)
        {
            _logger = logger;
        }

        public async Task<int> AddLeadResource(LeadResource leadResource)
        {
            _logger.LogInformation("Connecting to the MarvelousService.DB");
            using IDbConnection connection = ProvideConnection();
            _logger.LogInformation("Connection succedded");

            var id = await connection.QueryFirstOrDefaultAsync<int>(_insertProcedure,
                new
                {
                    leadResource.Period,
                    leadResource.Price,
                    leadResource.Status,
                    leadResource.LeadId,
                    leadResource.Resource.Id,
                },
                commandType: CommandType.StoredProcedure);

            _logger.LogInformation($"Resource {id} was added to Lead {leadResource.LeadId}");
            return id;
        }

        public async Task<List<LeadResource>> GetByLeadId(int id)
        {
            _logger.LogInformation("Connecting to the MarvelousService.DB");
            using IDbConnection connection = ProvideConnection();
            _logger.LogInformation("Connection succedded");

            var leadResourceList = await connection.QueryAsync<LeadResource, Resource, LeadResource>(
                _selectByLeadIdProcedure,
                 (leadResource, resource) =>
                 {
                     leadResource.Resource = resource;
                     return leadResource;
                 },
                new
                { Id = id },
                splitOn: "Id",
                commandType: CommandType.StoredProcedure);

            _logger.LogInformation($"Services for Lead with id {id} were received");
            return leadResourceList.ToList();
        }

        public async Task<List<LeadResource>> GetByPayDate(DateTime payDate)
        {
            _logger.LogInformation("Requesting LeadResources by pay date");
            _logger.LogInformation("Connecting to the MarvelousService.DB");
            using IDbConnection connection = ProvideConnection();
            _logger.LogInformation("Connection succedded");

            var leadResourceList = connection.QueryAsync<LeadResource>(
                _selectByPayDateProcedure,
                new { PayDate = payDate },
                commandType: CommandType.StoredProcedure)
                .Result
                .ToList();

            _logger.LogInformation($"Lead resources that need to be paid at {payDate} were received");
            return leadResourceList;
        }

        public async Task<LeadResource> GetLeadResourceById(int id)
        {
            _logger.LogInformation("Connecting to the MarvelousService.DB");
            using IDbConnection connection = ProvideConnection();
            _logger.LogInformation("Connection succedded");

            var service = connection.QueryAsync<LeadResource, Resource, LeadResource>(
                _selectByIdProcedure,
                 (leadResource, resource) =>
                 {
                     leadResource.Resource = resource;
                     return leadResource;
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
