using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Model.RequestModels;

namespace Services.Interfaces
{
    public interface IUserService
    {
        public  Task<UserModel> getUserByID(string id);
        public  Task<IEnumerable<UserModel>> getAllUsers();
    }
}
