﻿using Dapper;
using MarvelousService.DataLayer.Configuration;
using MarvelousService.DataLayer.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;

namespace MarvelousService.DataLayer.Repositories
{
    public class ResourcePaymentRepository : BaseRepository, IResourcePaymentRepository
    {
        private const string _insertProcedure = "dbo.ResourcePayment_Insert";
        private const string _selectByLeadResourceProcedure = "dbo.ResourcePayment_SelectByLeadResource";
        private readonly ILogger<ResourcePaymentRepository> _logger;

        public ResourcePaymentRepository(IOptions<DbConfiguration> options, ILogger<ResourcePaymentRepository> logger) : base(options)
        {
            _logger = logger;
        }

        public async Task<int> AddResourcePayment(ResourcePayment resourcePayment)
        {
            _logger.LogInformation("Connecting to the MarvelousService.DB.");
            using IDbConnection connection = ProvideConnection();
            _logger.LogInformation("Connection succedded.");

            var id = await connection.QueryFirstOrDefaultAsync<int>(
                    _insertProcedure,
                    new
                    {
                        resourcePayment.LeadResource.Id,
                        resourcePayment.TransactionId
                    },
                    commandType: CommandType.StoredProcedure
                );
            _logger.LogInformation($"Resource payment was added to MarvelousServiceDB.");
            return id;
        }

        public async Task<List<ResourcePayment>> GetResourcePaymentsByLeadResourceId(int id)
        {
            _logger.LogInformation("Connecting to the MarvelousService.DB.");
            using IDbConnection connection = ProvideConnection();
            _logger.LogInformation("Connection succedded.");

            var servicePayments = await connection
                .QueryAsync<ResourcePayment, LeadResource, ResourcePayment>(
                _selectByLeadResourceProcedure,
                (resourcePayment, leadResource) =>
                {
                    resourcePayment.LeadResource = leadResource;
                    return resourcePayment;
                },
                new { Id = id },
                splitOn: "LeadResourceId",
                commandType: CommandType.StoredProcedure);

            _logger.LogInformation($"Information about subscription pays or onetime pay with id {id} were received.");
            return servicePayments.ToList();
        }
    }
}