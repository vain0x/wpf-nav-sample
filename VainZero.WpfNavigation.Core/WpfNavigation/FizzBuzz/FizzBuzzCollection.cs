using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reactive.Bindings;

namespace VainZero.WpfNavigation.FizzBuzz
{
    public sealed class FizzBuzzCollection
    {
        public ReactiveProperty<int> Count { get; } =
            new ReactiveProperty<int>(0);

        public IReadOnlyReactiveProperty<IReadOnlyList<string>> Items { get; }

        static string CalculateFizzBuzz(int n)
        {
            return
                n % 15 == 0 ? "Fizz Buzz" :
                n % 3 == 0 ? "Fizz" :
                n % 5 == 0 ? "Buzz" : n.ToString();
        }

        public FizzBuzzCollection()
        {
            Items =
                Count.Select(count =>
                    Enumerable.Range(0, count)
                    .Select(CalculateFizzBuzz)
                    .ToArray()
                ).ToReadOnlyReactiveProperty();
        }
    }
}
