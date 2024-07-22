using Domin.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.IReprository;
using Infrastructure.Data;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.IReprository.ServicesRepository
{
    public class ServicesCategory : IServicesReprository<Category>
    {

        private readonly FreeBookDbContext _context;

        public ServicesCategory(FreeBookDbContext context)
        {
            _context = context;
        }
        public bool Delete(Guid Id)
        {
            try
            {
                var result = FindBy(Id);
                //result.CurrentStaut = 0;
                result.CurrentStaut = (int) Helper.eurrentStaut.Delete; 
                _context.Categories.Update(result);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Category FindBy(Guid Id)
        {
            try
            {
                return _context.Categories.FirstOrDefault(c => c.Id == Id && c.CurrentStaut > 0);
            }
            catch(Exception)
            {
                return null;
            }
        }

        public Category FindBy(string Name)
        {
            try
            {
                return _context.Categories.FirstOrDefault(c => c.Name.Equals(Name.Trim()) && c.CurrentStaut > 0);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Category> GetAll()
        {

            try
            {
                return _context.Categories.OrderBy(x => x.Name).Where(x => x.CurrentStaut > 0).AsNoTracking().ToList();
            }
            catch(Exception)
            {
                return null;
            }
        }

        public bool Save(Category model)
        {
            try
            {

                var result = FindBy(model.Id);
                //Create Or Add
                if (result == null)
                {
                    model.Id = Guid.NewGuid();
                    model.CurrentStaut = (int) Helper.eurrentStaut.Active;
                    _context.Categories.Add(model);
                }
                // Update
                else
                {
                    result.Name = model.Name;
                    result.Description = model.Description;
                    result.CurrentStaut = (int) Helper.eurrentStaut.Active;
                    _context.Categories.Update(result);
                }
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
