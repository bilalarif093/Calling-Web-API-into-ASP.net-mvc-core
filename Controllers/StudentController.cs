using System.Text.Json;
using ERP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP.Controllers
{
    public class StudentController : Controller
    {
        private string url = "https://localhost:7115/api/Students/";
        private HttpClient client = new HttpClient();
        public async Task<IActionResult> Index()
        {
            List<Student> students = null;
            HttpResponseMessage rep = await client.GetAsync(url);
            if (rep.IsSuccessStatusCode) {
                string data = await rep.Content.ReadAsStringAsync();
                students = JsonSerializer.Deserialize<List<Student>>(data);
            }
            return View(students);
        }

        [HttpGet]
        public IActionResult create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> create(Student std)
        {
            var response = await client.PostAsJsonAsync(url, std);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            // Optional: handle API error
            ModelState.AddModelError("", "Unable to create student. API error.");
            return View();           
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Student student = null;
            var response = await client.GetAsync(url+id);
            if (response.IsSuccessStatusCode)                
            {
                string data = await response.Content.ReadAsStringAsync();
                student = JsonSerializer.Deserialize<Student>(data);
            }
            return View(student);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Student std)
        {
            var response = await client.PutAsJsonAsync(url+$"{std.Id}", std);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            // Optional: handle API error
            ModelState.AddModelError("", "Unable to Edit student. API error.");
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Delete(Student std)
        {
            var response = await client.DeleteAsync(url + std.Id);
            if (response.IsSuccessStatusCode)
            {                
                ViewData["ErrorMsg"] = "data deleted successfully!";
                return RedirectToAction("Index");
            }
            return View(std.Id);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Student student = null;
            var response = await client.GetAsync(url + id);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                student = JsonSerializer.Deserialize<Student>(data);
            }
            return View(student);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            Student student = null;
            var response = await client.GetAsync(url + id);
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                student = JsonSerializer.Deserialize<Student>(data);
            }
            return View(student);
        }

    }
}
