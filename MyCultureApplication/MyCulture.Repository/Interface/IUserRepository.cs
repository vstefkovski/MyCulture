using MyCulture.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCulture.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<MyCultureApplicationUser> GetAll();
        MyCultureApplicationUser Get(string id);
        void Insert(MyCultureApplicationUser entity);
        void Update(MyCultureApplicationUser entity);
        void Delete(MyCultureApplicationUser entity);
    }
}
