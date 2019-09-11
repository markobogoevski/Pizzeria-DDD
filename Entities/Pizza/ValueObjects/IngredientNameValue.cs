namespace Entities.Pizza.ValueObjects
{
    using Contracts.Common.ErrorCodes;
    using Contracts.Result;
    using System.Collections.Generic;

    public class IngredientNameValue : ValueObject
    {
        private const int MAX_NAME_LENGTH = 60;

        public static readonly IngredientNameValue Empty = new IngredientNameValue(null);

        public string Value { get; }

        private IngredientNameValue(string value)
        {
            Value = value;
        }

        public static Result<IngredientNameValue> Create(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Result.Invalid<IngredientNameValue>(ResultErrorCodes.INGREDIENT_INVALID_NAME);
            }

            value = value.Trim();

            if (value.Length > MAX_NAME_LENGTH)
            {
                return Result.Invalid<IngredientNameValue>(ResultErrorCodes.INGREDIENT_INVALID_NAME_LENGTH);
            }

            return Result.Ok(new IngredientNameValue(value));
        }

        public static implicit operator string(IngredientNameValue obj)
        {
            return obj.Value;
        }

        public static explicit operator IngredientNameValue(string value)
        {
            return Of(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        private static IngredientNameValue Of(string value)
        {
            return Create(value).Value;
        }
    }
}
