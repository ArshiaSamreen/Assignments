using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using PublishSubscribeWebAPI.Services;
using PublishSubscribeWebAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PublishSubscribeWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly ILogger<MessageController> _logger;

        private readonly PublishMessage _publish;

        public MessageController(ILogger<MessageController> logger, PublishMessage publish)
        {
            _logger = logger;
            _publish = publish;
        }
        // GET: api/<MessageController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MessageController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MessageController>
        [HttpPost]
        public async Task<string> Post([FromBody]PromotionMessage promotionMessage)
        {
            string result = await _publish.SendMessagesAsync(promotionMessage);
            return result;
        }

        // PUT api/<MessageController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MessageController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
