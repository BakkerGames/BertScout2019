using BertScout2019Data.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpClientSample
{
    

    class Program
    {

        private static string baseWebAddress = "http://localhost:64190/";

        static HttpClient client = new HttpClient();
        

        static void ShowFRCEvent(FRCEvent FRCEvent)
        {
            Console.WriteLine($"Name: {FRCEvent.Name}\tLocation: " +
                $"{FRCEvent.Location}\tEventKey: {FRCEvent.EventKey}");
        }

        
        static async Task<Uri> CreateFRCEventAsync(FRCEvent FRCEvent)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/FRCEvents", FRCEvent);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }
        

        
        static async Task<FRCEvent> GetFRCEventAsync(string path)
        {
            FRCEvent FRCEvent = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                FRCEvent = await response.Content.ReadAsAsync<FRCEvent>();
            }
            return FRCEvent;
        }
        

        
        static async Task<FRCEvent> UpdateFRCEventAsync(FRCEvent FRCEvent)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/FRCEvents/{FRCEvent.Id}", FRCEvent);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated FRCEvent from the response body.
            FRCEvent = await response.Content.ReadAsAsync<FRCEvent>();
            return FRCEvent;
        }
        
        static async Task<HttpStatusCode> DeleteFRCEventAsync(int? id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/FRCEvents/{id}");
            return response.StatusCode;
        }

        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri(baseWebAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            
            try
            {
                // Create a new FRCEvent
                FRCEvent FRCEvent = new FRCEvent
                {
                    Name = "Gizmo Event",
                    Location = "Anytown, ME",
                    EventKey = "GIZMOS"
                };

                var url = await CreateFRCEventAsync(FRCEvent);
                Console.WriteLine($"Created at {url}");

                // Get the FRCEvent
                FRCEvent = await GetFRCEventAsync(url.PathAndQuery);
                ShowFRCEvent(FRCEvent);

                // Update the FRCEvent
                Console.WriteLine("Updating Location...");
                FRCEvent.Location = "Anothertown, ME";
                await UpdateFRCEventAsync(FRCEvent);

                // Get the updated FRCEvent
                FRCEvent = await GetFRCEventAsync(url.PathAndQuery);
                ShowFRCEvent(FRCEvent);

                // Delete the FRCEvent
                var statusCode = await DeleteFRCEventAsync(FRCEvent.Id);
                Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
        
    }
}
