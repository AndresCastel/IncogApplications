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

namespace IncogStuffControl.Services
{
    public class ServiceEmployee
    {
        public static async Task<MessageResponseViewModel<EmployeeVsRosterVM>> GetEmployee(EmployeeRegisterViewModel employer)
        {

            MessageResponseViewModel<EmployeeVsRosterVM> emplo = new MessageResponseViewModel<EmployeeVsRosterVM>();
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

                    var json = JsonConvert.SerializeObject(employer);
                    var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                    // Initialization.
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP GET
                    response = await client.PostAsync("api/Employee/get/", stringContent).ConfigureAwait(false);

                    // Verification
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.
                        string result = response.Content.ReadAsStringAsync().Result;
                        emplo = JsonConvert.DeserializeObject<MessageResponseViewModel<EmployeeVsRosterVM>>(result);

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

            return emplo;
        }

        public static async Task<MessageResponseViewModel<AllStuffVM>> GetAllStuff()
        {

            MessageResponseViewModel<AllStuffVM> Stuff = new MessageResponseViewModel<AllStuffVM>();
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

                    // Initialization.
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP GET
                    response = await client.GetAsync("api/Employee/Stuff").ConfigureAwait(false);

                    // Verification
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.
                        string result = response.Content.ReadAsStringAsync().Result;
                        Stuff = JsonConvert.DeserializeObject<MessageResponseViewModel<AllStuffVM>>(result);

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

            return Stuff;
        }

        public static async Task<List<TimesheetsReportViewModel>> GetTimesheetReport(FilterParametersRoster Filter)
        {

            List<TimesheetsReportViewModel> lst = new List<TimesheetsReportViewModel>();
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

                    var json = JsonConvert.SerializeObject(Filter);
                    var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                    // Initialization.
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP Post
                    response = await client.PostAsync("api/Reports/timesheet/", stringContent).ConfigureAwait(false);

                    // Verification
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.
                        string result = response.Content.ReadAsStringAsync().Result;
                        lst = JsonConvert.DeserializeObject<List<TimesheetsReportViewModel>>(result);

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

            return lst;
        }

        public static async Task<MessageResponseViewModel<EmployeeRegisterViewModel>> RegisterEmployeeStuff(EmployeeRegisterViewModel EmployeeStuff)
        {

            MessageResponseViewModel<EmployeeRegisterViewModel> resulMessage = new MessageResponseViewModel<EmployeeRegisterViewModel>();
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

                    var json = JsonConvert.SerializeObject(EmployeeStuff);
                    var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                    // Initialization.
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP GET
                    response = await client.PostAsync("api/Employee/register/", stringContent).ConfigureAwait(false);

                    // Verification
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.
                        string result = response.Content.ReadAsStringAsync().Result;
                        resulMessage = JsonConvert.DeserializeObject<MessageResponseViewModel<EmployeeRegisterViewModel>>(result);

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
