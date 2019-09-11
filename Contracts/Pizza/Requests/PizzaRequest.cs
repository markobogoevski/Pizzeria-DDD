namespace Contracts.Pizza.Requests
{
    using System;
    using System.Collections.Generic;
    using Contracts.Pizza.Models;

    public class PizzaRequest
    {
        public PizzaRequest()
        {
           
        }

        public string Name { get; set; }

        public int Size { get; set; }

        public DateTime Date { get; set; }

        public IEnumerable<IngredientDto> Ingredients { get; set; }
    }
}
