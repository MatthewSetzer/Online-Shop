using Shop.Domain.Infrastructure;
using Shop.Domain.Models;
using System.Threading.Tasks;

namespace Shop.Application.Cart
{
    [Service]
    public class AddToCart
    {
        //Making use of session manager and stock manager interfaces
        private ISessionManager _sessionManager;
        private IStockManager _stockManager;

        public AddToCart(
            ISessionManager sessionManager,
            IStockManager stockManager)
        {
            _sessionManager = sessionManager;
            _stockManager = stockManager;
        }

        //Request model
        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }

        //async bool method to add product to cart session
        public async Task<bool> Do(Request request)
        {
            //service responsibility
            if (!_stockManager.EnoughStock(request.StockId, request.Qty))
            {
                return false;
            }

            //putting stock on hold
            await _stockManager.PutStockOnHold(request.StockId, request.Qty, _sessionManager.GetId());

            var stock = _stockManager.GetStockWithProduct(request.StockId);

            //adds the cart as well as stock reference to session
            var cartProduct = new CartProduct()
            {
                ProductId = stock.ProductId,
                ProductName = stock.Product.Name,
                StockId = stock.Id,
                Qty = request.Qty,
                Value = stock.Product.Value,
            };

            //Adding product to session
            _sessionManager.AddProduct(cartProduct);

            return true;
        }
    }
}
