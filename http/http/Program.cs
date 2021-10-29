using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace http
{
    class Program
    {
        async static Task Main(string[] args)
        {
            using var client = new HttpClient();
            Console.WriteLine("Hello!");
            Console.WriteLine("Введите год");
            string year = Console.ReadLine();
            
            Console.WriteLine("Введите месяц");
            string month =Console.ReadLine();
            Console.WriteLine("Введите день");
            string day = Console.ReadLine();
            DateTime dt = new DateTime(Convert.ToInt32(year), Convert.ToInt32(month), Convert.ToInt32(day));
            string dataToSend = String.Format("{0:yyyyMMdd}", dt);
            HttpResponseMessage response = await client.GetAsync($"https://isdayoff.ru/{dataToSend}");
            int code = (int)response.StatusCode;
            if (code == 200)
            {
                string data = await response.Content.ReadAsStringAsync();
                if (data == "0")
                {
                    Console.WriteLine("Рабочий день");
                }
                else if (data == "1")
                {
                    Console.WriteLine("Выходной");
                }
                else
                {
                    Console.WriteLine(data);
                }
            }
            else if (code == 404)
            {
                Console.WriteLine("Данные не найдены");
            }
            else if(code == 400)
            {
                Console.WriteLine("Ошибка в дате");
            }
        }
    }
}
