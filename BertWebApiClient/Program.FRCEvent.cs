using BertScout2019Data.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BertWebApiClient
{
    public partial class Program
    {
        static void ShowFRCEvent(FRCEvent item)
        {
            Console.WriteLine(
                $"Id: {item.Id} - Uuid: {item.Uuid}" +
                $" - EventKey: {item.EventKey}" +
                $" - Name: {item.Name}" +
                $" - Location: {item.Location}");

        }

        static async Task<Uri> CreateFRCEventAsync(FRCEvent item)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("api/FRCEvents", item);
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
                $"api/FRCEvents?uuid={item.Uuid}", item);
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
    }
}
