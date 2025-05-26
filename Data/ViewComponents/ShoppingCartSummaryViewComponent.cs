using CinemaHub.Data.Cart;
using Microsoft.AspNetCore.Mvc;

namespace CinemaHub.Data.ViewComponents
{
    public class ShoppingCartSummaryViewComponent : ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;
        public ShoppingCartSummaryViewComponent (ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }
        public IViewComponentResult Invoke ()
        {
            var items = _shoppingCart.GetshoppingCartItems ();
            return View (items.Count);
        }
    }
}
