using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace Business
{
    public class Category
    {
        AspCrudDataContext _context;

        public List<DataAccess.Category> Populate()
        {
            _context = new AspCrudDataContext();
            var list = from p in _context.Categories
                       select p;

            return list.ToList();
        }
        public List<DataAccess.SubCategory>PouplateSubCateogory(int Id)
        {
            _context = new AspCrudDataContext();
            var list = from p in _context.SubCategories
                       where p.CategoryId == Id
                       select p;
            return list.ToList();
        }
    }
}
