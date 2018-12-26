using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;


namespace Business
{
   public class User
    {
        private AspCrudDataContext _context;

        public List<DataAccess.User> Populate()
        {
            _context = new AspCrudDataContext();
            var list = from p in _context.Users
                       select p;

            return list.ToList();
        }

        public void Insert(string _firstName, string _lastName, string _remark)
        {
            _context = new AspCrudDataContext();

            DataAccess.User user = new DataAccess.User { FirstName = _firstName, LastName = _lastName, Remark = _remark };
            _context.Users.InsertOnSubmit(user);
            _context.SubmitChanges();
        }

        public void Save (DataAccess.User _user)
        {
            _context = new AspCrudDataContext();
            _context.Users.InsertOnSubmit(_user);
            _context.SubmitChanges();
        }

        public void Update( int _id, string _firstName, string _lastName, string _remark)
        {
            _context = new AspCrudDataContext();
            var _data = _context.Users.Where(p => p.Id == _id).First();

            _data.FirstName = _firstName;
            _data.LastName = _lastName;
            _data.Remark = _remark;

            _context.SubmitChanges();

        }
     
        public void Delete( int _id)
        {
            _context = new AspCrudDataContext();
            var _data = _context.Users.First(p => p.Id == _id);
            _context.Users.DeleteOnSubmit(_data);
            _context.SubmitChanges();
        }
        
    }
}
