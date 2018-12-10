using Checkout.Resources.Models;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Checkout.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {
            var client = new Client("https://localhost:44322", new HttpClient());

            Console.WriteLine("Adding an order with an item");

            var product = new Product { Name = "Emperor Palpatine" };
            var item = new OrderItem { Product = product, Quantity = 5 };

            var order = await client.OrderAddItemAsync(null, item);

            Console.WriteLine($"Got back an order with ID {order.OrderId}");
            Console.WriteLine($"It has one order item with a quantity of {order.Items.Single().Quantity} and a product called {order.Items.Single().Product.Name}");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine($"Adding an item to the order we just created");

            var productToAdd = new Product { Name = "Wampa" };
            var itemToAdd = new OrderItem { Product = productToAdd, Quantity = 8 };
            var orderWithItemAdded = await client.OrderAddItemAsync(order.OrderId, itemToAdd);

            Console.WriteLine($"The order now has {orderWithItemAdded.Items.Count} items");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Updating the quantity of Wampas ordered, because we need more");
            var wampa = orderWithItemAdded.Items.Single(x => x.Product.Name == "Wampa");

            var orderWithMoreWampas = await client.OrderItemUpdateQuantityAsync(order.OrderId, wampa.ItemId, 11);
            var newWampaItem = orderWithMoreWampas.Items.Single(x => x.Product.Name == "Wampa");

            Console.WriteLine($"This order now has {newWampaItem.Quantity} wampas");
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("Clearing all the items from the order");

            var clearedOrder = await client.OrderClearAsync(order.OrderId);
            Console.WriteLine($"This order now has {clearedOrder.Items.Count} items in it");

            Console.ReadLine();
        }
    }
}
