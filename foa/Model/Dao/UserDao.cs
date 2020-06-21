using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using PagedList;

namespace Model.Dao
{
    public class UserDao
    {
        FoaDBContext db = null;

        public UserDao()
        {
            db = new FoaDBContext();
        }
        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.id;
        }
        public bool Update (User entity)
        {
            try
            {
                var user = db.Users.Find(entity.id);
                user.username = entity.username;
                user.email = entity.email;
                user.date = entity.date; db.SaveChanges();
            }
            catch(Exception ex)
            {
                return false;
            }
           
            return true;
        }
        public User ViewDetailByID(int id)
        {
            return db.Users.Find(id);
        }
        public User GetById(string userName)
        {
            return db.Users.SingleOrDefault(x=> x.username==userName);
        }
        public IEnumerable<User> lisAll(string searchString,int page, int pageSize)
        {
            IQueryable <User> model = db.Users;
            if(!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.username.Contains(searchString));
            }
            return db.Users.OrderByDescending(x=>x.id).ToPagedList(page,pageSize);
        }
        public bool Login (string userName, string password)
        {
            var result = db.Users.Count(x => x.username == userName && x.password == password);
            if(result > 0)
            {
                return true;
            }
            return false;
        }
 
        public bool Delete(int id)
        {
            var user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return true;
        }
        public bool ChangeStatus(int id)
        {
            var user = db.Users.Find(id);
            if(user.date=="0")
            {
                user.date = "1";
            } else
            {
                user.date = "0";
            }
            db.SaveChanges();
            return true;
        }
    }
}
