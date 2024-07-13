using DocuEye.Infrastructure.Persistence.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.Infrastructure.MongoDB
{
    /// <summary>
    /// Generic collection of mongodb documents
    /// </summary>
    /// <typeparam name="T">Type of documents in collection</typeparam>
    public class GenericCollection<T> : IGenericCollection<T> where T : BaseEntity
    {
        private IMongoCollection<T> collection;
        //private ILogger logger;

        //public IMongoCollection<T> Collection { get { return collection; } }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="collection">Collection of elements</param>
        public GenericCollection(IMongoCollection<T> collection)
        {
            this.collection = collection;
        }
        /// <summary>
        /// Inserts one item to collection
        /// </summary>
        /// <param name="item">item</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        public async Task InsertOneAsync(T item, CancellationToken cancellationToken = default)
        {
            await this.collection.InsertOneAsync(item, null, cancellationToken);
        }
        /// <summary>
        /// Inserts many items to collection
        /// </summary>
        /// <param name="items">items to insert</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        public async Task InsertManyAsync(IEnumerable<T> items, CancellationToken cancellationToken = default)
        {
            await this.collection.InsertManyAsync(items, null, cancellationToken);
        }
        /// <summary>
        /// Replaces item in collection using Id property to find existing item
        /// </summary>
        /// <param name="item">Item to replace</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        public async Task ReplaceOneAsync(T item, CancellationToken cancellationToken = default)
        {
            await this.collection.ReplaceOneAsync(o => o.Id == item.Id, item, cancellationToken: cancellationToken);
        }
        /// <summary>
        /// Replaces item in collection using filter to find existing item
        /// </summary>
        /// <param name="filter">Filter to find existing item</param>
        /// <param name="item">Item to replace</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        public async Task ReplaceOneAsync(Expression<Func<T, bool>> filter, T item, CancellationToken cancellationToken = default)
        {
            await this.collection.ReplaceOneAsync(filter, item, cancellationToken: cancellationToken);
        }
        /// <summary>
        /// Inserts new item or replaces existing item using Id property
        /// </summary>
        /// <param name="item">Item to replace or insert</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        public async Task UpsertOneAsync(T item, CancellationToken cancellationToken = default)
        {
            var replaceOptions = new ReplaceOptions { IsUpsert = true };
            await this.collection.ReplaceOneAsync(o => o.Id == item.Id, item, replaceOptions, cancellationToken);
        }
        /// <summary>
        /// Inserts new item or replaces existing item using filter
        /// </summary>
        /// <param name="filter">Filter to find existing item</param>
        /// <param name="item">Item to replace or insert</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        public async Task UpsertOneAsync(Expression<Func<T, bool>> filter, T item, CancellationToken cancellationToken = default)
        {
            var replaceOptions = new ReplaceOptions { IsUpsert = true };
            await this.collection.ReplaceOneAsync(filter, item, replaceOptions, cancellationToken);
        }
        /// <summary>
        /// Removes item from collection using filter
        /// </summary>
        /// <param name="filter">Filter for finding existing item</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        public async Task DeleteOneAsync(Expression<Func<T,bool>> filter, CancellationToken cancellationToken = default)
        {
            await this.collection.DeleteOneAsync<T>(filter, cancellationToken);
        }
        /// <summary>
        /// Removes many elements from collection
        /// </summary>
        /// <param name="filter">Filter for finding items to remove</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        public async Task DeleteManyAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
        {
            await this.collection.DeleteManyAsync<T>(filter, cancellationToken);
        }
        /// <summary>
        /// Finds one item in collection
        /// </summary>
        /// <param name="filter">Filter for finding item</param>
        /// <returns>Item that was found</returns>
        public async Task<T> FindOne(Expression<Func<T, bool>> filter)
        {
            return await this.collection.Find<T>(filter).FirstOrDefaultAsync();
        }
        /// <summary>
        /// Finds many items in collection 
        /// </summary>
        /// <param name="filter">Filter for finding items</param>
        /// <returns>List of founded items</returns>
        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> filter)
        {
            return await this.collection.Find<T>(filter).ToListAsync();
        }
        /// <summary>
        /// Finds many items in collection 
        /// </summary>
        /// <param name="filter">Filter for finding items</param>
        /// <param name="sortValueSelector">Filed selector for ordering items</param>
        /// <param name="ascendingSort">Sort direction</param>
        /// <param name="limit">Maximum number of items that will be returned</param>
        /// <param name="skip">Number of items to skip</param>
        /// <returns>List of founded items</returns>
        public async Task<IEnumerable<T>> Find(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sortValueSelector, bool ascendingSort = true, int? limit = null, int? skip = null)
        {
            var query = this.collection.Find<T>(filter);
            if(ascendingSort)
            {
                query.SortBy(sortValueSelector);
            }else
            {
                query.SortByDescending(sortValueSelector);
            }
            if(limit != null)
            {
                query.Limit(limit);
            }
            if(skip != null)
            {
                query.Skip(skip);
            }
            return await query.ToListAsync();
        }
    }
}
