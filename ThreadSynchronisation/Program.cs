using System;
using System.Threading;

namespace ThreadSynchronisation
{
    class MainClass
    {
        
        public static int result; // shared field for work result (default = 0)
        private static object lockHandle = new object(); // lock handle for shared result


        //event wait handles
        public static EventWaitHandle _waitingForIncrement = new AutoResetEvent(false);
        public static EventWaitHandle _incrementFinished = new AutoResetEvent(false);
  
        public static void DoWork()
        {
            while (true)
            {
                Thread.Sleep(15);// simulate long calculation
                _waitingForIncrement.WaitOne();// blocks this thread until _waitingForIncrement.Set();
                lock (lockHandle)
                {
                    result++; // set the result

                } 
                _incrementFinished.Set(); // signals to main that we have set our result
            }
        }

        public static void Main(string[] args)
        {
            var t = new Thread(DoWork); t.Start(); // start the thread
            for (var i = 0; i < 100; i++)
            {
                _waitingForIncrement.Set(); // tell thread that we're ready to receive the result
                _incrementFinished.WaitOne(); // blocks this thread until _incrementFinished.Set();
                lock (lockHandle)
                {
                    Console.WriteLine(result); // simulate other work 

                }
                Thread.Sleep(10); 
            }
            t.Abort();
        }
    }
}
