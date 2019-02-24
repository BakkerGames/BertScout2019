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
        static void ShowEventTeamMatch(EventTeamMatch item)
        {
            Console.WriteLine(
                $"Id: {item.Id} - Uuid: {item.Uuid}" +
                $" - EventKey: {item.EventKey}" +
                $" - Team: {item.TeamNumber}" +
                $" - Match: {item.MatchNumber}" +
                $" - AllianceResult = {item.AllianceResult}");
        }

        static async Task<Uri> CreateEventTeamMatchAsync(EventTeamMatch item)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/EventTeamMatches", item);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        static async Task<EventTeamMatch> GetEventTeamMatchAsync(string path)
        {
            EventTeamMatch item = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                item = await response.Content.ReadAsAsync<EventTeamMatch>();
            }
            return item;
        }

        static async Task<List<EventTeamMatch>> GetEventTeamMatchesAsync()
        {
            List<EventTeamMatch> items = null;
            HttpResponseMessage response = await client.GetAsync("api/EventTeamMatches");
            if (response.IsSuccessStatusCode)
            {
                items = await response.Content.ReadAsAsync<List<EventTeamMatch>>();
            }
            return items;
        }

        static async Task<EventTeamMatch> UpdateEventTeamMatchAsync(EventTeamMatch item)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/EventTeamMatchs?uuid={item.Uuid}", item);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated EventTeamMatch from the response body.
            item = await response.Content.ReadAsAsync<EventTeamMatch>();
            return item;
        }

        static async Task<HttpStatusCode> DeleteEventTeamMatchAsync(int? id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/EventTeamMatchs/{id}");
            return response.StatusCode;
        }
    }
}
