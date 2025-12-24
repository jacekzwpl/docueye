using System.Linq.Expressions;

namespace DocuEye.Structurizr.DSL.Expressions.Builders
{
    public static class ExpressionBuilder
    {
        public static Expression<Func<T, bool>> New<T>()
        {
            return x => true;
        }

        public static Expression<Func<T, bool>> New<T>(Expression<Func<T, bool>> expression)
        {
            return expression;
        }

        public static Expression<Func<T, bool>> And<T>(
            this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right)
        {
            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(
                    left.Body,
                    Expression.Invoke(right, left.Parameters[0])), left.Parameters[0]);
        }

        public static Expression<Func<T, bool>> Or<T>(
            this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right)
        {
            return Expression.Lambda<Func<T, bool>>(
                Expression.OrElse(
                    left.Body,
                    Expression.Invoke(right, left.Parameters[0])), left.Parameters[0]);
        }
    }
}
