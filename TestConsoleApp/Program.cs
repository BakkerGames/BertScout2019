using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace TestConsoleApp
{
    public partial class FRCEvent
    {
        public int? Id { get; set; }
        public string Uuid { get; set; }
        public int Changed { get; set; }
        public string EventKey { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }

    class Program
    {
        static HttpClient client = new HttpClient();

        static void ShowFRCEvent(FRCEvent frcEvent)
        {
            Console.WriteLine($"Name: {frcEvent.Name}");
        }

        static void ShowAllFRCEvents(List<FRCEvent> frcEvents)
        {
            foreach (FRCEvent item in frcEvents)
            {
                ShowFRCEvent(item);
            }
        }

        static async Task<Uri> CreateFRCEventAsync(FRCEvent frcEvent)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/GetFRCEvents", frcEvent);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        static async Task<FRCEvent> GetFRCEventAsync(string path)
        {
            FRCEvent frcEvent = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                frcEvent = await response.Content.ReadAsAsync<FRCEvent>();
            }
            return frcEvent;
        }

        static async Task<List<FRCEvent>> GetFRCEventsAsync(string path)
        {
            List<FRCEvent> frcEvents = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                frcEvents = await response.Content.ReadAsAsync<List<FRCEvent>>();
            }
            return frcEvents;
        }

        static async Task<FRCEvent> UpdateFRCEventAsync(FRCEvent frcEvent)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/GetFRCEvents/{frcEvent.Id}", frcEvent);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated frcEvent from the response body.
            frcEvent = await response.Content.ReadAsAsync<FRCEvent>();
            return frcEvent;
        }

        static async Task<HttpStatusCode> DeleteFRCEventAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/GetFRCEvents/{id}");
            return response.StatusCode;
        }

        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:64190/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {

                List<FRCEvent> frcEvents = await GetFRCEventsAsync("http://localhost:64190/api/FRCEvents");
                ShowAllFRCEvents(frcEvents);

                // Create a new frcEvent
                FRCEvent frcEvent = new FRCEvent
                {
                    Name = "Gizmo",
                };

                var url = await CreateFRCEventAsync(frcEvent);
                Console.WriteLine($"Created at {url}");

                //// Get the frcEvent
                //frcEvent = await GetFRCEventAsync(url.PathAndQuery);
                //ShowFRCEvent(frcEvent);

                //// Delete the frcEvent
                //var statusCode = await DeleteFRCEventAsync(frcEvent.Id.Value);
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
