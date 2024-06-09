using System;
using System.Linq;
using System.Threading;

class Program
{
    private static Mutex mutex = new Mutex();
    private static bool isModificationComplete = false;
    private static int[] dataArray;

    static void Main()
    {
        // Ініціалізуємо масив даними
        dataArray = new int[] { 1, 2, 3, 4, 5 };

        Thread modifyThread = new Thread(ModifyArray);
        Thread findMaxThread = new Thread(FindMaxInArray);

        modifyThread.Start();
        findMaxThread.Start();

        modifyThread.Join();
        findMaxThread.Join();

        Console.WriteLine("All threads have completed.");
    }

    private static void ModifyArray()
    {
        mutex.WaitOne();
        try
        {
            Random random = new Random();
            for (int i = 0; i < dataArray.Length; i++)
            {
                int randomValue = random.Next(1, 10); // Довільне число від 1 до 9
                dataArray[i] += randomValue;
                Console.WriteLine($"Modified element {i}: {dataArray[i]}");
                Thread.Sleep(100); // Для демонстрації роботи потоків
            }
            isModificationComplete = true;
        }
        finally
        {
            mutex.ReleaseMutex();
        }
    }

    private static void FindMaxInArray()
    {
        while (!isModificationComplete)
        {
            Thread.Sleep(100); // Очікування завершення модифікації масиву
        }

        mutex.WaitOne();
        try
        {
            int maxValue = dataArray.Max();
            Console.WriteLine($"Max value in the array: {maxValue}");
        }
        finally
        {
            mutex.ReleaseMutex();
        }
    }
}
