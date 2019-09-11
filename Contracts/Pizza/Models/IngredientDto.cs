namespace Contracts.Pizza.Models
{
    using Contracts.Common.Models;
    using System.Collections.Generic;

    public class IngredientDto : RestBase
    {
        /// <summary>
        /// The ingredient's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The ingredient's size
        /// </summary>
        public int Quantity { get; set; }
    }
}
