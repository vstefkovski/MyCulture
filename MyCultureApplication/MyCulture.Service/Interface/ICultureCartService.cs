using MyCulture.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyCulture.Service.Interface
{
    public interface ICultureCartService
    {
        CultureCartDto getCultureCartInfo(string userId);
        bool deleteCultureFromCultureCart(string userId, Guid id);
        bool orderNow(string userId);
    }
}
