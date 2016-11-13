using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Repository.Pattern.Ef6
{
    public class SelectorClause<TEntity>
    {
        private readonly List<Expression<Func<TEntity, object>>> _lstInclude;
        private Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> _orderBy;
        private readonly List<string> _lstStrInclude;
        private Expression<Func<TEntity, dynamic>> _selector;

        public SelectorClause()
        {
            _lstInclude = new List<Expression<Func<TEntity, object>>>();
            _lstStrInclude = new List<string>();
            _orderBy = null;
            _selector = null;
        }

        public void Include(Expression<Func<TEntity, object>> expression)
        {
            _lstInclude.Add(expression);
        }

        public void Include(string include)
        {
            if (!string.IsNullOrWhiteSpace(include))
                _lstStrInclude.Add(include);
        }

        public void Include(List<string> include)
        {
            _lstStrInclude.AddRange(include);
        }

        public void UnInclude(string include)
        {
            if (!string.IsNullOrWhiteSpace(include) && _lstStrInclude.Count > 0)
                _lstStrInclude.Remove(include);
        }
        public void RemoveInclude()
        {
            _lstInclude.Clear();
            _lstStrInclude.Clear();
        }

        public List<Expression<Func<TEntity, object>>> LstIncludeExp
        {
            get { return _lstInclude; }
        }
        public List<string> LstIncludeStr
        {
            get { return _lstStrInclude; }
        }

        public void setOrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy)
        {
            _orderBy = orderBy;
        }

        public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> GetOrderBy
        {
            get { return _orderBy; }
        }

        public SelectorClause<TEntity> setSelector(Expression<Func<TEntity, dynamic>> selector)
        {
            _selector = selector;
            return this;
        }

        public Expression<Func<TEntity, dynamic>> GetSelector
        {
            get { return _selector; }
        }
    }
}