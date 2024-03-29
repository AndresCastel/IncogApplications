﻿using IncogStuffControl.Services.ViewModel;
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
        public static async Task<EmployeeRegisterViewModel> GetEmployee(string barcode)
        {

            EmployeeRegisterViewModel emplo = new EmployeeRegisterViewModel();
            try
            {


                // Posting.
                using (var client = new HttpClient())
                {
                    // Setting Base address.
                    client.BaseAddress = new Uri("https://localhost:44390");
                    

                   // Setting content type.
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Setting timeout.
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP GET
                    response = await client.GetAsync("api/Employee/get/" + barcode).ConfigureAwait(false);

                    // Verification
                    if (response.IsSuccessStatusCode)
                    {
                        // Reading Response.
                        string result = response.Content.ReadAsStringAsync().Result;
                        emplo = JsonConvert.DeserializeObject<EmployeeRegisterViewModel>(result);

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

        public static async Task<List<TimesheetsReportViewModel>> GetTimesheetReport()
        {

            List<TimesheetsReportViewModel> lst = new List<TimesheetsReportViewModel>();
            try
            {


                // Posting.
                using (var client = new HttpClient())
                {
                    // Setting Base address.
                    client.BaseAddress = new Uri("https://localhost:44390");


                    // Setting content type.
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Setting timeout.
                    client.Timeout = TimeSpan.FromSeconds(Convert.ToDouble(1000000));

                    // Initialization.
                    HttpResponseMessage response = new HttpResponseMessage();

                    // HTTP GET
                    response = await client.GetAsync("api/Employee/timesheet/").ConfigureAwait(false);

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

        public static async Task<MessageResponseViewModel> RegisterEmployeeStuff(EmployeeRegisterViewModel EmployeeStuff)
        {

            MessageResponseViewModel resulMessage = new MessageResponseViewModel();
            try
            {


                // Posting.
                using (var client = new HttpClient())
                {
                    // Setting Base address.
                    client.BaseAddress = new Uri("https://localhost:44390");


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
                        resulMessage = JsonConvert.DeserializeObject<MessageResponseViewModel>(result);

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
