using Dapper;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MarvelousService.DataLayer.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private const string _serviceAddProcedure = "dbo.Service_Insert";
        private const string _serviceGetByLeadIdProcedure = "dbo.Service_SelectByLead";
        private const string _serviceGetByIdProcedure = "dbo.Service_SelectById";


        public IDbConnection _connection;

        public ServiceRepository(IDbConnection dbConnection)
        {
            _connection = dbConnection;
        }

        public IDbConnection Connection => new SqlConnection(_connection.ConnectionString);

        public int AddService(Service service)
        {
            using IDbConnection connection = Connection;

            return connection.QueryFirstOrDefault<int>(_serviceAddProcedure,
                new
                {
                    service.Name,
                    service.Type,
                    service.Period,
                    service.Description,
                    service.Price,                  
                    service.Status,
                    service.LeadId,
                    service.TransactionId                   
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public List<Service> GetByLeadId(int id)
        {
            using IDbConnection connection = Connection;

            return connection.Query<Service>(_serviceGetByLeadIdProcedure,new { LeadId = id },commandType: CommandType.StoredProcedure)
                .ToList();
        }

        public Service GetServiceById(int id)
        {
            using IDbConnection connection = Connection;

            return connection.QuerySingle<Service>(_serviceGetByIdProcedure, new { Id = id },commandType: CommandType.StoredProcedure);
        }
    }
}
