using Shop.Domain.Models;
using System;
using System.Collections.Generic;

namespace Shop.Domain.Infrastructure
{
    /// <summary>
    /// ISessionManager interface that inherits from the SessionManager infrastructure class contained in Shop.UI
    /// </summary>
    public interface ISessionManager
    {
        string GetId();
        void AddProduct(CartProduct cartProduct);

        void RemoveProduct(int stockId, int qty);

        IEnumerable<TResult> GetCart<TResult>(Func<CartProduct, TResult> selector);

        void ClearCart();

        void AddCustomerInformation(CustomerInformation customer);

        CustomerInformation GetCustomerInformation();
    }
}
