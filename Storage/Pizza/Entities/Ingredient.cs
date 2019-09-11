namespace Storage.Pizza.Entities
{
    using Pizzeria.Storage.Common.Entities;

    public class Ingredient : BaseEntity
    {
        protected internal Ingredient() { }

        /// <summary>
        /// Name of ingredient
        /// </summary>
        public string Name { get; protected internal set; }

        /// <summary>
        /// Quantity of ingredient
        /// </summary>
        public int Quantity { get; protected internal set; }

        /// <summary>
        /// Pizza Fk
        /// </summary>
        public int PizzaFk { get; protected internal set; }

        /// <summary>
        /// Parent Pizza pointed by PizzaFk
        /// </summary>
        public Pizza Pizza { get; protected internal set; }
    }
}
