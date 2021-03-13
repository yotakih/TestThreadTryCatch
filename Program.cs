using System;
using System.Threading;
using System.Threading.Tasks;

namespace TestThreadTryCatch
{
    class Program
    {
        static void Main(string[] args)
        {
            write_console($"Main");
            // catch_error1();
            // catch_error2();
            catch_error3();
            // catch_error4();

            write_console($"End");
            Console.ReadLine();
        }
        static void throw_error_task(int id)
        {
            try
            {
                write_console($"start heavy task: {id}");
                Thread.Sleep(3000);
                throw new Exception($"error: {id}");
                write_console($"end   heavy task: {id}");
            }
            catch (Exception e)
            {
                write_console($"catch inner try {e.Message}");
                throw;
            }
        }
        static void catch_error1()
        {
            write_console($"start catch_error1");
            try
            {
                Task.Run(() =>
                {
                    throw_error_task(1);
                });
            }
            catch (Exception e)
            {
                write_console($"catch outer try {e.Message}");
            }
            write_console($"end   catch_error1");
        }
        static void catch_error2()
        {
            write_console($"start catch_error2");
            try
            {
                Task.Run(() =>
                {
                    throw_error_task(2);
                }).Wait(3000);
            }
            catch (Exception e)
            {
                write_console($"catch outer try {e.Message}");
            }
            write_console($"end   catch_error2");
        }
        static async void catch_error3()
        {
            write_console($"start catch_error3");
            try
            {
                await Task.Run(() =>
                {
                    throw_error_task(3);
                });
            }
            catch (Exception e)
            {
                write_console($"catch outer try {e.Message}");
            }
            write_console($"end   catch_error3");
        }
        static async void catch_error4()
        {
            write_console($"start catch_error4");
            try
            {
            }
            catch (Exception e)
            {
                write_console($"catch outer try {e.Message}");
            }
            write_console($"end   catch_error4");
        }
        static void write_console(string str)
        {
            Console.WriteLine(
                $"{DateTime.Now.ToString("HH:mm:ss.fff")}"
                + " "
                + $"threadId: {Thread.CurrentThread.ManagedThreadId}"
                + " "
                + str
            );
        }
    }
}
