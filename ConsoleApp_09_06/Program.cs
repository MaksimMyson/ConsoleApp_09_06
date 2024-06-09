using System;
using System.Threading;

class Program
{
    private static Mutex mutex = new Mutex();
    private static bool isFirstThreadFinished = false;

    static void Main()
    {
        Thread firstThread = new Thread(PrintAscending);
        Thread secondThread = new Thread(PrintDescending);

        firstThread.Start();
        secondThread.Start();

        firstThread.Join();
        secondThread.Join();

        Console.WriteLine("All threads have completed.");
    }

    private static void PrintAscending()
    {
        mutex.WaitOne();
        try
        {
            for (int i = 0; i <= 20; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(100); // Для демонстрації роботи потоків
            }
            isFirstThreadFinished = true;
        }
        finally
        {
            mutex.ReleaseMutex();
        }
    }

    private static void PrintDescending()
    {
        while (!isFirstThreadFinished)
        {
            Thread.Sleep(100); // Очікування завершення першого потоку
        }

        mutex.WaitOne();
        try
        {
            for (int i = 10; i >= 0; i--)
            {
                Console.WriteLine(i);
                Thread.Sleep(100); // Для демонстрації роботи потоків
            }
        }
        finally
        {
            mutex.ReleaseMutex();
        }
    }
}
