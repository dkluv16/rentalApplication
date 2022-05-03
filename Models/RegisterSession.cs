using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CampChetekRental.Models
{
    public class RegisterSession
    {
        private const string RegisterId = "registerId";
        private const string StartDates = "startDate";
        private const string EndDates = "endDate";
        private const string HousingComplete = "housingComplete";
        private const string ActivitiesComplete = "activitesComplete";
        private const string MealsComplete = "mealsComplete";
        private const string MealChoice = "mealChoice";
        private const string HousingChoice = "housingChoice";
        private const string ActivitiesChoice = "activitiesChoice";

        private ISession registerSession { get; set; }

        public RegisterSession(ISession session)
        {
            this.registerSession = session;
        }
        public void SetMealChoice(string mealChoice = "mealChoice")
        {
            registerSession.SetString(MealChoice, mealChoice);
        }
        public void SetHousingChoice(string housingChoice = "housingChoice")
        {
            registerSession.SetString(HousingChoice, housingChoice);
        }
        public void SetActivitesChoice(string activitesChoice = "activitesChoice")
        {
            registerSession.SetString(ActivitiesChoice, activitesChoice);
        }
        public void SetHousingComplete(string housingComplete = "complete")
        {
            registerSession.SetString(HousingComplete, housingComplete);
        }
        public void SetActvitiesComplete(string activitiesComplete = "complete")
        {
            registerSession.SetString(ActivitiesComplete, activitiesComplete);
        }
        public void SetMealsComplete(string mealsComplete = "complete")
        {
            registerSession.SetString(MealsComplete, mealsComplete);
        }
        public void SetRegisterId(string registerId = "0")
        {
            registerSession.SetString(RegisterId, registerId);
        }
        public void SetStartDate(string startDate = "01/01/1990")
        {
            registerSession.SetString(StartDates, startDate);
        }
        public void SetEndDate(string endDate = "02/02/1990")
        {
            registerSession.SetString(EndDates, endDate);
        }

        public string GetRegisterId() => registerSession.GetString(RegisterId);
        public string GetStartDate() => registerSession.GetString(StartDates);
        public string GetEndDate() => registerSession.GetString(EndDates);
        public string GetMealsComplete() => registerSession.GetString(MealsComplete);
        public string GetActivitesComplete() => registerSession.GetString(ActivitiesComplete);
        public string GetHousingComplete() => registerSession.GetString(HousingComplete);
        public string GetHousingChoice() => registerSession.GetString(HousingChoice);
        public string GetMealChoice() => registerSession.GetString(MealChoice);
        public string GetActivitesChoice() => registerSession.GetString(ActivitiesChoice);


        public void RemoveRegisterUser()
        {
            registerSession.Remove(RegisterId);
            registerSession.Remove(StartDates);
            registerSession.Remove(EndDates); 
        }
    }
}
