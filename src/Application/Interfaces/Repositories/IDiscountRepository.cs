﻿using Application.Dtos;

namespace Application.Interfaces.Repositories
{
    public interface IDiscountRepository
    {
        public string CreateNewDiscount(DiscountDto discountDto);
        public string UpdateDiscountCode(DiscountDto discountDto, int discountId);
    }
}
