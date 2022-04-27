using Dapper;
using MarvelousService.DataLayer.Configuration;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;

namespace MarvelousService.DataLayer.Repositories
{
    public class ResourceRepository : BaseRepository, IResourceRepository
    {
        private const string _addProcedure = "dbo.Resource_Insert";
        private const string _getByIdProcedure = "dbo.Resource_SelectById";
        private const string _updateProcedure = "dbo.Resource_Update";
        private const string _softDeleteProcedure = "dbo.Resource_SoftDelete";
        private const string _getAllProcedure = "dbo.Resource_SelectAll";

        private readonly ILogger<ResourceRepository> _logger;

        public ResourceRepository(IOptions<DbConfiguration> options, ILogger<ResourceRepository> logger) : base(options)
        {
            _logger = logger;
        }

        public async Task<int> AddResource(Resource resource)
        {
            _logger.LogInformation("Connecting to the MarvelousService.DB");
            using IDbConnection connection = ProvideConnection();
            _logger.LogInformation("Connection succedded");

            var id = await connection.QueryFirstOrDefaultAsync<int>(
                _addProcedure,
                new
                {
                    resource.Name,
                    resource.Price,
                    resource.Description,
                    resource.Type,
                    resource.IsDeleted
                },
                commandType: CommandType.StoredProcedure) ;

            _logger.LogInformation($"Resource - {resource.Name} added to MarvelousService.DB");
            return id;  
        }

        public async Task<List<Resource>> GetAllResources()
        {
            _logger.LogInformation("Requesting a services");
            _logger.LogInformation("Connecting to the MarvelousService.DB");
            using IDbConnection connection = ProvideConnection();
            _logger.LogInformation("Connection succedded");

            var resources = await connection.QueryAsync<Resource>(
                _getAllProcedure,
                commandType: CommandType.StoredProcedure);

            _logger.LogInformation("All resources were successfully recieved");
            return resources.ToList();
        }

        public async Task<Resource> GetResourceById(int id)
        {
            _logger.LogInformation("Requesting a resource by id");
            _logger.LogInformation("Connecting to the MarvelousService.DB");
            using IDbConnection connection = ProvideConnection();
            _logger.LogInformation("Connection succedded");

            var resource = await connection.QueryFirstOrDefaultAsync<Resource>(
                _getByIdProcedure,
                new { Id = id },
                commandType: CommandType.StoredProcedure);

            _logger.LogInformation($"Resource with id {id} was received");
            return resource;
        }

        public async Task SoftDelete(Resource resource)
        {
            _logger.LogInformation("Connecting to the MarvelousService.DB");
            using IDbConnection connection = ProvideConnection();
            _logger.LogInformation("Connection succedded");

            var newResource = await connection.QueryFirstOrDefaultAsync<Resource>(
                _softDeleteProcedure,
                new 
                { 
                    resource.Id,
                    resource.IsDeleted 
                },
                commandType: CommandType.StoredProcedure);

            _logger.LogInformation($"Resource - {resource.Name} changed status to 'Removed' in the MarvelousService.DB");
        }

        public async Task UpdateResource(Resource resource)
        {
            _logger.LogInformation("Connecting to the MarvelousService.DB");
            using IDbConnection connection = ProvideConnection();
            _logger.LogInformation("Connection succedded");

            await connection.ExecuteAsync(_updateProcedure,
                new 
                {
                    resource.Id,
                    resource.Name,
                    resource.Price,
                    resource.Description,
                    resource.Type
                },
                commandType: CommandType.StoredProcedure);

            _logger.LogInformation($"Resource - { resource.Name}, changed in database");
        }
    }
}
