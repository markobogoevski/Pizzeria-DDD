namespace Entities.Pizza.Domain
{
    using Contracts.Common.ErrorCodes;
    using Contracts.Result;
    using Entities.Common.Entities;
    using Entities.Pizza.ValueObjects;
    using System;
    using System.Collections.Generic;

    public sealed class Pizza : AggregateRoot
    {
        private Pizza() { }

        private readonly List<Ingredient> _ingredients = new List<Ingredient>();

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
        /// The pizza's name
        /// </summary>
        public PizzaNameValue Name { get; private set; }

        /// <summary>
        /// The pizza's size (radius)
        /// </summary>
        public PizzaSizeValue Size { get; private set; }

        /// <summary>
        /// The pizza's ingredients
        /// </summary>
        public IReadOnlyList<Ingredient> Ingredients => _ingredients;

        /// <summary>
        /// This method should be used for creating domain object from DB
        /// </summary>
        /// <returns></returns>

        public static Result<Pizza> Create(int id,
                                           Guid uid,
                                           DateTime createdOn,
                                           DateTime? deletedOn,
                                           PizzaNameValue pizzaName,
                                           PizzaSizeValue pizzaSize,
                                           ICollection<Ingredient> ingredients)
        {
            if (uid == default(Guid))
            {
                return Result.Invalid<Pizza>(ResultErrorCodes.PIZZA_INVALID_UID);
            }

            if (createdOn == default(DateTime))
            {
                return Result.Invalid<Pizza>(ResultErrorCodes.PIZZA_INVALID_CREATED_ON);
            }

            var pizza =  new Pizza
            {
                CreatedOn = createdOn,
                DeletedOn = deletedOn,
                Id = id,
                Uid = uid,
                Name = pizzaName,
                Size = pizzaSize
            };

            pizza._ingredients.AddRange(ingredients);
            return Result.Ok(pizza);
        }

        public static Result<Pizza> Create(Guid uid,
                                           DateTime createdOn,
                                           string pizzaName,
                                           int pizzaSize,
                                           ICollection<Ingredient> ingredients)
        {
            if (uid == default(Guid))
            {
                return Result.Invalid<Pizza>(ResultErrorCodes.PIZZA_INVALID_UID);
            }

            if (createdOn == default(DateTime))
            {
                return Result.Invalid<Pizza>(ResultErrorCodes.PIZZA_INVALID_CREATED_ON);
            }

            Result<PizzaNameValue> pizzaNameOrError = PizzaNameValue.Create(pizzaName);
            Result<PizzaSizeValue> pizzaSizeOrError = PizzaSizeValue.Create(pizzaSize);

            Result okOrError = Result.FirstFailureOrOk(pizzaNameOrError, pizzaSizeOrError);
            if (okOrError.IsFailure)
            {
                return Result.FromError<Pizza>(okOrError);
            }

            var pizza = new Pizza
            {
               CreatedOn = createdOn,
               Uid = uid,
               Name = pizzaNameOrError.Value,
               Size = pizzaSizeOrError.Value
            };

            pizza._ingredients.AddRange(ingredients);
            return Result.Ok(pizza);
        }

        public Result UpdateName(PizzaNameValue newPizzaName)
        {

            Result resultOrError = CheckEditable();

            if (resultOrError.IsFailure)
            {
                return resultOrError;
            }

            if (newPizzaName == null)
            {
                return Result.Invalid(ResultErrorCodes.PIZZA_INVALID_NAME);
            }

            Name = newPizzaName;

            MarkModify();

            return Result.Ok();
        }

        public Result UpdateSize(PizzaSizeValue newPizzaSize)
        {
            Result resultOrError = CheckEditable();

            if (resultOrError.IsFailure)
            {
                return resultOrError;
            }

            if(newPizzaSize == default(int))
            {
                return Result.Invalid(ResultErrorCodes.PIZZA_INVALID_SIZE);
            }

            Size = newPizzaSize;

            MarkModify();

            return Result.Ok();
        }
    }
}
