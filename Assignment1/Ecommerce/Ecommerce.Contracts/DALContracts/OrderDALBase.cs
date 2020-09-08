using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Ecommerce.Entities;
using Newtonsoft.Json;
namespace Ecommerce.Ecommerce.Contracts.DALContracts
{
    public abstract class OrderDALBase
    {      
        protected static List<Order> ordersList = new List<Order>();
        private static readonly string fileName = "Orders.json";
        public abstract Order GetOrderByOrderNumberDAL(double orderNumber);
        public abstract (bool,Guid) AddOrderDAL(Order newOrder);
        public abstract Order GetOrderByOrderIDDAL(Guid searchOrderID);
        public abstract bool UpdateOrderDAL(Order updateOrder);
        public abstract bool DeleteOrderDAL(Guid deleteOrderID);
        public abstract List<Order> GetOrdersByCustomerIDDAL(Guid customerID);

        public static void Serialize()
        {
            string serializedJson = JsonConvert.SerializeObject(ordersList);
            using (StreamWriter streamWriter = new StreamWriter(fileName))
            {
                streamWriter.Write(serializedJson);
                streamWriter.Close();
            }
        }
                
        public static void Deserialize()
        {
            if (!File.Exists(fileName))
                File.Create(fileName).Close();

            using (StreamReader streamReader = new StreamReader(fileName))
            {
                string fileContent = streamReader.ReadToEnd();
                streamReader.Close();
                var orderListFromFile = JsonConvert.DeserializeObject<List<Order>>(fileContent);
                if (orderListFromFile != null)
                {
                    ordersList = orderListFromFile;
                }
            }
        }

        static OrderDALBase()
        {
            Deserialize();
        }
    }
}
