using DocuEye.Infrastructure.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.Infrastructure.MongoDB
{
    /// <summary>
    /// Interface for Generic collection of mongodb documents
    /// </summary>
    /// <typeparam name="T">Type of documents in collection</typeparam>
    public interface IGenericCollection<T> where T : BaseEntity
    {
        /// <summary>
        /// Inserts one item to collection
        /// </summary>
        /// <param name="item">item</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        Task InsertOneAsync(T item, CancellationToken cancellationToken = default);
        /// <summary>
        /// Inserts many items to collection
        /// </summary>
        /// <param name="items">items to insert</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        Task InsertManyAsync(IEnumerable<T> items, CancellationToken cancellationToken = default);
        /// <summary>
        /// Replaces item in collection using Id property to find existing item
        /// </summary>
        /// <param name="item">Item to replace</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        Task ReplaceOneAsync(T item, CancellationToken cancellationToken = default);
        /// <summary>
        /// Replaces item in collection using filter to find existing item
        /// </summary>
        /// <param name="filter">Filter to find existing item</param>
        /// <param name="item">Item to replace</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        Task ReplaceOneAsync(Expression<Func<T, bool>> filter, T item, CancellationToken cancellationToken = default);
        /// <summary>
        /// Inserts new item or replaces existing item using Id property
        /// </summary>
        /// <param name="item">Item to replace or insert</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        Task UpsertOneAsync(T item, CancellationToken cancellationToken = default);
        /// <summary>
        /// Inserts new item or replaces existing item using filter
        /// </summary>
        /// <param name="filter">Filter to find existing item</param>
        /// <param name="item">Item to replace or insert</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        Task UpsertOneAsync(Expression<Func<T, bool>> filter, T item, CancellationToken cancellationToken = default);
        /// <summary>
        /// Removes item from collection using filter
        /// </summary>
        /// <param name="filter">Filter for finding existing item</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        Task DeleteOneAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);
        /// <summary>
        /// Removes many elements from collection
        /// </summary>
        /// <param name="filter">Filter for finding items to remove</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        Task DeleteManyAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);
        /// <summary>
        /// Finds one item in collection
        /// </summary>
        /// <param name="filter">Filter for finding item</param>
        /// <returns>Item that was found</returns>
        Task<T> FindOne(Expression<Func<T, bool>> filter);
        /// <summary>
        /// Finds many items in collection 
        /// </summary>
        /// <param name="filter">Filter for finding items</param>
        /// <returns>List of founded items</returns>
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> filter);
        /// <summary>
        /// Finds many items in collection 
        /// </summary>
        /// <param name="filter">Filter for finding items</param>
        /// <param name="sortValueSelector">Filed selector for ordering items</param>
        /// <param name="ascendingSort">Sort direction</param>
        /// <param name="limit">Maximum number of items that will be returned</param>
        /// <param name="skip">Number of items to skip</param>
        /// <returns>List of founded items</returns>
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sortValueSelector, bool ascendingSort = true, int? limit = null, int? skip = null);
    }
}
