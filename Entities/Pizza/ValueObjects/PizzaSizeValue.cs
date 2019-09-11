namespace Entities.Pizza.ValueObjects
{
    using Contracts.Common.ErrorCodes;
    using Contracts.Result;
    using System.Collections.Generic;

    public sealed class PizzaSizeValue : ValueObject
    {
        private const int MAX_SIZE = 40;

        public static readonly PizzaSizeValue Empty = new PizzaSizeValue(default(int));

        public int Value { get; }

        private PizzaSizeValue(int value)
        {
            Value = value;
        }

        public static Result<PizzaSizeValue> Create(int value)
        {
            if (value == default(int))
            {
                return Result.Invalid<PizzaSizeValue>(ResultErrorCodes.PIZZA_INVALID_SIZE);
            }

            if (value > MAX_SIZE)
            {
                return Result.Invalid<PizzaSizeValue>(ResultErrorCodes.PIZZA_INVALID_SIZE);
            }

            return Result.Ok(new PizzaSizeValue(value));
        }

        public static implicit operator int(PizzaSizeValue obj)
        {
            return obj.Value;
        }

        public static explicit operator PizzaSizeValue(int value)
        {
            return Of(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        private static PizzaSizeValue Of(int value)
        {
            return Create(value).Value;
        }
    }
}
