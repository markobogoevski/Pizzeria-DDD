namespace Storage.Pizza.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Contracts.Common.ErrorCodes;
    using Contracts.Filters;
    using Contracts.Pizza.Models;
    using Contracts.Pizza.Requests;
    using Contracts.Result;
    using global::Entities.Pizza.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Storage.Common.Database.Context;
    using Storage.Common.Repositories;
    using Storage.Pizza.Entities;
    using Storage.Pizza.Factories;

    public class PizzaRepository : Repository<Pizza>, IPizzaRepository
    {
        public PizzaRepository(IPizzeriaDbContext pizzaDbContext) : base(pizzaDbContext) { }

        public Result<global::Entities.Pizza.Domain.Pizza> CreatePizza(PizzaRequest request)
        {
            Result<IEnumerable<global::Entities.Pizza.Domain.Ingredient>> ingredientsOrError =  GetPizzaIngredients(request.Ingredients);
            if (ingredientsOrError.IsFailure)
            {
                return Result.FromError<global::Entities.Pizza.Domain.Pizza>(ingredientsOrError);
            }

            return global::Entities.Pizza.Domain.Pizza.Create(Guid.NewGuid(),
                                                              DateTime.UtcNow,
                                                              request.Name,
                                                              request.Size,
                                                              ingredientsOrError.Value.ToList());
        }

        private Result<IEnumerable<global::Entities.Pizza.Domain.Ingredient>> GetPizzaIngredients(IEnumerable<IngredientDto> ingredients)
        {
            IReadOnlyList<Result<global::Entities.Pizza.Domain.Ingredient>> ingredientResults = 
                ingredients.Select(ingredient => global::Entities.Pizza.Domain.Ingredient.Create(ingredient.Id,
                                                                                                 DateTime.UtcNow,
                                                                                                 ingredient.Name,
                                                                                                 ingredient.Quantity)).ToArray();

            Result okOrError = Result.FirstFailureOrOk(ingredientResults);
            if (okOrError.IsFailure)
            {
                return Result.FromError<IEnumerable<global::Entities.Pizza.Domain.Ingredient>>(okOrError);
            }

            return Result.Ok(ingredientResults.Select(ingredient => ingredient.Value));
        }

        public async Task<Result<PizzaDto>> GetPizzaAsync(Guid pizzaUid)
        {
            PizzaDto pizzaDto = await (from dbPizza in AllNoTrackedOf<Pizza>()
                                       where dbPizza.Uid == pizzaUid
                                       select dbPizza.ToPizzaDto())
                                       .SingleOrDefaultAsync();

            if (pizzaDto == null)
            {
                return Result.NotFound<PizzaDto>(ResultErrorCodes.PIZZA_NOT_FOUND);
            }

            return Result.Ok(pizzaDto);
        }

        public async Task<Result<IReadOnlyList<PizzaDto>>> GetPizzasAsync(FilterBySkipTakeRequest filter)
        {
            IReadOnlyList<PizzaDto> pizzaDtos = await (from dbPizza in AllNoTrackedOf<Pizza>()
                                                       select dbPizza.ToPizzaDto())
                                                       .Skip((int)filter.Skip)
                                                       .Take(filter.Take)
                                                       .ToArrayAsync();

            if (pizzaDtos == null)
            {
                return Result.NotFound<IReadOnlyList<PizzaDto>>(ResultErrorCodes.NO_PIZZAS);
            }

            return Result.Ok(pizzaDtos);
        }

        public void Insert(global::Entities.Pizza.Domain.Pizza pizza)
        {
            Pizza dbPizza = pizza.ToPizzaStorage();

            foreach (Ingredient ingredient in dbPizza.Ingredients)
            {
                ingredient.Pizza = dbPizza;
                ingredient.PizzaFk = dbPizza.Id;
            }

            Insert(dbPizza);
        }
    }
}
