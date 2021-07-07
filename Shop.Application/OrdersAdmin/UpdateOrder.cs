using Shop.Domain.Infrastructure;
using System.Threading.Tasks;

namespace Shop.Application.OrdersAdmin
{
    /// <summary>
    /// UpdateOrder Admin class using orderManager interface method to advance order forward
    /// </summary>
    [Service]
    public class UpdateOrder
    {
        private IOrderManager _orderManager;

        public UpdateOrder(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        public Task<int> DoAsync(int id)
        {
            return _orderManager.AdvanceOrder(id);
        }
    }
}
