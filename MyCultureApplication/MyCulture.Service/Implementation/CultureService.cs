using MyCulture.Domain.DomainModels;
using MyCulture.Domain.DTO;
using MyCulture.Repository.Interface;
using MyCulture.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyCulture.Service.Implementation
{
    public class CultureService : ICultureService
    {

        private readonly IRepository<Culture> _cultureRepository;
        private readonly IRepository<CultureInCultureCart> _cultureInCultureCartRepository;
        private readonly IUserRepository _userRepository;

        public CultureService(IRepository<Culture> cultureRepository,
            IRepository<CultureInCultureCart> cultureInCultureCartRepository,
             IUserRepository userRepository)
        {
            _cultureRepository = cultureRepository;
            _cultureInCultureCartRepository = cultureInCultureCartRepository;
            _userRepository = userRepository;
        }


        public bool AddToCultureCart(AddToCultureCartDto item, string userID)
        {
            var loggedInUser = this._userRepository.Get(userID);
            var userCultureCart = loggedInUser.UserCart;
            if (item.CultureId != null && userCultureCart != null)
            {
                var culture = this.GetDetailsForCulture(item.CultureId);
                if (culture != null)
                {
                    CultureInCultureCart itemToAdd = new CultureInCultureCart
                    {
                        Culture = culture,
                        CultureId = culture.Id,
                        CultureCart = userCultureCart,
                        CultureCartId = userCultureCart.Id,
                        Quantity = item.Quantity

                    };
                    this._cultureInCultureCartRepository.Insert(itemToAdd);
                    return true;
                }
                return false;
            }
            return false;
        }

        public void CreateNewCulture(Culture c)
        {
            this._cultureRepository.Insert(c);
        }

        public void DeleteCulture(Guid id)
        {
            var culture = this.GetDetailsForCulture(id);
            this._cultureRepository.Delete(culture);
        }

        public List<Culture> GetAllCultures()
        {
            return this._cultureRepository.GetAll().ToList();
        }

        public AddToCultureCartDto GetCultureCartInfo(Guid? id)
        {
            var culture = this.GetDetailsForCulture(id);
            AddToCultureCartDto model = new AddToCultureCartDto
            {
                SelectedCulture = culture,
                CultureId = culture.Id,
                Quantity = 1
            };
            return model;
        }

        public Culture GetDetailsForCulture(Guid? id)
        {
            return this._cultureRepository.Get(id);
        }

        public void UpdateExistingCulture(Culture c)
        {
            this._cultureRepository.Update(c);
        }
    }
}
