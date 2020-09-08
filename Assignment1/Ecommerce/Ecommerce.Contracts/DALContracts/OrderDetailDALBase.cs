using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Ecommerce.Entities;
using Newtonsoft.Json;

namespace Ecommerce.Ecommerce.Contracts
{
    
    public abstract class OrderDetailDALBase
    {
        protected static List<OrderDetail> orderDetailsList = new List<OrderDetail>();
        private static string fileName = "orderDetails.json";
        public abstract (bool,Guid) AddOrderDetailsDAL(OrderDetail newOrder);
        public abstract List<OrderDetail> GetOrderDetailsByOrderIDDAL(Guid searchOrderID);
        public abstract OrderDetail GetOrderDetailByOrderIDDAL(Guid searchOrderID);
        public abstract bool UpdateOrderDetailsDAL(OrderDetail updateOrder);
        public abstract bool DeleteOrderDetailsDAL(Guid deleteOrderID);
        public abstract bool UpdateOrderDeliveredStatusDAL(Guid orderID);
        public abstract bool UpdateOrderShippedStatusDAL(Guid orderID);
        public abstract bool UpdateOrderDispatchedStatusDAL(Guid orderID);

        public static void Serialize()
        {
            string serializedJson = JsonConvert.SerializeObject(orderDetailsList);
            using (StreamWriter streamWriter = new StreamWriter(fileName))
            {
                streamWriter.Write(serializedJson);
                streamWriter.Close();
            }
        }

        public static void Deserialize()
        {
            string fileContent = string.Empty;
            if (!File.Exists(fileName))
                File.Create(fileName).Close();

            using (StreamReader streamReader = new StreamReader(fileName))
            {
                fileContent = streamReader.ReadToEnd();
                streamReader.Close();
                var orderListFromFile = JsonConvert.DeserializeObject<List<OrderDetail>>(fileContent);
                if (orderListFromFile != null)
                {
                    orderDetailsList = orderListFromFile;
                }
            }
        }

        static OrderDetailDALBase()
        {
            Deserialize();
        }
    }
}
