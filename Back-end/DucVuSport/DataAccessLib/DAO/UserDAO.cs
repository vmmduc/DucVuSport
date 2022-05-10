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

        public tbUser Login(string email, string pwd)
        {
            return data.tbUsers.Where(x=>x.email == email && x.passwordHash == pwd).FirstOrDefault();
        } 
        public void Register(string fullname, string email, string phonenumber, string pwd)
        {
        }
    }
}
