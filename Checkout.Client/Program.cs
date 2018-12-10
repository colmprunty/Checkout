using System;
using System.Net.Http;

namespace Checkout.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Client("http://localhost:44322", new HttpClient());
            Console.WriteLine("Adding an order with an item");

            client.OrderAddItemAsync(null, null);
        }
    }
}
