using Repository.Pattern.Infrastructure;
using Repository.Pattern.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Repository.Pattern.Ef6
{
    public sealed class QueryFluent<TEntity> : IQueryFluent<TEntity> where TEntity : class, IObjectState
    {
        #region Private Fields

        private readonly Expression<Func<TEntity, bool>> _expression;
        private readonly List<Expression<Func<TEntity, object>>> _includes;
        private readonly List<string> _lstStrIncludes;
        private readonly Repository<TEntity> _repository;
        private Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> _orderBy;

        #endregion Private Fields

        #region Constructors

        public QueryFluent(Repository<TEntity> repository)
        {
            _repository = repository;
            _includes = new List<Expression<Func<TEntity, object>>>();
            _lstStrIncludes = new List<string>();
        }

        public QueryFluent(Repository<TEntity> repository, IQueryObject<TEntity> queryObject) : this(repository)
        {
            _expression = queryObject.Query();
        }

        public QueryFluent(Repository<TEntity> repository, Expression<Func<TEntity, bool>> expression) : this(repository)
        {
            _expression = expression;
        }

        #endregion Constructors

        public IQueryFluent<TEntity> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            _orderBy = orderBy;
            return this;
        }

        public IQueryFluent<TEntity> Include(Expression<Func<TEntity, object>> expression)
        {
            _includes.Add(expression);
            return this;
        }

        public IQueryFluent<TEntity> Include(string incude)
        {
            if (string.IsNullOrWhiteSpace(incude))
                _lstStrIncludes.Add(incude);
            return this;
        }

        public IQueryFluent<TEntity> Include(List<string> lstinclude)
        {
            if (lstinclude != null && lstinclude.Count > 0)
                _lstStrIncludes.AddRange(lstinclude);
            return this;
        }

        public IEnumerable<TEntity> SelectPage(int page, int pageSize, out int totalCount)
        {
            totalCount = _repository.Select(_expression).Count();
            int pageCount = totalCount / pageSize;
            if (totalCount % pageSize > 0) pageCount++;
            if (page > pageCount && pageCount > 0)
                page = pageCount;
            return _repository.Select(_expression, _orderBy, _includes, _lstStrIncludes, page, pageSize);
        }

        public IQueryable<TEntity> SelectPageQ(int page, int pageSize, out int totalCount)
        {
            totalCount = _repository.Select(_expression).Count();
            return _repository.Select(_expression, _orderBy, _includes, _lstStrIncludes, page, pageSize);
        }

        public IEnumerable<TEntity> Select()
        {
            return _repository.Select(_expression, _orderBy, _includes, _lstStrIncludes);
        }

        public IQueryable<TEntity> SelectQ()
        {
            return _repository.Select(_expression, _orderBy, _includes, _lstStrIncludes);
        }

        public IEnumerable<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            return _repository.Select(_expression, _orderBy, _includes, _lstStrIncludes).Select(selector);
        }

        public IQueryable<TResult> SelectQ<TResult>(Expression<Func<TEntity, TResult>> selector)
        {
            return _repository.Select(_expression, _orderBy, _includes, _lstStrIncludes).Select(selector);
        }

        public async Task<IEnumerable<TEntity>> SelectAsync()
        {
            return await _repository.SelectAsync(_expression, _orderBy, _includes, _lstStrIncludes);
        }

        public IQueryable<TEntity> SqlQuery(string query, params object[] parameters)
        {
            return _repository.SelectQuery(query, parameters).AsQueryable();
        }

        public int Count()
        {
            return _repository.Select(_expression).Count();
        }
    }
}