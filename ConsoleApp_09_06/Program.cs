using System;
using System.Threading;

class Program
{
    // Створюємо семафор з максимум трьома дозволами
    private static Semaphore semaphore = new Semaphore(3, 3);

    static void Main()
    {
        // Створюємо та запускаємо 10 потоків
        for (int i = 0; i < 10; i++)
        {
            Thread thread = new Thread(ThreadWork);
            thread.Start(i);
        }

        // Очікуємо завершення всіх потоків перед завершенням програми
        Console.WriteLine("Press Enter to exit.");
        Console.ReadLine();
    }

    private static void ThreadWork(object id)
    {
        // Очікуємо на доступ до семафора
        semaphore.WaitOne();

        try
        {
            // Відображаємо ідентифікатор потоку
            Console.WriteLine($"Thread {id} started.");

            // Генеруємо та виводимо набір випадкових чисел
            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Thread {id} random number: {random.Next(100)}");
                Thread.Sleep(100); // Затримка для імітації роботи
            }

            Console.WriteLine($"Thread {id} finished.");
        }
        finally
        {
            // Звільняємо семафор
            semaphore.Release();
        }
    }
}
