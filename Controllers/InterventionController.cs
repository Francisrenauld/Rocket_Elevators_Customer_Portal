using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using Rocket_Elevator_Customer_Portal.Models;
using System.Text;

namespace Rocket_Elevator_Customer_Portal.Controllers
{
    public class InterventionController : Controller
    {
        public class DataObject
        {
            public long Id { get; set; }
            public string? Email_Of_The_Company_Contact { get; set; }
            public List<Building>? Buildings { get; set; }
        }

        public class Building
        {
            public long Id { get; set; }
            public string? Full_Name_Of_The_Building_Administrator { get; set; }

            public List<Battery>? batteries { get; set; }

        }
        public class Battery
        {
            public long Id { get; set; }
            public string? Status { get; set; }
            public string? Certificate_Of_Operations { get; set; }
            public string? Notes { get; set; }
            public string? Type { get; set; }
            public DateTime? Date_Of_Commissioning { get; set; }
            public List<Column>? columns { get; set; }
        }
        public class Column
        {
            public long Id { get; set; }
            public string? Status { get; set; }
            public string? Type { get; set; }
            public string? Notes { get; set; }
            public List<Elevator>? elevators { get; set; }
        }
        public class Elevator
        {
            public long Id { get; set; }
            public string? Status { get; set; }
            public string? Model { get; set; }
            public string? Notes { get; set; }
            public DateTime? Date_Of_Last_Inspection { get; set; }
        }
        public class Employee
        {
            public long Id { get; set; }
            public string? Fisrt_Name { get; set; }
            public string? Last_Name { get; set; }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Intervention(InterventionModel intervention)
        {

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(intervention);
            var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");
            var urL = "https://rest-api-francis-renauld.herokuapp.com/api/Intervention";
            using var client = new HttpClient();
            var response = await client.PostAsync(urL, data);
            string result = response.Content.ReadAsStringAsync().Result;

            return View();
        }


        [Authorize]
        public IActionResult Intervention()
        {
            const string URL = "https://rest-api-francis-renauld.herokuapp.com/api/Customer_By_Email/";
            using (HttpClient client = new HttpClient())
            {

                var email = User.Identity.Name;
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(email).Result;
                ViewBag.FullUrl = URL + email;
                ViewBag.Email = email;
                
                if (response.IsSuccessStatusCode)
                {
                    var customer = response.Content.ReadAsAsync<DataObject>().Result;
                    ViewBag.customer = customer;
                    return View();
                }
                else
                {
                    return NotFound("Not found");
                }
            }
        }
    }
}
