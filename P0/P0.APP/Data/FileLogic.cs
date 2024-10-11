using System.Text.Json;
using P0.APP.Model;

namespace P0.APP.Data {
    public static class FileLogic {

        //Write to File
        public static async void SerializeAsync(List<Customer> customers, string fileName = "Data/customers.json") {
            string customerList = JsonSerializer.Serialize(customers);

            try{
                using StreamWriter writer = File.CreateText(fileName);
                await writer.WriteAsync(customerList);
            }
            catch(Exception e){
                Console.WriteLine($"Could not save changes: {e.Message}");
            }
        }

        //Read from File
        public static List<Customer> DeserializeCustomers(string fileName = "Data/customers.json"){
            try{
                using StreamReader reader = File.OpenText(fileName);
                string jsonString = reader.ReadToEnd();
                return JsonSerializer.Deserialize<List<Customer>>(jsonString)!;
            }
            catch(Exception e){
                Console.WriteLine($"Could not load data: {e.Message}");
                return [];
            }
        }
    }
}