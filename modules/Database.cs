using MySql.Data.MySqlClient;
namespace Modules
{
    public class Database
    {
        private readonly string server = "server=localhost;uid=root;pwd=;database=shakehub";
        protected MySqlConnection Connection;
        public Database() { }
        public async Task Connect()
        {
            try
            {
                Connection = new MySqlConnection(this.server);
                Connection.Open();
            }
            catch (MySqlException error)
            {
                Console.WriteLine($"Error: {error.Message}");
            }
            finally
            {
                Connection.Close();
            }
        }
    }
}