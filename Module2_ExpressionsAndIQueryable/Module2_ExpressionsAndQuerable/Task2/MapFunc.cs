using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public static class MapFunc<TInput, TOutput>
    {
        public static readonly Func<TInput, TOutput> Instance = CreateMapFunc<TInput, TOutput>();

        private static Func<TInput, TOutput> CreateMapFunc<TInput, TOutput>()
        {
            var source = Expression.Parameter(typeof(TInput), "source");
            var body = Expression.MemberInit(Expression.New(typeof(TOutput)),
                source.Type.GetProperties().Select(p => Expression.Bind(typeof(TOutput).GetProperty(p.Name), Expression.Property(source, p))));
            var expr = Expression.Lambda<Func<TInput, TOutput>>(body, source);
            return expr.Compile();
        }

    }
}
