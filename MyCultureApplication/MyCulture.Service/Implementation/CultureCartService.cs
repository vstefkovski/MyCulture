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
    public class CultureCartService : ICultureCartService
    {
        private readonly IRepository<CultureCart> _cultureCartRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<CultureInOrder> _cultureInOrderRepository;
        private readonly IUserRepository _userRepository;

        public CultureCartService(IRepository<CultureCart> cultureCartRepository,
            IRepository<Order> orderRepository,
            IRepository<CultureInOrder> cultureInOrderRepository,
            IUserRepository userRepository)
        {
            _cultureCartRepository = cultureCartRepository;
            _orderRepository = orderRepository;
            _cultureInOrderRepository = cultureInOrderRepository;
            _userRepository = userRepository;
        }

        public bool deleteCultureFromCultureCart(string userId, Guid id)
        {
            if (!string.IsNullOrEmpty(userId) && id != null)
            {
                //Select * from Users Where Id LIKE userId

                var loggedInUser = this._userRepository.Get(userId);

                var userCultureCart = loggedInUser.UserCart;

                var itemToDelete = userCultureCart.CultureInCultureCarts.Where(z => z.CultureId.Equals(id)).FirstOrDefault();

                userCultureCart.CultureInCultureCarts.Remove(itemToDelete);

                this._cultureCartRepository.Update(userCultureCart);

                return true;
            }

            return false;
        }

        public CultureCartDto getCultureCartInfo(string userId)
        {
            var loggedInUser = this._userRepository.Get(userId);

            var userCultureCart = loggedInUser.UserCart;

            var AllCultures = userCultureCart.CultureInCultureCarts.ToList();

            var allCulturePrice = AllCultures.Select(z => new
            {
                CulturePrice = z.Culture.CulturePrice,
                Quanitity = z.Quantity
            }).ToList();

            var totalPrice = 0;


            foreach (var item in allCulturePrice)
            {
                totalPrice += item.Quanitity * item.CulturePrice;
            }


            CultureCartDto ccDto = new CultureCartDto
            {
                Cultures = AllCultures,
                TotalPrice = totalPrice
            };


            return ccDto;
        }

        public bool orderNow(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                //Select * from Users Where Id LIKE userId

                var loggedInUser = this._userRepository.Get(userId);

                var userCultureCart = loggedInUser.UserCart;

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    User = loggedInUser,
                    UserId = userId
                };

                this._orderRepository.Insert(order);

                List<CultureInOrder> cultureInOrders = new List<CultureInOrder>();

                var result = userCultureCart.CultureInCultureCarts.Select(z => new CultureInOrder
                {
                    Id = Guid.NewGuid(),
                    CultureId = z.Culture.Id,
                    SelectedCulture = z.Culture,
                    OrderId = order.Id,
                    UserOrder = order
                }).ToList();

                cultureInOrders.AddRange(result);

                foreach (var item in cultureInOrders)
                {
                    this._cultureInOrderRepository.Insert(item);
                }

                loggedInUser.UserCart.CultureInCultureCarts.Clear();

                this._userRepository.Update(loggedInUser);

                return true;
            }
            return false;
        }
    }
}
