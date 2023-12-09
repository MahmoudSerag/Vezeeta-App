﻿using Application.Dtos;
using Application.Interfaces.Repositories;
using Core.Models;
using Infrastructure.Database.Context;

namespace Infrastructure.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly ApplicationDbContext context;

        public DiscountRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public string CreateNewDiscount(DiscountDto discountDto)
        {
            try
            {
                int discountTypeId = context.DiscountTypes
                    .Where(dis => dis.Name == discountDto.DiscountType)
                    .Select(dis => dis.Id)
                    .FirstOrDefault();

                if (discountTypeId == 0)
                    return "No discount type (Percentage / Value) found to create new discount";

                var discount = this.context.Discounts.Add(
                    new Discount
                    {
                        DiscountCode = discountDto.DiscountCode,
                        IsActivated = true,
                        DiscountValue = discountDto.DiscountValue,
                        DiscountTypeId = discountTypeId
                    }
                );

                this.context.SaveChanges();

                return "Succeeded";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}