using DataAccessLib.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLib.DAO
{
    public class UserDAO
    {
        private dataContext data;
        
        public UserDAO()
        {
            data = new dataContext();
        }

        public Account GetUser(string email, string pwd)
        {
            return data.Accounts.Where(x=>x.Email == email && x.PasswordHash == pwd).FirstOrDefault();
        }

        public int Insert(Account user)
        {
            data.Accounts.Add(user);
            data.SaveChanges();
            return data.Accounts.Count(x => x.Email == user.Email);
        }

        public bool CheckEmail(string email)
        {
            return data.Accounts.Count(x=>x.Email == email) > 0;
        }
    }
}
