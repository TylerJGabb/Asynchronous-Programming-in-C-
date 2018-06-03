using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;

namespace Fundamentals
{
    public class Locking
    {
        //If we look at the code in RaceCondition.UnpredictableBehavior()
        //we notice that there is a region of code which can be freely
        //executed by any thread, and sometimes simultaneously. 
        //by using the `lock` keyword, we can turn the block of code
        //that is sensitive into an atomic operation called a 'critical section'
        //which can only be executed by one thread at a time. 

        //It is important to lock all compare-and-assign operations

        //Also, it is important to do your best to pull out any long running methods
        //from the critical section;



        /// <summary>
        /// A static object (of reference type) that can be used to synchronize the lock.
        /// It is convention to have this be a static property of the class enclosing the thread logic
        /// </summary>
        private static object _synchronizationObject = new
        {
            //Any reference type will do. 
        };

        private static int _sharedVariable;

        private static void PrintStars()
        {
            for(_sharedVariable = 0; _sharedVariable < 5; _sharedVariable++)
                Console.Write("*");
        }

        /// <summary>
        /// An example of using lock to properly moderate variables
        /// accessed by critical sections
        /// </summary>
        public static void Good()
        {
            Console.WriteLine("|--------| <===expecting this many stars for predictable behavior");
            var t1 = new Thread(() =>
            {
                lock (_synchronizationObject)
                {
                    PrintStars();
                }
            });

            var t2 = new Thread(() =>
            {
                lock (_synchronizationObject)
                {
                    PrintStars();
                }
            });

            t1.Start();
            t2.Start();

            //  OUTPUT
            //    |--------| <===expecting this many stars for predictable behavior
            //    **********


        }

        /// <summary>
        /// Example of not using lock to moderate access to shared variables
        /// </summary>
        public static void Bad()
        {
            Console.WriteLine("|--------| <===expecting this many stars for predictable behavior");
            var t1 = new Thread(PrintStars);
            var t2 = new Thread(PrintStars);
            t1.Start();
            t2.Start();

            //OUTPUT
               
            //  |--------| <===expecting this many stars for predictable behavior
            //  ******

        }

    }
}
