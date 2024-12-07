using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

class Emulator
{
    static async Task Main(string[] args)
    {
        HttpClient client = new HttpClient();
        Random rnd = new Random();

       
        string baseUrl = "http://localhost:8080/api/sensors";

        while (true)
        {
           
            double voltage = rnd.Next(220, 241); 
            double current = rnd.NextDouble() * (20 - 10) + 10; 
            double frequency = rnd.NextDouble() * (51 - 49) + 49; 
            double power = voltage * current; 

            
            var sensorData = new
            {
                Voltage = voltage,
                Current = current,
                Frequency = frequency,
                Power = power
            };

            
            string jsonData = JsonConvert.SerializeObject(sensorData);

           
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            try
            {
               
                HttpResponseMessage response = await client.PostAsync(baseUrl, content);

                
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Data sent: Voltage={voltage}V, Current={current}A, Frequency={frequency}Hz, Power={power}W");
                }
                else
                {
                    Console.WriteLine("Error: Unable to send data.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

          
            await Task.Delay(2000);
        }
    }
}