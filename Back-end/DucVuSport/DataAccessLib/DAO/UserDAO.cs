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

        public tbUser GetUser(string email, string pwd)
        {
            return data.tbUsers.Where(x=>x.email == email && x.passwordHash == pwd).FirstOrDefault();
        }
        public void Register(string fullname, string email, string phonenumber, string pwd)
        {
        }

        public bool CheckEmail(string email)
        {
            return data.tbUsers.Count(x=>x.email == email) > 0;
        }
        public int Insert(tbUser user)
        {
            data.tbUsers.Add(user);
            data.SaveChanges();
            return data.tbUsers.Count(x=>x.email == user.email);
        }
    }
}
