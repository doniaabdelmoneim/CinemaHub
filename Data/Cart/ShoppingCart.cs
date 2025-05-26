using CinemaHub.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemaHub.Data.Cart
{
    public class ShoppingCart
    {
        //this class to add  and remove data from cart

        public AppDbContext _context;
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }


        public ShoppingCart (AppDbContext context) => _context = context;
        //to open session
        public static ShoppingCart GetShoppingCart (IServiceProvider service)
        {
            // Get IHttpContextAccessor and AppDbContext safely
            var httpContextAccessor = service.GetService<IHttpContextAccessor> ();
            var context = service.GetService<AppDbContext> ();

            if (httpContextAccessor?.HttpContext == null || context == null)
            {
                throw new InvalidOperationException ("Cannot access HttpContext or AppDbContext.");
            }

            var session = httpContextAccessor.HttpContext.Session;
            // Use consistent key casing ("CartId")
            var cartId = session.GetString ("CartId");
            if (string.IsNullOrEmpty (cartId))
            {
                cartId = Guid.NewGuid ().ToString ();
                session.SetString ("CartId", cartId);
            }
            return new ShoppingCart (context) { ShoppingCartId = cartId };
        }
        // add to cart  
        public void AddItemToCart(Movie movie)
        {
            // check if  movie is exist increase amount else and new one
            var ShoppingCartItem = _context.ShoppingCartItems
                .FirstOrDefault (n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);
            if (ShoppingCartItem == null) 
            {
                ShoppingCartItem = new ShoppingCartItem ()
                {
                    ShoppingCartId = ShoppingCartId,
                    Movie = movie,
                    Amount= 1
                };
                _context.ShoppingCartItems.Add (ShoppingCartItem);
            }else {
                ShoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }
        // remove item from cart
        public void RemoveItemToCart (Movie movie)
        {
            var ShoppingCartItem = _context.ShoppingCartItems
               .FirstOrDefault (n => n.Movie.Id == movie.Id && n.ShoppingCartId == ShoppingCartId);
            if (ShoppingCartItem != null)
            {
                if (ShoppingCartItem.Amount > 1)
                {
                    ShoppingCartItem.Amount--;
                }
                else
                {
                    _context.ShoppingCartItems.Remove (ShoppingCartItem);

                }
            }
          
            _context.SaveChanges ();

        }

        //get all shopping cart
        public List<ShoppingCartItem> GetshoppingCartItems()
        {
            return ShoppingCartItems ??(ShoppingCartItems=_context.ShoppingCartItems
                .Where(n=>n.ShoppingCartId==ShoppingCartId).Include(n=>n.Movie).ToList());

        }
        public double GetShoppingCartTotal() => _context.ShoppingCartItems
            .Where(n => n.ShoppingCartId == ShoppingCartId)
            .Select(n =>n.Movie.price * n.Amount).Sum();

        //clear shopping cart
        public async Task ClearShoppingCartAsync ()
        {
            var items = await _context.ShoppingCartItems
                .Where (n => n.ShoppingCartId == ShoppingCartId).ToListAsync ();
            _context.ShoppingCartItems.RemoveRange (items);
            await _context.SaveChangesAsync ();
        }

        }
}
