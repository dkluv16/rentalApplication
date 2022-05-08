using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CampChetekRental.Models;

namespace CampChetekRental.Areas.Admin.Controllers
{
   
    [Route("[area]/[controller]/[action]")]
    [Route("[area]/[controller]/[action]/{id?}")]
    [Area("Admin")]
    public class EventsController : Controller
    {
        private RentalAttribute context { get; set; }

        public EventsController(RentalAttribute ctx)
        {
            context = ctx;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            var session = new LoginSession(HttpContext.Session);
            string check = session.GetName();
           
            var register = from r in context.register select r;
            ViewBag.Activity = context.programs.OrderBy(a => a.programChoiceId).ToList();
            ViewBag.Housing = context.housingChoices.OrderBy(h => h.housingChoiceId).ToList();
            ViewBag.Meals = context.mealChoices.OrderBy(m => m.registerId).ToList();
            ViewBag.MealsType = context.mealTypes.OrderBy(c => c.mealTypeId).ToList();
            ViewBag.GroupType = context.groupTypes.OrderBy(g => g.TypeName).ToList();
            ViewBag.HousingType = context.housingTypes.OrderBy(b => b.housingTypeId).ToList();
            ViewBag.ActivityType = context.activityTypes.OrderBy(w => w.activityTypeId).ToList();

            if (check != null)
            {
                return View(register.ToList());
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpGet]
        public IActionResult Detail(int id)
        {
            

            ViewBag.Action = "Edit";
            var session = new LoginSession(HttpContext.Session);
            string check = session.GetName();
            var register = context.register.Find(id);
            ViewBag.GroupType = context.groupTypes.OrderBy(b => b.GroupTypeId).ToList();
            if (check != null)
            {
                return View(register);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        public IActionResult Detail(Register register)
        {
                ViewBag.GroupType = context.groupTypes.OrderBy(b => b.GroupTypeId).ToList();
                context.register.Update(register);
                context.SaveChanges();
                return RedirectToAction("Dashboard", "Events");   
        }
        [HttpGet]
        public IActionResult MealsEdit(int id)
        {
            var meal = from m in context.mealChoices select m;
            ViewBag.RegisterId = id;
            ViewBag.Action = "Edit";
            ViewBag.MealTypes = context.mealTypes.OrderBy(m => m.mealDescription).ToList();
            return View(meal.ToList());
        }
        [HttpPost]
        public IActionResult MealsEdit(List<int> selectedMealsId, List<int> selectedMeals, List<int> numberMeals, int id)
        {
            //Zip three list together, allowing to update multiple values at once in the mealChoices table.
            List<MealChoice> mealChoices = new List<MealChoice>();
            foreach (var meal in selectedMealsId.ZipThree(selectedMeals, numberMeals, Tuple.Create))
            {
                mealChoices.Add(new MealChoice { mealChoiceId = meal.Item1, mealTypeId = meal.Item2, numberOfMeals = meal.Item3, registerId = id });
            }
            this.context.mealChoices.UpdateRange(mealChoices);
            context.SaveChanges();

            return RedirectToAction("MealsEdit", "Events", id);
        }
        [HttpGet]
        public IActionResult HousingEdit(int id)
        {
            var housing = from h in context.housingChoices select h;
            ViewBag.RegisterId = id;
            ViewBag.Action = "Edit";
            ViewBag.HousingType = context.housingTypes.OrderBy(h => h.building).ToList();
            ViewBag.BeddingType = context.beddingTypes.OrderBy(b => b.beddingTypeId).ToList();
            return View(housing.ToList());
        }
        [HttpPost]
        public IActionResult HousingEdit(List<int> selectedHousingId, List<int> selectedHousing, List<int> numberHousing, List<int> selectedBedding, int id)
        {
            //Zip three list together, allowing to update multiple values at once in the housingChoices table.
            List<HousingChoice> housingChoices = new List<HousingChoice>();
            foreach (var choice in selectedHousingId.ZipFour(selectedHousing, numberHousing, selectedBedding, Tuple.Create))
            {
                    housingChoices.Add(new HousingChoice { housingChoiceId = choice.Item1, housingTypeId = choice.Item2, numberHousing = choice.Item3, beddingTypeId = choice.Item4, registerId =  id});
            }
                this.context.housingChoices.UpdateRange(housingChoices);
                context.SaveChanges();
            
            return RedirectToAction("HousingEdit", "Events", id);
        }
        [HttpGet]
        public IActionResult ActvitiesEdit(int id)
        {
            var programChoice = from a in context.programs select a;
            ViewBag.RegisterId = id;
            ViewBag.Action = "Edit";
            ViewBag.ActivityType = context.activityTypes.OrderBy(a => a.activityTypeId).ToList();
            return View(programChoice.ToList());
        }
        [HttpPost]
        public IActionResult ActvitiesEdit(List<int> selectedProgramId, List<int> selectedActivity, List<int> numberActivity, int id)
        {
            //Zip three list together, allowing to update multiple values at once in the programChoice table.
            List<ProgramChoice> programChoices = new List<ProgramChoice>();
            foreach (var activity in selectedProgramId.ZipThree(selectedActivity, numberActivity, Tuple.Create))
            {
                programChoices.Add(new ProgramChoice { programChoiceId = activity.Item1, activityTypeId = activity.Item2, numberParticipating = activity.Item3, registerId = id });
            }
            this.context.programs.UpdateRange(programChoices);
            context.SaveChanges();

            return RedirectToAction("ActvitiesEdit", "Events", id);
        }
        [HttpGet]
        public IActionResult BlockDates()
        {
            var blockDates = from b in context.blockDates select b; 
            return View(blockDates.ToList());
        }
        [HttpGet]
        public IActionResult AddBlockDates()
        {
            var blockDates = from b in context.blockDates select b;
            ViewBag.Action = "Add";
            return View("EditBlockDates", new BlockDates());
        }
        [HttpPost]
        public IActionResult AddBlockDates(BlockDates blockDates)
        {
                if (blockDates.blockDatesId == 0)
                {
                    context.blockDates.Add(blockDates);
                }
                else
                {
                    context.blockDates.Update(blockDates);
                }
                context.SaveChanges();
                return RedirectToAction("BlockDates", "Events");
        }
        [HttpGet]
        public IActionResult EditBlockDates(int id)
        {
            ViewBag.Action = "Edit";
            var blockDate = context.blockDates.Find(id);
            return View(blockDate);
        }
        public IActionResult DeleteBlockDate(int id)
        {
            var removeDate = context.blockDates.Find(id);
            context.Remove(removeDate);
            context.SaveChanges();
            return RedirectToAction("BlockDates", "Events");
        }
        [HttpGet]
        public IActionResult ActvitiesType()
        {
            var actvitiesDate = from a in context.activityTypes select a;
            ViewBag.TimeOfYear = context.timeOfYears.OrderBy(a => a.timeOfYearId).ToList();
            return View(actvitiesDate.ToList());
        }
        public IActionResult EditActivitiesType(int id)
        {
            ViewBag.Action = "Edit";
            var activitiesType = context.activityTypes.Find(id);
            ViewBag.TimeOfYear = context.timeOfYears.OrderBy(a => a.timeOfYearId).ToList();
            return View(activitiesType);
        }
        [HttpPost]
        public IActionResult AddActivitiesType(ActivityType activityType)
        {
            if (activityType.activityTypeId == 0)
            {
                context.activityTypes.Add(activityType);
            }
            else
            {
                activityType.dateUpdated = DateTime.Now;
                context.activityTypes.Update(activityType);
            }
            context.SaveChanges();
            return RedirectToAction("ActvitiesType", "Events");
        }
        [HttpGet]
        public IActionResult AddActivitiesType()
        {
            var activitiesType = from a in context.activityTypes select a;
            ViewBag.TimeOfYear = context.timeOfYears.OrderBy(a => a.timeOfYearId).ToList();
            ViewBag.Action = "Add";
            return View("EditActivitiesType", new ActivityType());
        }
        public IActionResult DeleteActivitiesType(int id)
        {
            var removeActivitie = context.activityTypes.Find(id);
            context.Remove(removeActivitie);
            context.SaveChanges();
            return RedirectToAction("ActvitiesType", "Events");
        }
        [HttpGet]
        public IActionResult MealsType()
        {
            var mealType = from a in context.mealTypes select a;
            return View(mealType.ToList());
        }
        [HttpGet]
        public IActionResult EditMealType(int id)
        {
            ViewBag.Action = "Edit";
            var mealType = context.mealTypes.Find(id);
            return View(mealType);
        }
        [HttpGet]
        public IActionResult AddMealsType()
        {
            var mealsType = from m in context.mealTypes select m;
            ViewBag.Action = "Add";
            return View("EditMealType", new MealType());
        }
        [HttpPost]
        public IActionResult AddMealsType(MealType mealType)
        {
            if (mealType.mealTypeId == 0)
            {
                context.mealTypes.Add(mealType);
            }
            else
            {
                mealType.dateUpdated = DateTime.Now;
                context.mealTypes.Update(mealType);
            }
            context.SaveChanges();
            return RedirectToAction("MealsType", "Events");
        }
        public IActionResult DeleteMealType(int id)
        {
            var removeMealType = context.mealTypes.Find(id);
            context.Remove(removeMealType);
            context.SaveChanges();
            return RedirectToAction("MealsType", "Events");
        }
        public IActionResult HousingType()
        {
            var mealTypes = from h in context.housingTypes select h;
            return View(mealTypes.ToList());
        }
        [HttpGet]
        public IActionResult EditHousingType(int id)
        {
            ViewBag.Action = "Edit";
            var housisngType = context.housingTypes.Find(id);
            return View(housisngType);
        }
        [HttpPost]
        public IActionResult AddHousingType(HousingType housingType)
        {
            if (housingType.housingTypeId == 0)
            {
                context.housingTypes.Add(housingType);
            }
            else
            {
                housingType.dateUpdated = DateTime.Now;
                context.housingTypes.Update(housingType);
            }
            context.SaveChanges();
            return RedirectToAction("HousingType", "Events");
        }
        public IActionResult AddHousingType()
        {
            var housingType = from h in context.housingTypes select h;
            ViewBag.Action = "Add";
            return View("EditHousingType", new HousingType());
        }
        public IActionResult DeleteHousingType(int id)
        {
            var removeHousingType = context.housingTypes.Find(id);
            context.Remove(removeHousingType);
            context.SaveChanges();
            return RedirectToAction("HousingType", "Events");
        }
        public IActionResult Users()
        {
            var session = new LoginSession(HttpContext.Session);
            ViewBag.UserRole = context.userRoles.OrderBy(a => a.userRoleId).ToList();
            if (session.GetUserRole() == "1")
            {
                var user = from h in context.users select h;
                return View(user.ToList());
            }
            else
            {
                var user = context.users.Where(a => a.userId.Equals(int.Parse(session.GetId()))).ToList();
                return View(user.ToList());
            }
            
        }
        [HttpGet]
        public IActionResult EditUser(int id)
        {
            ViewBag.Action = "Edit";
            var user = context.users.Find(id);
            ViewBag.UserRole = context.userRoles.OrderBy(a => a.userRoleId).ToList();
            return View(user);
        }
        [HttpPost]
        public IActionResult AddUser(User user)
        {
            if (user.userId == 0)
            {
                context.users.Add(RentalAttribute.CreateNewUser(user.userId, user.Password, user.FirstName, user.LastName, user.Email, user.userRoleId));
            }
            else
            {
                context.users.Update(RentalAttribute.CreateNewUser(user.userId, user.Password, user.FirstName, user.LastName, user.Email, user.userRoleId));
            }
            context.SaveChanges();
            return RedirectToAction("Users", "Events");
        }
        [HttpGet]
        public IActionResult AddUser()
        {
            var user = from u in context.users select u;
            ViewBag.UserRole = context.userRoles.OrderBy(a => a.userRoleId).ToList();
            ViewBag.Action = "Add";
            return View("EditUser", new User());
        }
        public IActionResult DeleteUser(int id)
        {
            var removeUser = context.users.Find(id);
            context.Remove(removeUser);
            context.SaveChanges();
            return RedirectToAction("Users", "Events");
        }
        [HttpGet]
        public IActionResult EditTimeOfYear(int id)
        {
            ViewBag.Action = "Edit";
            var timeOfYear = context.timeOfYears.Find(id);
            return View(timeOfYear);
        }
        [HttpGet]
        public IActionResult AddTimeOfYear()
        {
            ViewBag.Action = "Add";
            return View("EditTimeOfYear", new TimeOfYear());
        }
        [HttpPost]
        public IActionResult AddTimeOfYear(TimeOfYear timeOfYear)
        {
            if (timeOfYear.timeOfYearId == 0)
            {
                context.timeOfYears.Add(timeOfYear);
            }
            else
            {
                context.timeOfYears.Update(timeOfYear);
            }
            context.SaveChanges();
            return RedirectToAction("TimeOfYear", "Events");
        }
        public IActionResult DeleteTimeOfYear(int id)
        {
            var removeTimeOfYear = context.timeOfYears.Find(id);
            context.Remove(removeTimeOfYear);
            context.SaveChanges();
            return RedirectToAction("TimeOfYear", "Events");
        }
        public IActionResult TimeOfYear()
        {
            var timeOfYear = from t in context.timeOfYears select t;
            return View(timeOfYear.ToList());
        }
        public IActionResult DeleteRegistration(int id)
        {
            var removeRegistration = context.register.Find(id);
            var removeMeal = context.mealChoices.Find(id);
            var removeActivity = context.programs.Find(id);
            var removeHousing = context.housingChoices.Find(id);

            context.register.RemoveRange(removeRegistration);
            if (removeMeal != null)
            {
                context.mealChoices.RemoveRange(removeMeal);
            }
            if (removeActivity != null)
            {
                context.programs.RemoveRange(removeActivity);
            }
            if (removeHousing != null) {
                context.housingChoices.RemoveRange(removeHousing);
            }
            
            context.SaveChanges();

            return RedirectToAction("Dashboard", "Events");

        }

    }
    }
public static class ZipThreeList
{
    //Zips three list together
    public static IEnumerable<TResult> ZipThree<T1, T2, T3, TResult>(
        this IEnumerable<T1> source,
        IEnumerable<T2> second,
        IEnumerable<T3> third,
        Func<T1, T2, T3, TResult> func)
    {
        using (var e1 = source.GetEnumerator())
        using (var e2 = second.GetEnumerator())
        using (var e3 = third.GetEnumerator())
        {
            while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext())
                yield return func(e1.Current, e2.Current, e3.Current);
        }
    }
    //Zips four list together
    public static IEnumerable<TResult> ZipFour<T1, T2, T3, T4, TResult>(
       this IEnumerable<T1> source,
       IEnumerable<T2> second,
       IEnumerable<T3> third,
       IEnumerable<T4> fourth,
       Func<T1, T2, T3, T4, TResult> func)
    {
        using (var e1 = source.GetEnumerator())
        using (var e2 = second.GetEnumerator())
        using (var e3 = third.GetEnumerator())
        using (var e4 = fourth.GetEnumerator())
        {
            while (e1.MoveNext() && e2.MoveNext() && e3.MoveNext() && e4.MoveNext())
                yield return func(e1.Current, e2.Current, e3.Current, e4.Current);
        }
    }
}
