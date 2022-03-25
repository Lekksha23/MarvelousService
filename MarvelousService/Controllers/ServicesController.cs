﻿using AutoMapper;
using CRM.APILayer.Attribites;
using Marvelous.Contracts.Enums;
using MarvelousService.API.Extensions;
using MarvelousService.API.Models;
using MarvelousService.API.Models.ExceptionModel;
using MarvelousService.API.Models.Request;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace MarvelousService.API.Controllers
{
    [ApiController]
    [Route("api/services")]
    [AuthorizeRole]
    public class ServicesController : Controller
    {
        private readonly IServiceToService _serviceService;
        private readonly IMapper _autoMapper;
        private readonly ILogger<ServicesController> _logger;

        public ServicesController(IServiceToService serviceService, IMapper autoMapper, ILogger<ServicesController> logger)
        {
            _serviceService = serviceService;
            _autoMapper = autoMapper;
            _logger = logger;
        }

        //api/services
        [HttpPost]
        [SwaggerOperation("Add new service")]
        [AuthorizeRole(Role.Admin)]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ExceptionOutputModel), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ExceptionOutputModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<int>> AddService([FromBody] ServiceInsertRequest serviceInsertRequest)
        {
            var leadIdentity = this.GetLeadFromToken();

            _logger.LogInformation($"Получен запрос на добавление новой услуги.");

            var serviceModel = _autoMapper.Map<ServiceModel>(serviceInsertRequest);
            serviceModel.Id = leadIdentity.Id;
            Role role = leadIdentity.Role;
            var id = await _serviceService.AddService(serviceModel, (int)role);
            _logger.LogInformation($"Услуга с id = {id} успешно добавлена.");
            return StatusCode(StatusCodes.Status201Created, id);
        }

        //api/services/
        [HttpGet("id")]
        [SwaggerOperation("Get service by id")]
        [SwaggerResponse(StatusCodes.Status200OK, "Successful", typeof(List<ServiceResponse>))]
        public async Task<ActionResult<List<ServiceResponse>>> GetServiceById(int id)
        {
            _logger.LogInformation($"Запрос на получение услуги по id = {id}");
            var serviceModel = await _serviceService.GetServiceById(id);
            var result = _autoMapper.Map<List<ServiceResponse>>(serviceModel);
            _logger.LogInformation($"Услуга по id = {id} получена");
            return Ok(result);
        }

        //api/services/
        [HttpPut("id")]
        [SwaggerOperation("Update services")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionOutputModel), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ExceptionOutputModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ExceptionOutputModel), StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<ServiceUpdateRequest>> UpdateService(int id, ServiceUpdateRequest serviceUpdateRequest)
        {
            ServiceModel service = _autoMapper.Map<ServiceModel>(serviceUpdateRequest);
            await _serviceService.UpdateService(id, service);
            return Ok(service);
        }

        //api/services/
        [HttpPatch("id")]
        [SwaggerOperation("Deleted services")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionOutputModel), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(ExceptionOutputModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ExceptionOutputModel), StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult<ServiceDeletedRequest>> SoftDelete(int id, ServiceDeletedRequest serviceDeletedRequest)
        {
            ServiceModel service = _autoMapper.Map<ServiceModel>(serviceDeletedRequest);
            await _serviceService.SoftDelete(id, service);
            return Ok(service);
        }
    }
}
