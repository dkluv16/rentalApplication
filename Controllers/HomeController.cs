using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CampChetekRental.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CampChetekRental.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private RentalAttribute context { get; set; }

        public HomeController(RentalAttribute ctx)
        {
            context = ctx;
        }
        [Route("/")]
        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return View();
        }
        public IActionResult WhoWeAre()
        {
            return View();
        }
        public IActionResult Services()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            var register = context.register.OrderBy(r => r.event_start).ToList();
            var blockDates = context.blockDates.OrderBy(b => b.startDate).ToList();
            List<string> listOfDates = new List<string>();
            //Adding the dates between start and end to a list, to bock dates
            foreach(var choice in register)
            {
               for(var dt = choice.event_start; dt <= choice.event_end; dt = dt.AddDays(1))
                {
                    listOfDates.Add(dt.ToString("MM-dd-yyyy"));
                }
            }
            foreach(var blockDate in blockDates)
            {
                for(var bd = blockDate.startDate; bd <= blockDate.endDate; bd = bd.AddDays(1))
                {
                    listOfDates.Add(bd.ToString("MM-dd-yyyy"));
                }
            }
           
            ViewBag.ListOfDates = listOfDates.ToList();
           
            ViewBag.Action = "Submit";
            ViewBag.GroupType = context.groupTypes.OrderBy(b => b.GroupTypeId).ToList();
            
            return View(new Register());
        }
        [HttpPost]
        public IActionResult Register(Register register)
        {
            if (ModelState.IsValid)
            {
                context.register.Add(register);
                context.SaveChanges();
                int id = register.registerId;

                var session = new RegisterSession(HttpContext.Session);
                session.SetRegisterId(id.ToString());
                session.SetStartDate(register.event_start.ToString());
                session.SetEndDate(register.event_end.ToString());

                return RedirectToAction("Needs", "Home");
            }
            else
            {
                var registered = context.register.OrderBy(r => r.event_start).ToList();
                var blockDates = context.blockDates.OrderBy(b => b.startDate).ToList();
                List<string> listOfDates = new List<string>();

                foreach (var choice in registered)
                {
                    for (var dt = choice.event_start; dt <= choice.event_end; dt = dt.AddDays(1))
                    {
                        listOfDates.Add(dt.ToString("MM-dd-yyyy"));
                    }
                }
                foreach (var blockDate in blockDates)
                {
                    for (var bd = blockDate.startDate; bd <= blockDate.endDate; bd = bd.AddDays(1))
                    {
                        listOfDates.Add(bd.ToString("MM-dd-yyyy"));
                    }
                }

                ViewBag.ListOfDates = listOfDates.ToList();
                ViewBag.Action = "Submit";
                ViewBag.GroupType = context.groupTypes.OrderBy(b => b.GroupTypeId).ToList();
                return View();
            }
        }
        [HttpGet]
        public IActionResult Activities()
        {
            ViewBag.Action = "Submit";
            var session = new RegisterSession(HttpContext.Session);
            var startDate = DateTime.Parse(session.GetStartDate()).Month;
            if (session.GetRegisterId() != null)
            {
                var timeOfYear = context.timeOfYears.OrderBy(t => t.startDate).ToList();
                var activity = context.activityTypes.Where(b => b.isActive == true).ToList();
                var listOfActivities = new List<ActivityType>();
                foreach (var choice in activity)
                {
                    int start = choice.TimeOfYear.startDate.Month;
                    int end = choice.TimeOfYear.endDate.Month;
                   //Checking each activity to make sure its available during the dates selected
                    if(start <= startDate & startDate <= end)
                    {
                        listOfActivities.Add(choice);
                    }
                }
                ViewBag.ActivityType = listOfActivities;
                return View(new ProgramChoice());
            }
            else
            {
                return RedirectToAction("Register", "Home");
            }
            
        }
        [HttpPost]
        public IActionResult Activities(List<int> SelectedActivity, List<int> SelectedNumber)
        {
            var session = new RegisterSession(HttpContext.Session);
            List<ProgramChoice> activiesChoice = new List<ProgramChoice>();
            int id = int.Parse(session.GetRegisterId());
            //Getting a list of selected activites and number of people attending and saving new ProgramChocie
            foreach (var choice in SelectedActivity.Zip(SelectedNumber, Tuple.Create))
            {
                activiesChoice.Add(new ProgramChoice { registerId = id, numberParticipating = choice.Item2, activityTypeId = choice.Item1 });
            }

            context.programs.AddRange(activiesChoice);
            context.SaveChanges();
            session.SetActvitiesComplete("COMPLETE");

            return RedirectToAction("SelectedNeeds", "Home");
        }
        [HttpGet]
        public IActionResult Meals()
        {
            
            var mealList = context.mealTypes.Where(l => l.isActive == true).ToList();
            var session = new RegisterSession(HttpContext.Session);
            //Breaking the mealType into three list, to display in view.
            ViewBag.BreakfastType = mealList.Where(s => s.type.Equals("Breakfast"));
            ViewBag.LunchType = mealList.Where(s => s.type.Equals("Lunch"));
            ViewBag.DinnerType = mealList.Where(s => s.type.Equals("Dinner"));         
            ViewBag.Action = "Submit";
            if (session.GetRegisterId() != null)
            {
                return View(new MealChoice());
            }
            else
            {
                return RedirectToAction("Register", "Home");
            }
        }
        [HttpPost]
        public IActionResult Meals(List<int> selectedMeal, List<int> selectedNumber)
        {
            var session = new RegisterSession(HttpContext.Session);
            List<MealChoice> mealChoices = new List<MealChoice>();
            int id = int.Parse(session.GetRegisterId());
            //Getting a list of selected meals and number of people attending and saving new MealChoice
            foreach (var choice in selectedMeal.Zip(selectedNumber, Tuple.Create))
            {
                mealChoices.Add(new MealChoice { registerId = id, numberOfMeals = choice.Item2, mealTypeId = choice.Item1 });
            }

            context.mealChoices.AddRange(mealChoices);
            context.SaveChanges();
            session.SetMealsComplete("COMPLETE");
            return RedirectToAction("SelectedNeeds", "Home");
        }
        [HttpGet]
        public IActionResult Housing()
        {
            var housingList = context.housingTypes.Where(l => l.isActive == true).ToList();
            var session = new RegisterSession(HttpContext.Session);
            ViewBag.Action = "Submit";
            //Breaking the housingType into three list, to display in view.
            ViewBag.BeddingType = context.beddingTypes.OrderBy(t => t.cost).ToList();
            ViewBag.MeetingType = housingList.Where(s => s.type.Equals("Meeting Area"));
            ViewBag.LodgingType = housingList.Where(s => s.type.Equals("Lodging"));
            ViewBag.LodgingTypeBedding = housingList.Where(s => s.type.Equals("LodgingWithBedding"));
            if (session.GetRegisterId() != null)
            {
                return View(new HousingChoice());

            }
            else
            {
                return RedirectToAction("Register", "Home");
            }
            }
        [HttpPost]
        public IActionResult Housing(List<int> lodging, List<int> selectedNumber, List<int> bedding)
        {
            var session = new RegisterSession(HttpContext.Session);
            List<HousingChoice> lodgingChoices = new List<HousingChoice>();
            int id = int.Parse(session.GetRegisterId());
            //Getting List Of Housing Options and Getting Selected Options.
            foreach (var choice in lodging.ZipThree(selectedNumber, bedding, Tuple.Create))
            {
                lodgingChoices.Add(new HousingChoice { registerId = id, numberHousing = choice.Item2, housingTypeId = choice.Item1, beddingTypeId = choice.Item3 });
            }
            context.housingChoices.AddRange(lodgingChoices);
            context.SaveChanges();
            session.SetHousingComplete("COMPLETE");
            return RedirectToAction("SelectedNeeds", "Home");
        }
        
        public IActionResult Needs()
        {
            ViewBag.Action = "Submit";
            return View();
        }
        [HttpPost]
        public IActionResult Needs(List<string> needs)
        {
            
            var session = new RegisterSession(HttpContext.Session);
            foreach (var choice in needs)
            {
                if (choice == "Housing")
                {
                    session.SetHousingChoice("Housing");                  
                }
                else if (choice == "Activities")
                {
                    session.SetActivitesChoice("Activities");                   
                }
                else if (choice == "Meals")
                {
                    session.SetMealChoice("Meals");                    
                }
            }
                return RedirectToAction("SelectedNeeds", "Home");     
        }
        public IActionResult SelectedNeeds()
        {
            var session = new RegisterSession(HttpContext.Session);
            string housing = session.GetHousingChoice();
            string activities = session.GetActivitesChoice();
            string meal = session.GetMealChoice();
            string housingComplete = session.GetHousingComplete();
            string activitiesComplete = session.GetActivitesComplete();
            string mealsComplete = session.GetMealsComplete();

            //Checking to make sure all selected areas are complete before displaying the quote view.
            if (session.GetRegisterId() != null)
            {
                if (housing != null && housingComplete == "COMPLETE" && activities == null && meal == null)
                {
                    return RedirectToAction("Quote", "Home");
                }
                else if (housing != null && housingComplete == "COMPLETE" && activities != null && activitiesComplete == "COMPLETE" && meal == null)
                {
                    return RedirectToAction("Quote", "Home");
                }
                else if (housing != null && housingComplete == "COMPLETE" && activities != null && activitiesComplete == "COMPLETE" && meal != null && mealsComplete == "COMPLETE")
                {
                    return RedirectToAction("Quote", "Home");
                }
                else if (meal != null && mealsComplete == "COMPLETE" && activities == null && housing == null)
                {
                    return RedirectToAction("Quote", "Home");
                }
                else if (meal != null && mealsComplete == "COMPLETE" && activities != null && activitiesComplete == "COMPLETE" && housing == null)
                {
                    return RedirectToAction("Quote", "Home");
                }
                else if (housing != null && housingComplete == "COMPLETE" && meal != null && mealsComplete == "COMPLETE" && activities == null)
                {
                    return RedirectToAction("Quote", "Home");
                }
                else if (activities != null && activitiesComplete == "COMPLETE" && housing == null && meal == null)
                {
                    return RedirectToAction("Quote", "Home");
                }
                else if (activities != null && activitiesComplete == "COMPLETE" && housing != null && housingComplete == "COMPLETE" && meal == null)
                {
                    return RedirectToAction("Quote", "Home");
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Register", "Home");
            }
        }

        [HttpGet]
        public IActionResult Quote(int id, List<int> totalHousing, List<int> totalMeals, List<int> totalActivities, Email email)
        {

            var session = new RegisterSession(HttpContext.Session);
            if (session.GetRegisterId() != null)
            {
                id = int.Parse(session.GetRegisterId());


                ViewBag.HousingType = context.housingTypes.OrderBy(b => b.housingTypeId).ToList();
                ViewBag.MealsType = context.mealTypes.OrderBy(c => c.mealTypeId).ToList();
                ViewBag.ActivityType = context.activityTypes.OrderBy(w => w.activityTypeId).ToList();
                ViewBag.BeddingType = context.beddingTypes.OrderBy(b => b.beddingTypeId).ToList();
                var activitys = context.programs.OrderBy(a => a.registerId).ToList();
                var meals = context.mealChoices.OrderBy(m => m.registerId).ToList();
                var register = context.register.OrderBy(r => r.registerId).ToList();
                var housing = context.housingChoices.OrderBy(h => h.registerId).ToList();
                int PetFee = 0;
                int grandTotal = 0;
           
                ViewBag.Housing = housing.Where(h => h.registerId.Equals(id));
                ViewBag.Activity = activitys.Where(a => a.registerId.Equals(id));
                ViewBag.Meals = meals.Where(m => m.registerId.Equals(id));
                ViewBag.Register = register.Where(r => r.registerId.Equals(id));

                foreach (var reg in register.Where(r => r.registerId.Equals(id)))
                {
                    if (reg.Pet == true)
                    {
                       PetFee = 100;
                    }
                }

                foreach (var house in housing.Where(h => h.registerId.Equals(id)))
                {
                    totalHousing.Add(house.numberHousing * house.HousingType.cost + house.BeddingType.cost);
                }
                foreach (var meal in meals.Where(m => m.registerId.Equals(id)))
                {
                    totalMeals.Add(meal.numberOfMeals * meal.MealType.cost);
                }
                foreach (var activity in activitys.Where(a => a.registerId.Equals(id)))
                {
                    totalActivities.Add(activity.numberParticipating * activity.ActivityType.cost);
                }
                grandTotal = totalMeals.Sum() + totalHousing.Sum() + totalActivities.Sum() + PetFee;
                ViewBag.PetFee = PetFee;
                ViewBag.totalMeals = totalMeals.Sum();
                ViewBag.totalHousing = totalHousing.Sum();
                ViewBag.totalActivity = totalActivities.Sum();
                ViewBag.total = grandTotal;
                ViewBag.Date = DateTime.Now.ToString("MM-dd-yyyy");
                email.buildEmail(register, id, totalMeals.Sum(), totalHousing.Sum(), totalActivities.Sum(), grandTotal, PetFee);
                return View();
            }
            else
            {
                return RedirectToAction("Register", "Home");
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

