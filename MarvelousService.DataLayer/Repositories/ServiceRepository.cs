using Dapper;
using MarvelousService.DataLayer.Configuration;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;

namespace MarvelousService.DataLayer.Repositories
{
    public class ServiceRepository : BaseRepository, IServiceRepository
    {
        private const string _serviceAddProcedure = "dbo.Service_Insert";
        private const string _serviceGetByIdProcedure = "dbo.Service_SelectById";
        private const string _serviceUpdateProcedure = "dbo.Service_Update";
        private const string _serviceSoftDeleteProcedure = "dbo.Service_SoftDelete";
        private const string _serviceGetAllService = "dbo.Service_SelectAll";

        private readonly ILogger<ServiceRepository> _logger;

        public ServiceRepository(IOptions<DbConfiguration> options, ILogger<ServiceRepository> logger) : base(options)
        {
            _logger = logger;
        }

        public async Task<int> AddService(Service service)
        {
            _logger.LogInformation("Connecting to the MarvelousService.DB");
            using IDbConnection connection = ProvideConnection();
            _logger.LogInformation("Connection succedded");

            var id = await connection.QueryFirstOrDefaultAsync<int>(
                _serviceAddProcedure,
                new
                {
                    service.Name,
                    service.Price,
                    service.Description,
                    service.Type,
                    service.IsDeleted
                },
                commandType: CommandType.StoredProcedure) ;

            _logger.LogInformation($"Service - {service.Name} added to MarvelousService.DB");
            return id;  
        }

        public async Task<List<Service>> GetAllService()
        {
            _logger.LogInformation("Requesting a services");
            _logger.LogInformation("Connecting to the MarvelousService.DB");
            using IDbConnection connection = ProvideConnection();
            _logger.LogInformation("Connection succedded");

            var services = connection.QueryAsync<Service>(
                _serviceGetAllService, commandType: CommandType.StoredProcedure)
                .Result.ToList();

            _logger.LogInformation("The selection was successfully, selected services");
            return services;
        }

        public async Task<Service> GetServiceById(int id)
        {
            _logger.LogInformation("Requesting a service by id");
            _logger.LogInformation("Connecting to the MarvelousService.DB");
            using IDbConnection connection = ProvideConnection();
            _logger.LogInformation("Connection succedded");

            var service = await connection.QueryFirstOrDefaultAsync<Service>(
                _serviceGetByIdProcedure,
                new { Id = id },
                commandType: CommandType.StoredProcedure);

            _logger.LogInformation($"Service with id {id} was received");
            return service;
        }

        public async Task SoftDelete(Service service)
        {
            _logger.LogInformation("Connecting to the MarvelousService.DB");
            using IDbConnection connection = ProvideConnection();
            _logger.LogInformation("Connection succedded");

            var newService = await connection.QueryFirstOrDefaultAsync<Service>(
                _serviceSoftDeleteProcedure,
                new 
                { 
                    service.Id,
                    service.IsDeleted 
                },
                commandType: CommandType.StoredProcedure);

            _logger.LogInformation($"Service - {service.Name} changed status to 'Removed' in the MarvelousService.DB");
        }

        public async Task UpdateService(Service service)
        {
            _logger.LogInformation("Connecting to the MarvelousService.DB");
            using IDbConnection connection = ProvideConnection();
            _logger.LogInformation("Connection succedded");

            await connection.ExecuteAsync(_serviceUpdateProcedure,
                new 
                {
                    service.Id,
                    service.Name,
                    service.Price,
                    service.Description,
                    service.Type
                },
                commandType: CommandType.StoredProcedure);

            _logger.LogInformation($"Service - { service.Name}, changed in database");
        }
    }
}
