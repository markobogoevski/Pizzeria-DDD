namespace Entities.Pizza.ValueObjects
{
    using Contracts.Common.ErrorCodes;
    using Contracts.Result;
    using System.Collections.Generic;

    public class PizzaNameValue : ValueObject
    {
        private const int MAX_NAME_LENGTH = 60;

        public static readonly PizzaNameValue Empty = new PizzaNameValue(null);

        public string Value { get; }

        private PizzaNameValue(string value)
        {
            Value = value;
        }

        public static Result<PizzaNameValue> Create(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return Result.Invalid<PizzaNameValue>(ResultErrorCodes.PIZZA_INVALID_NAME);
            }

            value = value.Trim();

            if (value.Length > MAX_NAME_LENGTH)
            {
                return Result.Invalid<PizzaNameValue>(ResultErrorCodes.PIZZA_INVALID_NAME_LENGTH);
            }

            return Result.Ok(new PizzaNameValue(value));
        }

        public static implicit operator string(PizzaNameValue obj)
        {
            return obj.Value;
        }

        public static explicit operator PizzaNameValue(string value)
        {
            return Of(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        private static PizzaNameValue Of(string value)
        {
            return Create(value).Value;
        }
    }
}
