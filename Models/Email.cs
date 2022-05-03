using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CampChetekRental.Models;
using System.Net.Mail;


namespace CampChetekRental.Models
{
    public class Email
    {
        public void buildEmail(List<Register> register, int id, int totalMeal, int totalHousing, int totalActivities, int grandTotal, int petFee)
        {
            foreach (var reg in register.Where(r => r.registerId.Equals(id)))
            {
            string bcc = "danael@campchetek.org";
            string to = reg.Email;
            string subject = "Camp Chetek Rental Quote #" + reg.registerId;
            string boby = reg.FirstName + " " + reg.LastName + "<br>" + reg.Address
                + "<br>" + reg.City + " " + reg.State + " " + "<br><br>" 
                + "<b>Your Quote: </b> <br>" + "Total Cost Meals: $" + totalMeal + "<br>Total Cost Housing: $"
                + totalHousing + "<br>Total Cost Activites: $" + totalActivities + "<br>Pet Fee: $" + petFee + "<br>"
                +"<br><b>Total: $" + grandTotal + "</b><br><hr>" + "Group Size: " + reg.GroupNumber + "<br>"
                + reg.event_start.ToString("MM/dd/yyyy") + "-" + reg.event_end.ToString("MM/dd/yyyy")
                + "<br><br>" + "Have a great day!<br><br>" + "Danael Kluver<br>" + "Office Manager";
            MailMessage mm = new MailMessage();
            mm.To.Add(to);
            mm.Subject = subject;
            mm.Body = boby;
            mm.From = new MailAddress("campchetekhousing@gmail.com");
            mm.Bcc.Add(bcc);
            mm.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com");
            smtp.Port = 587;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Credentials = new System.Net.NetworkCredential("campchetekhousing@gmail.com", "cccc1944AD");
            smtp.Send(mm);
            }

        }
    }
}
