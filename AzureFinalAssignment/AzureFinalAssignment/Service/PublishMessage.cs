using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AzureFinalAssignment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Azure.ServiceBus;

namespace AzureFinalAssignment.Service
{
    public class PublishMessage
    {
        static HttpClient client = new HttpClient();
        public async Task<string> SendMessagesAsync(PromotionMessage promotionMessage)
        {
             try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("http://localhost:5000/api/message", promotionMessage);
                response.EnsureSuccessStatusCode();
                return "Message has been published";
            }
            catch (Exception exception)
            {
                return $"{DateTime.Now} :: Exception: {exception.Message}";
            }
        }

    }
}
    