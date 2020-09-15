using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBusDemo.Sender
{
    class SenderConsole
    {
        //ToDo: Enter a valid Service Bus connection string
        static string ConnectionString = "Endpoint=sb://servicebusnamespacedemo.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=WuOX453tyktn4YAc8yUXHuwfs5ayxWxUR2f55vl97iQ=";
        static string QueuePath = "queuedemo";


        static void Main(string[] args)
        {

            // Create a queue client
            var queueClient = new QueueClient(ConnectionString, QueuePath);

            // Send some messages
            for (int i = 0; i < 10; i++)
            {
                var content = $"Message: { i }";
                var message = new Message(Encoding.UTF8.GetBytes(content));
                queueClient.SendAsync(message).Wait();
                Console.WriteLine("Sent: " + i);
            }

            // Close the client
            queueClient.CloseAsync().Wait();
            Console.WriteLine("Sent messages...");
            Console.ReadLine();

        }
    }
}
