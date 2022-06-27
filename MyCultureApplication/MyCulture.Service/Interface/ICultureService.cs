using MyCulture.Domain.DomainModels;
using MyCulture.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCulture.Service.Interface
{
    public interface ICultureService
    {
        List<Culture> GetAllCultures();
        Culture GetDetailsForCulture(Guid? id);
        void CreateNewCulture(Culture c);
        void UpdateExistingCulture(Culture c);
        AddToCultureCartDto GetCultureCartInfo(Guid? id);
        void DeleteCulture(Guid id);
        bool AddToCultureCart(AddToCultureCartDto item, string userID);
    }
}
