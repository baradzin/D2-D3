using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace Task1
{
    public class ParameterReplacerVisitor : ExpressionVisitor
    {
        public Dictionary<string, ConstantExpression> ParametersDictionary { get; set; }

        public ParameterReplacerVisitor(Dictionary<string, ConstantExpression> dict)
        {
            ParametersDictionary = dict;
        }

        protected override Expression VisitParameter(ParameterExpression node)
            => ParametersDictionary.TryGetValue(node.Name, out var ce) ? (Expression)ce : node;

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            //var parameters = node.Parameters
            //    .Where(p => !ParametersDictionary.ContainsKey(p.Name));

            return Expression.Lambda(Visit(node.Body), node.Parameters);  // don't visit the parameters
        }
       
    }
}
