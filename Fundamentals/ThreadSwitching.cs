using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Fundamentals
{
    public static class ThreadSwitching
    {
        public static void Demo()
        {
            var t1 = new Thread(() =>
            {
                for (var i = 0; i < 1000; i++)
                {
                    Console.Write("A");
                }
            }) {Name = "Thread 1"};

            var t2 = new Thread(() =>
            {
                for (var i = 0; i < 1000; i++)
                {
                    Console.Write("B");
                }
            }) {Name = "Thread 2"};

            t1.Start();
            t2.Start();
            //Output: AAAAABBBBABAABBBBBBAAABBBABA....
        }
    }
}
