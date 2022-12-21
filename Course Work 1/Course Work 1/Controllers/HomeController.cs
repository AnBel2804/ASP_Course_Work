using Course_Work_1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Course_Work_1.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        static IConfiguration Configuration;

        AppDbContext context = new AppDbContext(Configuration);
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Table(string searchString, SortState sortOrder = SortState.ManufacturerAsc)
        {
            List<MainTableResult> results = new List<MainTableResult>();
            var result = from info in context.GeneralInformation
                         join manufacturer in context.Manufacturer on info.ManufacturerId equals manufacturer.ManufacturerId
                         join model in context.Model on info.ModelId equals model.ModelId
                         join registration in context.Registration on info.RegistrationId equals registration.RegistrationId
                         select new
                         {
                             Manufacturer = manufacturer.ManufacturerName,
                             Model = model.ModelName,
                             NumberOfCar = registration.RegistrationNumber
                         };

            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(s => s.Manufacturer.Contains(searchString)
                                       || s.Model.Contains(searchString) || s.NumberOfCar.Contains(searchString));
            }
            ViewBag.ManufacturerSort = sortOrder == SortState.ManufacturerAsc ? SortState.ManufacturerDesc : SortState.ManufacturerAsc;
            ViewBag.ModelSort = sortOrder == SortState.ModelAsc ? SortState.ModelDesc : SortState.ModelAsc;
            ViewBag.RegistrationSort = sortOrder == SortState.RegistrationAsc ? SortState.RegistrationDesc : SortState.RegistrationAsc;
                
            result = sortOrder switch
            {
                SortState.ManufacturerDesc => result.OrderByDescending(s => s.Manufacturer),
                SortState.ModelAsc => result.OrderBy(s => s.Model),
                SortState.ModelDesc => result.OrderByDescending(s => s.Model),
                SortState.RegistrationAsc => result.OrderBy(s => s.NumberOfCar),
                SortState.RegistrationDesc => result.OrderByDescending(s => s.NumberOfCar),
                _ => result.OrderBy(s => s.Manufacturer)
            };

            foreach (var item in result)
            {
                MainTableResult element = new MainTableResult();
                element.Manufacturer = item.Manufacturer;
                element.Model = item.Model;
                element.NumberOfCar = item.NumberOfCar;
                results.Add(element);
            }

            return View(results);
        }
        public IActionResult Statistic()
        {
            return View();
        }
    }
}
