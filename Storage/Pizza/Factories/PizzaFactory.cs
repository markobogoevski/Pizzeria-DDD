namespace Storage.Pizza.Factories
{
    using Contracts.Pizza.Models;
    using Contracts.Result;
    using global::Entities.Pizza.Domain;
    using global::Entities.Pizza.ValueObjects;
    using System.Collections.Generic;
    using System.Linq;

    public static class PizzaFactory
    {
        // To pizza dto from domain
        public static PizzaDto ToPizzaDto(this Pizza pizza)
        {
            return new PizzaDto
            {
                Id = pizza.Id,
                Name = pizza.Name,
                Size = pizza.Size,
                Uid = pizza.Uid,
                Ingredients = pizza.Ingredients.Select(ingredient => new IngredientDto
                {
                    Id = ingredient.Id,
                    Name = ingredient.Name,
                    Quantity = ingredient.Quantity
                }).ToList()
            };
        }

        // To pizza dto from storage
        public static PizzaDto ToPizzaDto(this Entities.Pizza dbPizza)
        {
            return new PizzaDto
            {
                Id = dbPizza.Id,
                Name = dbPizza.Name,
                Size = dbPizza.Size,
                Uid = dbPizza.Uid,
                Ingredients = dbPizza.Ingredients.Select(ingredient => new IngredientDto
                {
                    Id = ingredient.Id,
                    Name = ingredient.Name,
                    Quantity = ingredient.Quantity
                }).ToList()
            };
        }

        // Storage to domain
        public static Result<Pizza> ToPizzaDomain(this Entities.Pizza dbPizza)
        {
            Result<PizzaNameValue> pizzaNameOrError = PizzaNameValue.Create(dbPizza.Name);
            Result<PizzaSizeValue> pizzaSizeOrError = PizzaSizeValue.Create(dbPizza.Size);

            Result okOrError = Result.FirstFailureOrOk(pizzaNameOrError, pizzaSizeOrError);
            if (okOrError.IsFailure)
            {
                return Result.FromError<Pizza>(okOrError);
            }

            ICollection<Result<Ingredient>> ingredientResults =
                dbPizza.Ingredients
                                .Where(ingredient => ingredient.DeletedOn == null)
                                .Select(ingredient => ingredient.ToIngredientDomain()).ToList();

            Result ingredientsOrError = Result.FirstFailureOrOk(ingredientResults);
            if (ingredientsOrError.IsFailure)
            {
                return Result.FromError<Pizza>(ingredientsOrError);
            }

            return Pizza.Create(dbPizza.Id,
                                dbPizza.Uid,
                                dbPizza.CreatedOn,
                                dbPizza.DeletedOn,
                                pizzaNameOrError.Value,
                                pizzaSizeOrError.Value,
                                ingredientResults.Select(ingredient => ingredient.Value).ToList());
        }

        //Domain to storage
        public static Entities.Pizza ToPizzaStorage(this Pizza pizza)
        {
            return new Entities.Pizza
            {
                CreatedOn = pizza.CreatedOn,
                DeletedOn = pizza.DeletedOn,
                Id = pizza.Id,
                Ingredients = pizza.Ingredients.Select(x => x.ToIngredientStorage()).ToList(),
                Name = pizza.Name,
                Size = pizza.Size,
                Uid = pizza.Uid
            };
        }
    }
}
