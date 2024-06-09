using System;
using System.Threading;

class Program
{
    // Унікальне ім'я м'ютекса для ідентифікації додатка
    private const string MutexName = "Global\\MySingleInstanceApp";

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.Unicode;

        using (Mutex mutex = new Mutex(false, MutexName, out bool createdNew))
        {
            if (!createdNew)
            {
                Console.WriteLine("Програма вже запущена. Друга копія не може бути запущена.");
                return;
            }

            // Основний код додатка
            Console.WriteLine("Програма запущена. Натисніть Enter для завершення роботи.");
            Console.ReadLine();
        }
    }
}
