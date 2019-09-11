namespace Entities.Pizza.ValueObjects
{
    using Contracts.Common.ErrorCodes;
    using Contracts.Result;
    using System.Collections.Generic;

    public sealed class IngredientQuantityValue : ValueObject
    {
        private const int MAX_QUANTITY = 40;

        public static readonly IngredientQuantityValue Empty = new IngredientQuantityValue(default(int));

        public int Value { get; }

        private IngredientQuantityValue(int value)
        {
            Value = value;
        }

        public static Result<IngredientQuantityValue> Create(int value)
        {
            if (value == default(int))
            {
                return Result.Invalid<IngredientQuantityValue>(ResultErrorCodes.INGREDIENT_INVALID_QUANTITY);
            }

            if (value > MAX_QUANTITY)
            {
                return Result.Invalid<IngredientQuantityValue>(ResultErrorCodes.INGREDIENT_INVALID_QUANTITY);
            }

            return Result.Ok(new IngredientQuantityValue(value));
        }

        public static implicit operator int(IngredientQuantityValue obj)
        {
            return obj.Value;
        }

        public static explicit operator IngredientQuantityValue(int value)
        {
            return Of(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        private static IngredientQuantityValue Of(int value)
        {
            return Create(value).Value;
        }
    }
}
