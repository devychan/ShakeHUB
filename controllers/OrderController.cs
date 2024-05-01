using Models;
using Modules;
using MySql.Data.MySqlClient;
namespace Controllers
{
    public class OrderController : Database
    {

        public async Task<List<Orders>> GetAll()
        {
            try
            {
                await Connect();
                Connection.Open();
                MySqlCommand Command = new()
                {
                    Connection = Connection,
                    CommandText = @"SELECT * FROM orders"
                };
                List<Orders> orders = [];
                var Reader = await Command.ExecuteReaderAsync();
                while (Reader.Read())
                {
                    Orders order = new()
                    {
                        id = (int)Reader["id"],
                        product = (string)Reader["product"],
                        variant = (string)Reader["variant"],
                        amount = (decimal)Reader["amount"],
                        status = (string)Reader["status"],
                        createdAt = (DateTime)Reader["createdAt"]
                    };
                    orders.Add(order);
                }
                return orders;
            }
            catch (MySqlException error)
            {
                Console.WriteLine($"Message: {error.Message}");
                return null;
            }
        }
    }
}