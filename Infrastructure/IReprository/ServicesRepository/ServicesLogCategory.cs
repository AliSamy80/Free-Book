using Domin.Entity;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IReprository.ServicesRepository
{
    public class ServicesLogCategory : IServicesReprositoryLog<LogCategory>
    {

        private readonly FreeBookDbContext _context;



        public ServicesLogCategory(FreeBookDbContext context)
        {
            _context = context;
        }



        public bool Delete(Guid Id, Guid UserId)
        {
            try
            {
                var logCategory = new LogCategory
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Delete,
                    Date = DateTime.Now,
                    UserId = UserId,
                    CategoryId = Id
                };
                _context.LogCategories.Add(logCategory);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteLog(Guid Id)
        {
            try
            {
                var result = FindBy(Id);
                if (result != null)
                {
                    _context.LogCategories.Remove(result);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public LogCategory FindBy(Guid Id)
        {
           try
            {
                return _context.LogCategories.Include(x => x.Category).FirstOrDefault(c => c.Id == Id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<LogCategory> GetAll()
        {
            try
            {
                return _context.LogCategories.Include(x => x.Category).OrderByDescending(c => c.Date).AsNoTracking().ToList();
            }
            catch(Exception)
            {
                return null;
            }
        }

        public bool Save(Guid Id, Guid UserId)
        {
            try
            {
                var logCategory = new LogCategory
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Save,
                    Date = DateTime.Now,
                    UserId = UserId,
                    CategoryId = Id
                };
                _context.LogCategories.Add(logCategory);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Guid Id, Guid UserId)
        {
            try
            {
                var logCategory = new LogCategory
                {
                    Id = Guid.NewGuid(),
                    Action = Helper.Update,
                    Date = DateTime.Now,
                    UserId = UserId,
                    CategoryId = Id
                };
                _context.LogCategories.Add(logCategory);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
