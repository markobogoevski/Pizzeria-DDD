namespace Contracts.Pizza.Models
{
    using Contracts.Common.Models;
    using System.Collections.Generic;

    public class PizzaDto : RestBase
    {
        /// <summary>
        /// The pizza's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The pizza's size
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// The pizza's ingredients
        /// </summary>
        public List<IngredientDto> Ingredients { get; set; }
    }
}
