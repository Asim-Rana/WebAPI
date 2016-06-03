using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ClinicalWebApi2.Filters
{
    public class ExpressionFilter
    {
        public static Expression<Func<T, bool>> CreateContainsExpression<T>(T type, string phone)
        {
            ParameterExpression Param = Expression.Parameter(typeof(T));

            Expression field = Expression.PropertyOrField(Param, "Phone");
            Expression constant = Expression.Constant(phone);
            Expression equal = Expression.Equal(field, constant);
            Expression<Func<T, bool>> e1 =
            Expression.Lambda<Func<T, bool>>(equal, Param);
            return e1;
        }
    }
}