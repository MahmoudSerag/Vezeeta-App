﻿namespace Core.Models
{
    public class DiscountType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Discount> Discounts { get; set; }
    }
}
