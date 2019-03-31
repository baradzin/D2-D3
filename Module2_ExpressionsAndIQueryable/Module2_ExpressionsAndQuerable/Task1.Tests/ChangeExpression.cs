using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Task1.Tests
{
    [TestClass]
    public class ChangeExpressionTest
    {
        //a => ((a - 1) + (((a + 1) * (a + 5)) * (a + 1))) 130
        //a => (Decrement(a) + ((Increment(a) * (a + 5)) * Increment(a))) 130
        [TestMethod]
        public void SubTask1_Test()
        {
            Expression<Func<int, int>> source_exp = (a) => a-1 + (a + 1) * (a + 5) * (a + 1);
            var result_exp = new ExpressionTransformerVisitor().VisitAndConvert(source_exp, "");

            Console.WriteLine(source_exp + " " + source_exp.Compile().Invoke(3));
            Console.WriteLine(result_exp + " " + result_exp.Compile().Invoke(3));
        }

        [TestMethod]
        public void ParametersReplacer_Test()
        {
            var args = new Dictionary<string, ConstantExpression>
            {
                ["a"] = Expression.Constant(4),
                ["b"] = Expression.Constant(1),
            };
            Expression<Func<int, int, int, int>> source_exp = (a,b,c) => c + (a - 1) *b + (a + 1) * (a + 5) * (a + 1);
            var result_exp = new ParameterReplacerVisitor(args).VisitAndConvert(source_exp, "");
            Console.WriteLine(source_exp + " " + source_exp.Compile().Invoke(3,3,2));
            Console.WriteLine(result_exp + " " + result_exp.Compile().Invoke(3,3,2));
            Console.WriteLine("Source Expression:");
            new TraceExpressionVisitor().Visit(source_exp);
            Console.WriteLine("Result Expression:");
            new TraceExpressionVisitor().Visit(result_exp);
        }

        //(a, b, c) => ((c + ((a - 1) * b)) + (((a + 1) * (a + 5)) * (a + 1))) 136
        //(a, b, c) => ((c + ((4 - 1) * 1)) + (((4 + 1) * (4 + 5)) * (4 + 1))) 230
        //Source Expression:
        //Lambda - System.Linq.Expressions.Expression`1[System.Func`4[System.Int32, System.Int32, System.Int32, System.Int32]]
        //    Add - System.Linq.Expressions.SimpleBinaryExpression
        //        Add - System.Linq.Expressions.SimpleBinaryExpression
        //            Parameter - System.Linq.Expressions.PrimitiveParameterExpression`1[System.Int32]
        //Multiply - System.Linq.Expressions.SimpleBinaryExpression
        //                Subtract - System.Linq.Expressions.SimpleBinaryExpression
        //                    Parameter - System.Linq.Expressions.PrimitiveParameterExpression`1[System.Int32]
        //Constant - System.Linq.Expressions.ConstantExpression
        //                Parameter - System.Linq.Expressions.PrimitiveParameterExpression`1[System.Int32]
        //Multiply - System.Linq.Expressions.SimpleBinaryExpression
        //            Multiply - System.Linq.Expressions.SimpleBinaryExpression
        //                Add - System.Linq.Expressions.SimpleBinaryExpression
        //                    Parameter - System.Linq.Expressions.PrimitiveParameterExpression`1[System.Int32]
        //Constant - System.Linq.Expressions.ConstantExpression
        //                Add - System.Linq.Expressions.SimpleBinaryExpression
        //                    Parameter - System.Linq.Expressions.PrimitiveParameterExpression`1[System.Int32]
        //Constant - System.Linq.Expressions.ConstantExpression
        //            Add - System.Linq.Expressions.SimpleBinaryExpression
        //                Parameter - System.Linq.Expressions.PrimitiveParameterExpression`1[System.Int32]
        //Constant - System.Linq.Expressions.ConstantExpression
        //    Parameter - System.Linq.Expressions.PrimitiveParameterExpression`1[System.Int32]
        //Parameter - System.Linq.Expressions.PrimitiveParameterExpression`1[System.Int32]
        //Parameter - System.Linq.Expressions.PrimitiveParameterExpression`1[System.Int32]
        //Result Expression:
        //Lambda - System.Linq.Expressions.Expression`1[System.Func`4[System.Int32, System.Int32, System.Int32, System.Int32]]
        //    Add - System.Linq.Expressions.SimpleBinaryExpression
        //        Add - System.Linq.Expressions.SimpleBinaryExpression
        //            Parameter - System.Linq.Expressions.PrimitiveParameterExpression`1[System.Int32]
        //Multiply - System.Linq.Expressions.SimpleBinaryExpression
        //                Subtract - System.Linq.Expressions.SimpleBinaryExpression
        //                    Constant - System.Linq.Expressions.ConstantExpression
        //                    Constant - System.Linq.Expressions.ConstantExpression
        //                Constant - System.Linq.Expressions.ConstantExpression
        //        Multiply - System.Linq.Expressions.SimpleBinaryExpression
        //            Multiply - System.Linq.Expressions.SimpleBinaryExpression
        //                Add - System.Linq.Expressions.SimpleBinaryExpression
        //                    Constant - System.Linq.Expressions.ConstantExpression
        //                    Constant - System.Linq.Expressions.ConstantExpression
        //                Add - System.Linq.Expressions.SimpleBinaryExpression
        //                    Constant - System.Linq.Expressions.ConstantExpression
        //                    Constant - System.Linq.Expressions.ConstantExpression
        //            Add - System.Linq.Expressions.SimpleBinaryExpression
        //                Constant - System.Linq.Expressions.ConstantExpression
        //                Constant - System.Linq.Expressions.ConstantExpression
        //    Parameter - System.Linq.Expressions.PrimitiveParameterExpression`1[System.Int32]
        //Parameter - System.Linq.Expressions.PrimitiveParameterExpression`1[System.Int32]
        //Parameter - System.Linq.Expressions.PrimitiveParameterExpression`1[System.Int32]

    }
}

