namespace Pizzeria.Controllers
{
    using Contracts.Filters;
    using Contracts.Pizza.Interfaces;
    using Contracts.Pizza.Models;
    using Contracts.Pizza.Requests;
    using Contracts.Result;
    using Microsoft.AspNetCore.Mvc;
    using Pizzeria.Services;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("api/pizzeria/pizzas")]
    [ApiController]
    public class PizzaServiceController : ExtendedApiController
    {
        private readonly IPizzaService _pizzaService;

        public PizzaServiceController(IPizzaService pizzaService)
        {
            _pizzaService = pizzaService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetPizzas([FromQuery]FilterBySkipTakeRequest filter)
        {
            Result<IReadOnlyList<PizzaDto>> response =
                await _pizzaService.GetPizzasAsync(filter);

            return OkOrError(response);
        }

        [HttpGet]
        [Route("{pizzaUid:guid}")]
        public async Task<IActionResult> GetPizza([FromBody]Guid pizzaUid)
        {
            Result<PizzaDto> response =
                await _pizzaService.GetPizzaAsync(pizzaUid);

            return OkOrError(response);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> InsertPizza([FromBody]PizzaRequest request)
        {
            Result<PizzaDto> response =
                await _pizzaService.InsertPizzaAsync(request);

            return OkOrError(response);
        }
    }
}
