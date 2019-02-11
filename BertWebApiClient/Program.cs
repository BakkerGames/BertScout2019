using BertScout2019Data.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BertWebApiClient
{
    

    class Program
    {

        private static readonly string baseWebAddress = "http://localhost:64190/";

        static HttpClient client = new HttpClient();
        
        static void ShowFRCEvent(FRCEvent item)
        {
            Console.WriteLine($"Id: {item.Id} - Name: {item.Name} = Location: " +
                $"{item.Location} - EventKey: {item.EventKey}");
        }

        static async Task<Uri> CreateFRCEventAsync(FRCEvent item)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/FRCEvents", item);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }
        
        static async Task<FRCEvent> GetFRCEventAsync(string path)
        {
            FRCEvent item = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                item = await response.Content.ReadAsAsync<FRCEvent>();
            }
            return item;
        }

        static async Task<List<FRCEvent>> GetFRCEventsAsync()
        {
            List<FRCEvent> items = null;
            HttpResponseMessage response = await client.GetAsync("api/FRCEvents");
            if (response.IsSuccessStatusCode)
            {
                items = await response.Content.ReadAsAsync<List<FRCEvent>>();
            }
            return items;
        }

        static async Task<FRCEvent> UpdateFRCEventAsync(FRCEvent item)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/FRCEvents/{item.Id}", item);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated FRCEvent from the response body.
            item = await response.Content.ReadAsAsync<FRCEvent>();
            return item;
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
            List<FRCEvent> items;

            // Update port # in the following line.
            client.BaseAddress = new Uri(baseWebAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            
            try
            {
                // show all frcevents
                Console.WriteLine("All FRC Events:");
                items = await GetFRCEventsAsync();
                foreach (FRCEvent showItem in items)
                {
                    ShowFRCEvent(showItem);
                }
                Console.WriteLine();

                // Create a new FRCEvent
                FRCEvent item = new FRCEvent
                {
                    Name = "Gizmo Event7",
                    Location = "Anytown, ME",
                    EventKey = "GIZMOS7"
                };

                var url = await CreateFRCEventAsync(item);
                Console.WriteLine($"Created at {url}");

                // Get the FRCEvent
                item = await GetFRCEventAsync(url.PathAndQuery);
                ShowFRCEvent(item);

                // show all frcevents
                Console.WriteLine("All FRC Events:");
                items = await GetFRCEventsAsync();
                foreach (FRCEvent showItem in items)
                {
                    ShowFRCEvent(showItem);
                }
                Console.WriteLine();

                //// Update the FRCEvent
                //Console.WriteLine("Updating Location...");
                //item.Location = "Anothertown, ME";
                //await UpdateFRCEventAsync(item);

                //// Get the updated FRCEvent
                //item = await GetFRCEventAsync(url.PathAndQuery);
                //ShowFRCEvent(item);

                //// Delete the FRCEvent
                //var statusCode = await DeleteFRCEventAsync(item.Id);
                //Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
        
    }
}
