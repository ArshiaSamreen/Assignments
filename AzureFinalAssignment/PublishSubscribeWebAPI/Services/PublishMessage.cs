using Microsoft.Azure.ServiceBus;
using PublishSubscribeWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublishSubscribeWebAPI.Services
{
    public class PublishMessage
    {
        const string ServiceBusConnectionString = "Endpoint=sb://promotionmessage.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=I7jGw0KeGshHJu1OHd5EYvxO1/fE6D1nxsJcwuJRq+o=";
        const string TopicName = "promotionmessagetopic";
        static ITopicClient topicClient;

        // Send messages.
        //SendMessagesAsync(numberOfMessages);            
        //topicClient.CloseAsync();

        public async Task<string> SendMessagesAsync(PromotionMessage promotionMessage)
        {
            topicClient = new TopicClient(ServiceBusConnectionString, TopicName);
            try
            {
                // Create a new message to send to the topic                 
                var message = new Message(Encoding.UTF8.GetBytes($"{promotionMessage.UserName}," +
                    $"{promotionMessage.EmailAddress},{promotionMessage.PhoneNumber}"));
                List<Message> messages = new List<Message>()
                {
                    new Message(Encoding.UTF8.GetBytes(promotionMessage.UserName)),
                    new Message(Encoding.UTF8.GetBytes(promotionMessage.EmailAddress)),
                    new Message(Encoding.UTF8.GetBytes(promotionMessage.PhoneNumber))
                };

                //public override string ToString() => $"{}";

                // Send the message to the topic
                await topicClient.SendAsync(message);
                return "Message has been published";
            }
            catch (Exception exception)
            {
                return $"{DateTime.Now} :: Exception: {exception.Message}";
            }
            finally
            {
                await topicClient.CloseAsync();
            }
        }
    }
}