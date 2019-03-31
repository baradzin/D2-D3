using System;
using System.Linq.Expressions;

namespace Task1
{
    public class ExpressionTransformerVisitor : ExpressionVisitor
    {
        protected override Expression VisitBinary(BinaryExpression node)
        {
            if (node.NodeType == ExpressionType.Add || node.NodeType == ExpressionType.Subtract) {
                
                var tuple = InitBinaryExpressionParams(node);
                var param = tuple.Item1;
                var constant = tuple.Item2;

                if (param != null && constant != null && constant.Type == typeof(int) && (int)constant.Value == 1) {
                    switch (node.NodeType) {
                        case ExpressionType.Add:
                            return Expression.Increment(param);
                        case ExpressionType.Subtract:
                            return Expression.Decrement(param);
                    }
                }
            }

            return base.VisitBinary(node);
        }

        private Tuple<ParameterExpression, ConstantExpression> InitBinaryExpressionParams(BinaryExpression node)
        {
            ParameterExpression param = null;
            ConstantExpression constant = null;
            if (node.Left.NodeType == ExpressionType.Parameter) {
                param = (ParameterExpression)node.Left;
            } else if (node.Left.NodeType == ExpressionType.Constant) {
                constant = (ConstantExpression)node.Left;
            }

            if (node.Right.NodeType == ExpressionType.Parameter) {
                param = (ParameterExpression)node.Right;
            } 

            if (node.Right.NodeType == ExpressionType.Constant) {
                constant = (ConstantExpression)node.Right;
            }

            return Tuple.Create(param, constant);
        }
    }
}
