namespace Services.Pizza
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contracts.Common.Interfaces;
    using Contracts.Filters;
    using Contracts.Pizza.Interfaces;
    using Contracts.Pizza.Models;
    using Contracts.Pizza.Requests;
    using Contracts.Result;
    using Entities.Pizza.Repositories;
    using Entities.Pizza.Domain;
    using Storage.Pizza.Factories;

    public class PizzaService : IPizzaService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPizzaRepository _pizzaRepository;

        public PizzaService(
            IPizzaRepository pizzaRepository,
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _pizzaRepository = pizzaRepository;
        }

        public async Task<Result<PizzaDto>> GetPizzaAsync(Guid pizzaUid)
        {
            return await _pizzaRepository.GetPizzaAsync(pizzaUid);
        }

        public async Task<Result<IReadOnlyList<PizzaDto>>> GetPizzasAsync(FilterBySkipTakeRequest filter)
        {
            return await _pizzaRepository.GetPizzasAsync(filter);
        }

        public async Task<Result<PizzaDto>> InsertPizzaAsync(PizzaRequest request)
        {
            Result<Pizza> pizzaOrError =  _pizzaRepository.CreatePizza(request);

            if (pizzaOrError.IsFailure)
            {
                return Result.FromError<PizzaDto>(pizzaOrError);
            }

            _pizzaRepository.Insert(pizzaOrError.Value);

            //fire event here
            await _unitOfWork.SaveAsync();

            return Result.Ok(pizzaOrError.Value.ToPizzaDto());
        }
    }
}
