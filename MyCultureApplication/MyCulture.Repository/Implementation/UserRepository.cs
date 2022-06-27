using Microsoft.EntityFrameworkCore;
using MyCulture.Domain.Identity;
using MyCulture.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCulture.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext context;
        private DbSet<MyCultureApplicationUser> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<MyCultureApplicationUser>();
        }


        public void Delete(MyCultureApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }

        public MyCultureApplicationUser Get(string id)
        {
            return entities
                .Include(r => r.UserCart)
                .Include("UserCart.CultureInCultureCarts")
                .Include("UserCart.CultureInCultureCarts.Culture")
                .SingleOrDefault(r => r.Id == id);
        }

        public IEnumerable<MyCultureApplicationUser> GetAll()
        {
            return entities.AsEnumerable();
        }

        public void Insert(MyCultureApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(MyCultureApplicationUser entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }
    }
}
