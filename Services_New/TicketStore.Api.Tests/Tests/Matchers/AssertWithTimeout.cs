using System;
using System.Threading;
using System.Diagnostics;
using NHamcrest;
using NHamcrest.Core;
using NHamcrest.XUnit;

namespace TicketStore.Api.Tests.Tests.Matchers
{
    public class AssertWithTimeout
    {
        private static int _timeout = 10_000;
        private static int _delay = 300;

        public static void That<T>(Func<T> func, IMatcher<T> matcher)
        {
            That(null, func, matcher);
        }
        
        public static void That<T>(String reason, Func<T> func, IMatcher<T> matcher)
        {
            T actual = WaitAndGetActualValue(func, matcher, _timeout, _delay);
            if (!matcher.Matches(actual)) 
            {
                StringDescription stringDescription1 = new StringDescription();
                matcher.DescribeTo(stringDescription1);
                StringDescription stringDescription2 = new StringDescription();
                matcher.DescribeMismatch(actual, stringDescription2);
                throw new MatchException(stringDescription1.ToString(), stringDescription2.ToString(), reason);
            }
        }

        private static T WaitAndGetActualValue<T>(Func<T> func, IMatcher<T> matcher, int timeout, int delay)
        {
            var stopwatch = Stopwatch.StartNew();
            T value = func.Invoke();
            while (stopwatch.ElapsedMilliseconds < timeout && !matcher.Matches(value))
            {
                Thread.Sleep(delay);
                value = func.Invoke();
            }
            return value;
        }
    }
}
