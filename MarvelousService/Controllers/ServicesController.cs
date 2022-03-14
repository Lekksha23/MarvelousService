using AutoMapper;
using MarvelousService.API.Models;
using MarvelousService.BusinessLayer.Models;
using MarvelousService.BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Swashbuckle.AspNetCore.Annotations;

namespace MarvelousService.API.Controllers
{
    [ApiController]
    [Route("api/services")]
    public class ServicesController : Controller
    {
        private readonly IServiceService _serviceService;
        private readonly IMapper _autoMapper;
        private static Logger _logger;

        public ServicesController(IServiceService serviceService, IMapper autoMapper)
        {
            _serviceService = serviceService;
            _autoMapper = autoMapper;
            _logger = LogManager.GetCurrentClassLogger();
        }

        //api/Leads
        [HttpPost]
        [SwaggerOperation("Add new service")]
        [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
        public ActionResult<int> AddService([FromBody] ServiceInsertRequest serviceInsertRequest)
        {
            //_logger.Info($"Получен запрос на добавление новой услуги.");
            var serviceModel = _autoMapper.Map<ServiceModel>(serviceInsertRequest);
            var id = _serviceService.AddService(serviceModel);
            //_logger.Info($"Услуга с id = {id} успешно добавлена.");
            return StatusCode(StatusCodes.Status201Created, id);
        }
    }
}
