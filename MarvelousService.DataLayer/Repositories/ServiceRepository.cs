﻿using Dapper;
using MarvelousService.DataLayer.Configuration;
using MarvelousService.DataLayer.Entities;
using MarvelousService.DataLayer.Repositories.Interfaces;
using Microsoft.Extensions.Options;
using NLog;
using System.Data;

namespace MarvelousService.DataLayer.Repositories
{
    public class ServiceRepository : BaseRepository, IServiceRepository
    {
        private const string _serviceAddProcedure = "dbo.Service_Insert";
        private const string _serviceGetByIdProcedure = "dbo.Service_SelectById";
        private const string _serviceUpdateProcedure = "dbo.Service_Update";
        private const string _serviceSoftDeleteProcedure = "dbo.Service_SoftDelete";
        private static Logger _logger;

        public ServiceRepository(IOptions<DbConfiguration> options) : base(options)
        {
            _logger = LogManager.GetCurrentClassLogger();
        }


        public int AddService(Service service)
        {
            _logger.Info("Подключение к базе данных");
            using IDbConnection connection = ProvideConnection();
            _logger.Info("Подключение к базе данных произведено");

            var id = connection.QueryFirstOrDefault<int>(_serviceAddProcedure,
                new
                {
                    service.Name,
                    service.OneTimePrice,
                    service.Description
                },
                commandType: CommandType.StoredProcedure);

            _logger.Info("Услуга добавлена в базу данных");
            return id;
        }

        public Service GetServiceById(int id)
        {
            _logger.Info("Подключение к базе данных");
            using IDbConnection connection = ProvideConnection();
            _logger.Info("Подключение к базе данных произведено");

            var listService = connection.QueryFirstOrDefault<Service>(_serviceGetByIdProcedure, 
                new { Id = id }, 
                commandType: CommandType.StoredProcedure);

            _logger.Info("Услуги по Id получены");
            return listService;
        }

        public void SoftDeleted(int id, Service service)
        {
            _logger.Info("Подключение к базе данных");
            using IDbConnection connection = ProvideConnection();
            _logger.Info("Подключение к базе данных произведено");

            var newService = connection.QueryFirstOrDefault<Service>(_serviceSoftDeleteProcedure,
                new{IsDeleted = service.IsDeleted},
                commandType: CommandType.StoredProcedure);

            _logger.Info("Услуга сменила статус на удалена в базе данных");

        }

        public void UpdateService(int id, Service service)
        {
            _logger.Info("Подключение к базе данных");
            using IDbConnection connection = ProvideConnection();
            _logger.Info("Подключение к базе данных произведено");

            var newService = connection.QueryFirstOrDefault<Service>(_serviceUpdateProcedure,
                new 
                {
                    service.Name,
                    service.OneTimePrice,
                    service.Description,
                },
                commandType: CommandType.StoredProcedure);

            _logger.Info("Услуга изменена в базе данных");

        }
    }
}
