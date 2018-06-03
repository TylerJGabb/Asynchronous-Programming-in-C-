using System;
using System.Threading;

namespace ThreadSynchronisation
{
    class MainClass
    {
        
        public static int result; // shared field for work result (default = 0)
        private static object lockHandle = new object(); // lock handle for shared result

        //event wait handles
        public static EventWaitHandle _readyForResult = new AutoResetEvent(false);
        public static EventWaitHandle _finishedWithResult = new AutoResetEvent(false);

        public static void DoWork()
        {
            while (true)
            {
                Thread.Sleep(1);// simulate long calculation
                _readyForResult.WaitOne();// blocks this thread until main is ready for a result
                lock (lockHandle)
                {
                    result++; // set the result

                } 
                _finishedWithResult.Set(); // signals to main that we have set our result
            }
        }

        public static void Main(string[] args)
        {
            var t = new Thread(DoWork); t.Start(); // start the thread
            for (var i = 0; i < 100; i++)
            {
                _readyForResult.Set(); // tell thread that we're ready to receive the result
                _finishedWithResult.WaitOne(); // blocks this thread until _finishedWithResult is set
                lock (lockHandle)
                {
                    Console.WriteLine(result); // simulate other work 

                }
                Thread.Sleep(1000); 
            }
            t.Abort();
        }
    }
}
