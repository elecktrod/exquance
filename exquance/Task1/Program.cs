using System;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите число");
            string number = Console.ReadLine().Trim();
            int temp = 0;
            try
            {
                for (int i = 0; i < number.Length; i++)
                {
                    int x;
                    if (!Int32.TryParse(temp.ToString() + number[i], out x))
                    {
                        throw new Exception();
                    }
                    x = Int32.Parse(temp.ToString() + number[i]);
                    temp = x % 15;
                }
                string result = string.Empty;
                if (temp % 3 == 0)
                    result += "Foo";
                if (temp % 5 == 0)
                    result += "Bar";
                Console.WriteLine(result);
            }
            catch
            {
                Console.WriteLine("ошибка ввода");
            }
            Console.ReadKey();
        }
    }
}
