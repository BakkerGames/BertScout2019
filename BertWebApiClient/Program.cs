using BertScout2019Data.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace BertWebApiClient
{
    public partial class Program
    {
        private static readonly string baseWebAddress = "http://localhost:64190/";

        static HttpClient client = new HttpClient();

        static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            List<FRCEvent> items;
            List<EventTeamMatch> itemsETM;

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

                //// Create a new FRCEvent
                //FRCEvent item = new FRCEvent
                //{
                //    Name = "Gizmo Event7",
                //    Location = "Anytown, ME",
                //    EventKey = "GIZMOS7",
                //    Changed = 1,
                //};
                //var url = await CreateFRCEventAsync(item);
                //Console.WriteLine($"Created at {url}");

                // show all matches
                Console.WriteLine("All EventTeamMatches:");
                itemsETM = await GetEventTeamMatchesAsync();
                foreach (EventTeamMatch showItemETM in itemsETM)
                {
                    ShowEventTeamMatch(showItemETM);
                }
                Console.WriteLine();

                // Create a new EventTeamMatch
                EventTeamMatch itemETM = new EventTeamMatch
                {
                    EventKey = "TEST",
                    TeamNumber = 133,
                    MatchNumber = 5,
                    AllianceResult = 1,
                };
                var urlETM = await CreateEventTeamMatchAsync(itemETM);
                Console.WriteLine($"Created at {urlETM}");

                // show all matches
                Console.WriteLine("All EventTeamMatches:");
                itemsETM = await GetEventTeamMatchesAsync();
                foreach (EventTeamMatch showItemETM in itemsETM)
                {
                    ShowEventTeamMatch(showItemETM);
                }
                Console.WriteLine();

                //// Get the FRCEvent
                //item = await GetFRCEventAsync(url.PathAndQuery);
                //ShowFRCEvent(item);

                //// show all frcevents
                //Console.WriteLine("All FRC Events:");
                //items = await GetFRCEventsAsync();
                //foreach (FRCEvent showItem in items)
                //{
                //    ShowFRCEvent(showItem);
                //}
                //Console.WriteLine();

                //EventTeamMatch eventTeamMatch = new EventTeamMatch();
                //eventTeamMatch.EventKey = "WEEKZERO";
                //eventTeamMatch.TeamNumber = 133;
                //eventTeamMatch.MatchNumber = 17;
                //eventTeamMatch.Changed = 1;

                //var urlEtm = await CreateEventTeamMatchAsync(eventTeamMatch);
                //Console.WriteLine($"Created at {urlEtm}");

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
