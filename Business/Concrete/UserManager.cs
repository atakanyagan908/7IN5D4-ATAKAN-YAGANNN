using Business.Abstract;
using DataAccess.Abstract;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {

            _userDal = userDal;

        }


        public void Add(User user)
        {
            _userDal.Add(user);
        }

        public void Delete(User user)
        {
            _userDal.Delete(user);
        }

        public List<User> GetAll()
        {
            List<User> users = _userDal.GetAll();
            return users;
        }

        public User GetById(int id)
        {
            User user = _userDal.Get(u => u.Id == id);
            return user;

        }

        public void Update(User user)
        {
            _userDal.Update(user);
        }
    }

}

