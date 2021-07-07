using Shop.Domain.Infrastructure;
using System.Threading.Tasks;

namespace Shop.Application.Cart
{
    [Service]
    public class RemoveFromCart
    {
        private ISessionManager _sessionManager;
        private IStockManager _stockManager;

        //making use of stock and session manager interfaces
        public RemoveFromCart(
            ISessionManager sessionManager,
            IStockManager stockManager)
        {
            _sessionManager = sessionManager;
            _stockManager = stockManager;
        }

        public class Request
        {
            public int StockId { get; set; }
            public int Qty { get; set; }
        }

        public async Task<bool> Do(Request request)
        {
            //checks if the qty is valid
            if (request.Qty <= 0)
            {
                return false;
            }

            //remove stock from hold async method using stock manager
            await _stockManager
                .RemoveStockFromHold(request.StockId, request.Qty, _sessionManager.GetId());

            //removes product using session manager
            _sessionManager.RemoveProduct(request.StockId, request.Qty);

            return true;
        }
    }
}
