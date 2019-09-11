namespace Contracts.Pizza.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Contracts.Filters;
    using Contracts.Pizza.Models;
    using Contracts.Pizza.Requests;
    using Contracts.Result;

    public interface IPizzaService
    {
        /// <summary>
        /// Gets all pizzas from database with filter
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
        /// <param name="request"></param>
        /// <returns></returns>
        Task<Result<PizzaDto>> InsertPizzaAsync(PizzaRequest request);
    }
}
