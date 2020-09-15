using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServiceBusDemo.Receiver
{
    class ReceiverConsole
    {
        //ToDo: Enter a valid Service Bus connection string
        static string ConnectionString = "Endpoint=sb://servicebusnamespacedemo.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=WuOX453tyktn4YAc8yUXHuwfs5ayxWxUR2f55vl97iQ=";
        static string QueuePath = "queuedemo";

        

        static void Main(string[] args)
        {
            // Create a queue client
            var queueClient = new QueueClient(ConnectionString, QueuePath);

            // Create a message handler to receive messages
            queueClient.RegisterMessageHandler(ProcessMessagesAsync, ExceptionReceivedHandler);

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();

            // Close the client
            queueClient.CloseAsync().Wait();
        }


        static async Task ProcessMessagesAsync(Message message, CancellationToken token)
        {
            // Deserialize the message body.
            var text = Encoding.UTF8.GetString(message.Body);
            Console.WriteLine($"Received: { text }");
        }



        static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            return Task.CompletedTask;
        }
    }
}
