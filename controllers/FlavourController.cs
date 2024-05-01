using System.Data;
using Models;
using Modules;
using MySql.Data.MySqlClient;

namespace Controllers
{
    public class FlavourController : Database
    {
        public FlavourController() { }
        public async Task<List<Flavors>> GetAll()
        {
            try
            {
                await Connect();
                MySqlCommand Command = new MySqlCommand();
                Connection.Open();
                Command.Connection = Connection;
                Command.CommandText =
                @"SELECT tb1.*, tb2.ref as variant, tb2.name
                FROM shakes as tb1
                LEFT JOIN variants as tb2 ON tb1.type = tb2.id
                GROUP BY id
                ORDER BY variant, price asc";
                using var Reader = await Command.ExecuteReaderAsync();
                List<Flavors> flavours = new() { };
                while (Reader.Read())
                {
                    Flavors flavor = new()
                    {
                        id = (int)Reader["id"],
                        flavor = (string)Reader["flavor"],
                        price = (decimal)Reader["price"],
                        variant = (int)Reader["variant"],
                        name = (string)Reader["name"],
                        createdAt = (DateTime)Reader["createdAt"],
                        updatedAt = (DateTime)Reader["updatedAt"],
                        deletedAt = Reader.IsDBNull("deletedAt") ? null : (DateTime)Reader["deletedAt"],
                    };
                    flavours.Add(flavor);
                }
                Connection.Close();
                return flavours;
            }
            catch (MySqlException error)
            {
                Console.WriteLine($"Message: {error.Message}");
                Connection.Close();
                return null;
            }
        }
        public async Task<Flavors> GetData(int id)
        {
            try
            {
                await Connect();
                Connection.Open();
                MySqlCommand Command = new MySqlCommand();
                Command.Connection = Connection;
                Command.CommandText =
                @"
                    SELECT tb1.*, tb2.ref as variant, tb2.name
                    FROM shakes as tb1
                    LEFT JOIN variants as tb2 ON tb1.type = tb2.id
                    WHERE tb1.id = @id
                    GROUP BY id
                    ORDER BY variant, price asc
                ";
                Command.Parameters.AddWithValue("@id", id);
                var Reader = await Command.ExecuteReaderAsync();
                Flavors flavor = new Flavors();
                while (Reader.Read())
                {
                    flavor.id = (int)Reader["id"];
                    flavor.flavor = (string)Reader["flavor"];
                    flavor.price = (decimal)Reader["price"];
                    flavor.variant = (int)Reader["variant"];
                    flavor.name = (string)Reader["name"];
                }
                return flavor;
            }
            catch (MySqlException error)
            {
                Console.WriteLine($"Message: {error.Message}");
                return null;
            }
        }
        public async Task Create(AddOrder order)
        {
            try
            {
                await Connect();
                Connection.Open();
                MySqlCommand Command = new MySqlCommand();
                Command.Connection = Connection;
                Command.CommandText =
                @"
                    INSERT INTO orders (
                        shake_id,
                        product,
                        quantity,
                        variant,
                        amount
                    ) VALUES (
                        @shake_id,
                        @product,
                        @quantity,
                        @variant,
                        @amount
                    )
                ";
                Command.Parameters.AddWithValue("@shake_id", order.shake_id);
                Command.Parameters.AddWithValue("@product", order.product);
                Command.Parameters.AddWithValue("@quantity", order.quantity);
                Command.Parameters.AddWithValue("@variant", order.variant);
                Command.Parameters.AddWithValue("@amount", order.amount);
                await Command.ExecuteNonQueryAsync();
                Console.WriteLine("Order successful.");
                Connection.Close();
            }
            catch (MySqlException error)
            {
                Console.WriteLine($"Message: {error.Message}");
                Connection.Close();
            }
        }
    }
}