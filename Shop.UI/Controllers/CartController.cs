using Microsoft.AspNetCore.Mvc;
using Shop.Application.Cart;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.UI.Controllers
{
    /// <summary>
    /// Controller to manage cart functionality
    /// </summary>
    [Route("[controller]/[action]")]
    public class CartController : Controller
    {
        //adding single item to cart
        [HttpPost("{stockId}")]
        public async Task<IActionResult> AddOne( int stockId, [FromServices] AddToCart addToCart)
        {
            var request = new AddToCart.Request
            {
                StockId = stockId,
                Qty = 1
            };

            var success = await addToCart.Do(request);

            if (success)
            {
                return Ok("Item Added to cart");
            }

            return BadRequest("Failed to add to cart");
        }

        //removing items from cart
        [HttpPost("{stockId}/{qty}")]
        public async Task<IActionResult> Remove(int stockId,int qty, [FromServices] RemoveFromCart removeFromCart)
        {
            var request = new RemoveFromCart.Request
            {
                StockId = stockId,
                Qty = qty
            };

            var success = await removeFromCart.Do(request);

            if (success)
            {
                return Ok("Item removed from cart");
            }

            return BadRequest("Failed to remove item from cart");
        }

        //returning small cart component
        [HttpGet]
        public IActionResult GetCartComponent([FromServices] GetCart getCart)
        {
            var totalValue = getCart.Do().Sum(x => x.RealValue * x.Qty);

            return PartialView("Components/Cart/Small", $"R{totalValue}");
        }

        //returning main cart component 
        [HttpGet]
        public IActionResult GetCartMain([FromServices] GetCart getCart)
        {
            var cart = getCart.Do();

            return PartialView("_CartPartial", cart);
        }
    }
}
