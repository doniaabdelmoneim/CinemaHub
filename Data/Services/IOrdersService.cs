﻿using CinemaHub.Models;

namespace CinemaHub.Data.Services
{
    public interface IOrdersService
    {
        Task StoreOrderAsync(List<ShoppingCartItem>items,string userId,string userEmailAddress );
        Task<List<Order>> GetOrdersByUserIdAndRoleAsync (string userId, string userRole);
    }
}
