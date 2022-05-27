using System;
using System.Collections.Generic;

namespace Assimalign.Azure.WebJobs.Bindings.Storage.Utilities
{
    internal static class Cacher<TIn, TOut>
    {

        private readonly static IDictionary<TIn, TOut> cache = new Dictionary<TIn, TOut>();


        /// <summary>
        /// Memorizes a request
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="method"></param>
        /// <returns></returns>
        public static Func<TIn, TOut> Memoize(Func<TIn, TOut> method)
        {
            //var cache = new Dictionary<TIn, TOut>();
            return input => cache.TryGetValue(input, out var results) ?
                 results :
                 cache[input] = method(input);
        }
    }
}
