using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;

namespace Fundamentals
{
    public static class BackgoundThreads
    {
        /*
        * A thread marked as background will be killed as soon as
        * the enclosing thread terminates. Observe here we have
        * a thread which is printing to the console, and starts
        * a new thread which waits 5 seconds to print something to
        * the console. In one example we set the internal thread to be
         * a background process, in the other we do not. Observe
        */

        public static void Demo(bool isBackground)
        {
            var thread = new Thread(() =>
            {
                var inner = new Thread(() =>
                {
                    Thread.Sleep(5000);
                    Console.WriteLine("I have finished sleeping");
                });

                //this will cause early termination of the sleeping thread
                //if set to true
                inner.IsBackground = isBackground; 


                inner.Start();
                Console.WriteLine("The enclosing thread has started");

            });
            thread.Start();
        }
    }
}
