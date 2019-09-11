namespace Storage.Pizza.Entities
{
    using Pizzeria.Storage.Common.Entities;
    using System.Collections.Generic;

    public class Pizza : AggregateRoot
    {
        protected internal Pizza() { }

        /// <summary>
        /// The pizza name
        /// </summary>
        public string Name { get; protected internal set; }

        /// <summary>
        /// Pizza size (radius)
        /// </summary>
        public int Size { get; protected internal set; }

        /// <summary>
        /// The pizza's ingredients
        /// </summary>
        public virtual ICollection<Ingredient> Ingredients { get; protected internal set; }
    }
}
