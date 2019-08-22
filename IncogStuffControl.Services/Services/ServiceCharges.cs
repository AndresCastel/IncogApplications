using Incog.Utils;
using IncogStuffControl.Services.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace IncogStuffControl.Services.Services
{
    public class ServiceCharges
    {
        public static async Task<MessageResponseViewModel<RosterWM>> ChageRoster(List<RosterCViewModel> lstRoster)
        {
            RosterWM rosterWM = new RosterWM();
            rosterWM.lstRoster = lstRoster;
            MessageResponseViewModel<RosterWM> resulMessage = new MessageResponseViewModel<RosterWM>();
            try
            {


                // Posting.
                using (var client = new HttpClient())
                {
                    // Setting Base address.
                    client.BaseAddress = new Uri(Globals.BaseUrl);


                    // Setting content type.
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Setting timeout.
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    var json = JsonConvert.SerializeObject(rosterWM);
                    var stringContent = new StringContent(json.ToString(), UnicodeEncoding.UTF8, "application/json");

                    // Initialization.
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP GET
                    response = await client.PostAsync("api/Charges/Roster/", stringContent).ConfigureAwait(false);

                    // Verification
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.
                        string result = response.Content.ReadAsStringAsync().Result;
                        resulMessage = JsonConvert.DeserializeObject<MessageResponseViewModel<RosterWM>>(result);

                        // Releasing.
                        response.Dispose();
                    }
                    else
                    {
                        // Reading Response.
                        string result = response.Content.ReadAsStringAsync().Result;
                        //responseObj.code = 602;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resulMessage;
        }
    }
}
