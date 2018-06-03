using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Fundamentals
{
    class RaceConditions
    {
        //Each thread has its own private stack. That means that any
        //local variables used within the thread belong to the thread
        //and that thread alone. Observe the results of the following code

        public static void UnpredictableBehavior()
        {
            int _sharedVariable;
            void DoWork()
            {
                for (_sharedVariable = 0; _sharedVariable < 5; _sharedVariable++)
                {
                    Console.Write("*");
                }
            }

            var t = new Thread(DoWork);
            t.Start();
            DoWork();
            //Output: ???
            //Sometimes 5 starts, sometimes 6.
        }


        public static void PredictableBehavior()
        {
            void DoWork()
            {
                for (var i = 0; i < 5; i++)
                {
                    Console.Write("*");
                }
            }

            var t = new Thread(DoWork);
            t.Start();
            DoWork();
            
            // OUTPUT: **********
            //Ten stars. 
        }

    }
}
