using System;
using System.Threading;
using System.Threading.Tasks;

namespace WorkingWithTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            var task = Task<string>.Factory.StartNew(() =>
            {
                Thread.Sleep(2000);
                return "Tyler";
            });

            Console.Write("Your name is: ");
            Console.WriteLine(task.Result);
        }
    }
}
