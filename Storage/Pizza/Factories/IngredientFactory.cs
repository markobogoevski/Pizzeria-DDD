namespace Storage.Pizza.Factories
{
    using Contracts.Result;
    using global::Entities.Pizza.Domain;
    using global::Entities.Pizza.ValueObjects;

    public static class IngredientFactory
    {
        // Storage to domain 
        
        public static Result<Ingredient> ToIngredientDomain(this Entities.Ingredient dbIngredient)
        {
            Result<IngredientNameValue> ingredientNameOrError = IngredientNameValue.Create(dbIngredient.Name);
            Result<IngredientQuantityValue> ingredientQuantityOrError = IngredientQuantityValue.Create(dbIngredient.Quantity);

            Result okOrError = Result.FirstFailureOrOk(ingredientNameOrError, ingredientQuantityOrError);
            if (okOrError.IsFailure)
            {
                return Result.FromError<Ingredient>(okOrError);
            }

            return Ingredient.Create(dbIngredient.Id,
                                     dbIngredient.CreatedOn,
                                     dbIngredient.DeletedOn,
                                     ingredientNameOrError.Value,
                                     ingredientQuantityOrError.Value,
                                     dbIngredient.PizzaFk);
        }

        // Domain to storage

        public static Entities.Ingredient ToIngredientStorage(this Ingredient ingredient)
        {
            return new Entities.Ingredient
            {
                CreatedOn = ingredient.CreatedOn,
                DeletedOn = ingredient.DeletedOn,
                Id = ingredient.Id,
                Name = ingredient.Name,
                Quantity = ingredient.Quantity,
                PizzaFk = ingredient.PizzaId
            };
        }
    }
}
