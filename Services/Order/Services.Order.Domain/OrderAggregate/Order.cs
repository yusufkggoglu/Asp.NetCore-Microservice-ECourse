using Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Order.Domain.OrderAggregate
{
    // Ef Core features
    // -- Ownred Types
    // -- Shadow Property
    // -- Backing Field
    public class Order : Entity,IAggregateRoot
    {
        public DateTime CreatedDate { get;private set; }
        public Address Address { get; private set; }
        public string BuyerId { get; private set; }
        private readonly List<OrderItem> _orderItems; // Order üzerinden OrderItem eklenmemesi için field tanımlandı.
                                                      // Efcore alanları otomatik dolduracak.
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems; // Fieldı dış dünyaya açıyoruz.

        public Order()
        {

        }
        public Order(string buyerId, Address address)
        {
            _orderItems = new List<OrderItem>();
            CreatedDate = DateTime.Now;
            BuyerId = buyerId;
            Address = address;
        }


        public void AddOrderItem(string productId, string productName, decimal price, string pictureUrl) 
        {
            var existProduct = _orderItems.Any(x => x.ProductId == productId);
            if(!existProduct)
            {
                var newOrderItem = new OrderItem(productId, productName, pictureUrl, price);
                _orderItems.Add(newOrderItem);
            }
        }
        public decimal GetTotalPrice => _orderItems.Sum(x => x.Price);
    }
}
