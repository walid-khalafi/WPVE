using System;
namespace WPVE.Core.Domain.Orders
{
    public class ShoppingCartItem : BaseEntity
    {
        public ShoppingCartItem()
        {
        }

        /// <summary>
        /// Gets or sets the shopping cart type identifier
        /// </summary>
        public int ShoppingCartTypeId { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public int CustomerId { get; set; }
        /// <summary>
        /// Gets or sets the product identifier
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Gets or sets the quantity
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Gets the log type
        /// </summary>
        public ShoppingCartType ShoppingCartType
        {
            get => (ShoppingCartType)ShoppingCartTypeId;
            set => ShoppingCartTypeId = (int)value;
        }
    }
}

