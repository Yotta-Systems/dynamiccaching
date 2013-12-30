using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yotta.Caching;

namespace TestCacher
{
    class Program
    {
        
        public static double? DoCalculation(dynamic args)
        {
            if (args.i * args.p == 4)
                return null;
            return args.i * args.p;
        }

        public static string ThisIsATest(dynamic args)
        {
            return "This is a test " + args.message;
        }

        static void Main(string[] args)
        {
            BaseCacher cacher = new MemoryCache();

            var result = cacher.Invoke<double?>(new Function<double?>(DoCalculation), new { i = 1, p = 2.0 });
            Console.WriteLine(result);

            result = cacher.Invoke<double?>(new Function<double?>(DoCalculation), new { i = 1, p = 2.0 });
            Console.WriteLine(result);

            result = cacher.Invoke<double?>(new Function<double?>(DoCalculation), new { i = 2, p = 2.0 });
            Console.WriteLine(result);

            result = cacher.Invoke<double?>(new Function<double?>(DoCalculation), new { i = 1, p = 2.0 });
            Console.WriteLine(result);

            Console.WriteLine(cacher.Invoke<string>(new Function<string>(ThisIsATest), new { message = "Hallo" }));
            Console.WriteLine(cacher.Invoke<string>(new Function<string>(ThisIsATest), new { message = "Hallo 2" }));
            Console.WriteLine(cacher.Invoke<string>(new Function<string>(ThisIsATest), new { message = "Hallo" }));

            Console.Read();
        }
    }
}
