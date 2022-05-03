using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CampChetekRental.Models
{
    public class LoginSession
    {
        private const string IdKey = "id";
        private const string NameKey = "name";
        private const string UserRoles = "Admin";

        private ISession session { get; set; }
        public LoginSession(ISession session)
        {
            this.session = session;
        }

        public void SetName(string UserEmail = "friend")
        {
            session.SetString(NameKey, UserEmail);
        }

        public void SetUserRole(string UserRole = "Admin")
        {
            session.SetString(UserRoles, UserRole);
        }
        public void SetId(string IdKeys = "4")
        {
            session.SetString(IdKey, IdKeys);
        }

        public string GetUserRole() => session.GetString(UserRoles);
        public string GetName() => session.GetString(NameKey);


        public string GetId() => session.GetString(IdKey);

        public void RemoveMyUser()
        {
            session.Remove(NameKey);
            session.Remove(IdKey);
            session.Remove(UserRoles);
        }
    }
}
