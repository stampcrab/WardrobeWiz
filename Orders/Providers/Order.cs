using System;
using System.Collections.Generic;

namespace Orders.Providers
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateOnly OrderDate { get; set; }
        public decimal Total { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
