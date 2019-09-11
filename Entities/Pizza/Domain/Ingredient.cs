namespace Entities.Pizza.Domain
{
    using Contracts.Common.ErrorCodes;
    using Contracts.Result;
    using Entities.Common.Entities;
    using Entities.Pizza.ValueObjects;
    using System;

    public sealed class Ingredient : BaseEntity
    {
        private Ingredient() { }

        public int PizzaId { get; private set; }

        private Result CheckEditable()
        {
            if (DeletedOn == null)
            {
                return Result.Ok();
            }
            else
            {
                return Result.Forbidden(ResultErrorCodes.PIZZA_FORBIDDEN_EDIT);
            }
        }

        /// <summary>
        /// The Ingredient's name
        /// </summary>
        public IngredientNameValue Name { get; private set; }

        /// <summary>
        /// The Ingredient's quantity (radius)
        /// </summary>
        public IngredientQuantityValue Quantity { get; private set; }

        /// <summary>
        /// This method should be used for creating domain object from DB
        /// </summary>
        /// <returns></returns>

        public static Result<Ingredient> Create(int id,
                                                DateTime createdOn,
                                                DateTime? deletedOn,
                                                IngredientNameValue ingredientName,
                                                IngredientQuantityValue ingredientQuantity,
                                                int pizzaFk)
        {
            if (createdOn == default(DateTime))
            {
                return Result.Invalid<Ingredient>(ResultErrorCodes.INGREDIENT_INVALID_CREATED_ON);
            }

            return Result.Ok(new Ingredient
            {
                CreatedOn = createdOn,
                DeletedOn = deletedOn,
                Id = id,
                Name = ingredientName,
                Quantity = ingredientQuantity,
                PizzaId = pizzaFk
            });
        }


        /// <summary>
        /// Creates an ingredient 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="createdOn"></param>
        /// <param name="ingredientName"></param>
        /// <param name="ingredientQuantity"></param>
        /// <returns></returns>
        public static Result<Ingredient> Create(int id,
                                                DateTime createdOn,
                                                string ingredientName,
                                                int ingredientQuantity)
        {
            if (createdOn == default(DateTime))
            {
                return Result.Invalid<Ingredient>(ResultErrorCodes.INGREDIENT_INVALID_CREATED_ON);
            }

            Result<IngredientNameValue> ingredientNameOrError = IngredientNameValue.Create(ingredientName);
            Result<IngredientQuantityValue> ingredientQuantityOrError = IngredientQuantityValue.Create(ingredientQuantity);

            Result okOrError = Result.FirstFailureOrOk(ingredientNameOrError, ingredientQuantityOrError);
            if (okOrError.IsFailure)
            {
                return Result.FromError<Ingredient>(okOrError);
            }

            return Result.Ok(new Ingredient
            {
                CreatedOn = createdOn,
                Id = id,
                Name = ingredientNameOrError.Value,
                Quantity = ingredientQuantityOrError.Value,
            });
        }

        public Result UpdateName(IngredientNameValue newIngredientName)
        {

            Result resultOrError = CheckEditable();

            if (resultOrError.IsFailure)
            {
                return resultOrError;
            }

            if (newIngredientName == null)
            {
                return Result.Invalid(ResultErrorCodes.INGREDIENT_INVALID_NAME);
            }

            Name = newIngredientName;

            MarkModify();

            return Result.Ok();
        }

        public Result UpdateQuantity(IngredientQuantityValue newIngredientQuantity)
        {
            Result resultOrError = CheckEditable();

            if (resultOrError.IsFailure)
            {
                return resultOrError;
            }

            if (newIngredientQuantity == default(int))
            {
                return Result.Invalid(ResultErrorCodes.INGREDIENT_INVALID_QUANTITY);
            }

            Quantity = newIngredientQuantity;

            MarkModify();

            return Result.Ok();
        }
    }
}
