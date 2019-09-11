namespace Contracts.Common.Interfaces
{
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        /// <summary>
        /// Saves state of repositories subscribed to the unit
        /// </summary>
        /// <returns></returns>
        Task<int> SaveAsync();
    }
}
