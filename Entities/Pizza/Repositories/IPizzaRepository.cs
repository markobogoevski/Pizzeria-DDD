namespace Entities.Pizza.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contracts.Filters;
    using Contracts.Pizza.Models;
    using Contracts.Pizza.Requests;
    using Contracts.Result;
    using Entities.Pizza.Domain;

    public interface IPizzaRepository
    {
        /// <summary>
        /// Gets all pizzas from database
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<Result<IReadOnlyList<PizzaDto>>> GetPizzasAsync(FilterBySkipTakeRequest filter);

        /// <summary>
        /// Gets pizza for uid from database
        /// </summary>
        /// <param name="pizzaUid"></param>
        /// <returns></returns>
        Task<Result<PizzaDto>> GetPizzaAsync(Guid pizzaUid);

        /// <summary>
        /// Inserts a new pizza in database
        /// </summary>
        /// <param name="pizza"></param>
        void Insert(Pizza pizza);

        /// <summary>
        /// Creates a pizza from request
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Result<Pizza> CreatePizza(PizzaRequest request);
    }
}
