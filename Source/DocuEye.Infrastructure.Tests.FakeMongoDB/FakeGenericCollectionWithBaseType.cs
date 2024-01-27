using DocuEye.Infrastructure.MongoDB;
using DocuEye.Infrastructure.Persistence.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using MongoDB.Driver.Linq;

namespace DocuEye.Infrastructure.Tests.FakeMongoDB
{
    public class FakeGenericCollectionWithBaseType<T, B> : IGenericCollection<T>
        where T : B
        where B : BaseEntity
    {

        private List<B> collection;

        public FakeGenericCollectionWithBaseType(List<B> collection) { 
            this.collection = collection;
        }

        public Task InsertOneAsync(T item, CancellationToken cancellationToken = default)
        {
            this.collection.Add(item);
            return Task.CompletedTask;
        }

        public Task InsertManyAsync(IEnumerable<T> items, CancellationToken cancellationToken = default)
        {
            this.collection.AddRange(items);
            return Task.CompletedTask;
        }

        public Task ReplaceOneAsync(T item, CancellationToken cancellationToken = default)
        {
            int index = this.collection.FindIndex(o => o.Id == item.Id);
            if (index == -1)
            {
                return Task.CompletedTask;
            }
            this.collection[index] = item;
            return Task.CompletedTask;
        }

        public Task ReplaceOneAsync(Expression<Func<T, bool>> filter, T item, CancellationToken cancellationToken = default)
        {
            Func<T, bool> func = filter.Compile();
            Predicate<T> pred = t => func(t);
            int index = this.collection.OfType<T>().ToList().FindIndex(pred);
            if (index == -1)
            {
                return Task.CompletedTask;
            }
            this.collection[index] = item;
            return Task.CompletedTask;
        }

        public Task DeleteOneAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
        {
            Func<T, bool> func = filter.Compile();
            Predicate<T> pred = t => func(t);
            int index = this.collection.OfType<T>().ToList().FindIndex(pred);
            if (index == -1)
            {
                return Task.CompletedTask;
            }
            this.collection.RemoveAt(index);
            return Task.CompletedTask;
        }

        public Task DeleteManyAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default)
        {
            Func<T, bool> func = filter.Compile();
            Predicate<T> pred = t => func(t);
            this.collection.OfType<T>().ToList().RemoveAll(pred);
            return Task.CompletedTask;
        }

        public Task<T> FindOne(Expression<Func<T, bool>> filter)
        {
            var result = this.collection.OfType<T>().FirstOrDefault<T>(filter.Compile());
            return Task.FromResult(result);
        }

        public Task<IEnumerable<T>> Find(Expression<Func<T, bool>> filter)
        {
            Func<T, bool> func = filter.Compile();
            Predicate<T> pred = t => func(t);
            var result = this.collection.OfType<T>().ToList().FindAll(pred);
            return Task.FromResult(result.AsEnumerable<T>());
        }

        public Task<IEnumerable<T>> Find(Expression<Func<T, bool>> filter, Expression<Func<T, object>> sortValueSelector, bool ascendingSort = true, int? limit = null, int? skip = null)
        {

            var query = this.collection.OfType<T>().ToList().Where<T>(filter.Compile());


            IOrderedEnumerable<T> orderedQuery;

            if(ascendingSort)
            {
                orderedQuery = query.OrderBy<T, object>(sortValueSelector.Compile());
            }
            else
            {
                orderedQuery = query.OrderByDescending<T, object>(sortValueSelector.Compile());
            }
            if (skip != null)
            {
                orderedQuery = orderedQuery.Skip(skip.Value).OrderBy(o => 1);
            }

            if (limit != null)
            {
                orderedQuery = orderedQuery.Take(limit.Value).OrderBy(o => 1);
            }

            var result = orderedQuery.ToList();
            return Task.FromResult<IEnumerable<T>>(result.AsEnumerable<T>());

        }

        public Task UpsertOneAsync(T item, CancellationToken cancellationToken = default)
        {
            int index = this.collection.FindIndex(o => o.Id == item.Id);
            if (index == -1)
            {
                this.collection.Add(item);
                return Task.CompletedTask;
            }
            this.collection[index] = item;
            return Task.CompletedTask;
        }

        public Task UpsertOneAsync(Expression<Func<T, bool>> filter, T item, CancellationToken cancellationToken = default)
        {
            Func<T, bool> func = filter.Compile();
            Predicate<T> pred = t => func(t);
            int index = this.collection.OfType<T>().ToList().FindIndex(pred);
            if (index == -1)
            {
                this.collection.Add(item);
                return Task.CompletedTask;
            }
            this.collection[index] = item;
            return Task.CompletedTask;
        }
    }
}
