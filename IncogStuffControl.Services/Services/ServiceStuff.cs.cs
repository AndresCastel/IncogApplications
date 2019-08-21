using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncogStuffControl.Services.Services
{
    public class ServiceStuff
    {
        //    public static async Task<EmployeeViewModel> GetEmployee(string barcode)
        //    {

        //        EmployeeViewModel emplo = new EmployeeViewModel();
        //        try
        //        {


        //            // Posting.
        //            using (var client = new HttpClient())
        //            {
        //                // Setting Base address.
        //                client.BaseAddress = new Uri("https://localhost:44390");


        //                // Setting content type.
        //                client.DefaultRequestHeaders.Accept.Clear();
        //                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //                // Setting timeout.
        //                client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

        //                // Initialization.
        //                HttpResponseMessage response = new HttpResponseMessage();

        //                // HTTP GET
        //                response = await client.GetAsync("api/Employee/get/" + barcode).ConfigureAwait(false);

        //                // Verification
        //                if (response.IsSuccessStatusCode)
        //                {
        //                    // Reading Response.
        //                    string result = response.Content.ReadAsStringAsync().Result;
        //                    emplo = JsonConvert.DeserializeObject<EmployeeViewModel>(result);

        //                    // Releasing.
        //                    response.Dispose();
        //                }
        //                else
        //                {
        //                    // Reading Response.
        //                    string result = response.Content.ReadAsStringAsync().Result;
        //                    //responseObj.code = 602;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }

        //        return emplo;
        //    }
    }
}
