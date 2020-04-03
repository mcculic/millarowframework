using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Millarow.Reflection
{
    public static class ExpressionHelper
    {
        public static PropertyInfo GetPropertyInfo<T>(this Expression<Func<T>> propertyExpression)
        {
            propertyExpression.AssertNotNull(nameof(propertyExpression));

			if (GetMemberExpression(propertyExpression).Member is PropertyInfo propertyInfo)
				return propertyInfo;

			throw new ArgumentException("Expression member is not a PropertyInfo"); //TODO msg
        }

        public static string GetPropertyName<T>(this Expression<Func<T>> propertyExpression)
        {
            return propertyExpression.GetPropertyInfo().Name;
        }

        public static PropertyInfo GetPropertyInfo<TOwner, TProperty>(this Expression<Func<TOwner, TProperty>> propertyExpression)
        {
            propertyExpression.AssertNotNull(nameof(propertyExpression));

			if (GetMemberExpression(propertyExpression).Member is PropertyInfo propertyInfo)
				return propertyInfo;

			throw new ArgumentException("Expression member is not a PropertyInfo"); //TODO msg
		}

        private static MemberExpression GetMemberExpression(LambdaExpression expression)
        {
            if (expression.Body is MemberExpression)
                return expression.Body as MemberExpression;

            if (expression.Body is UnaryExpression)
            {
                var unaryExpression = expression.Body as UnaryExpression;
                if (unaryExpression.Operand is MemberExpression)
                    return unaryExpression.Operand as MemberExpression;
            }

            throw new ArgumentException(nameof(expression));
        }
    }
}