﻿using System.ComponentModel.DataAnnotations;

namespace VCommerce.CartApi.DTOs;

public class CouponDTO
{
    public int CouponId { get; set; }
    [Required]
    public string? CouponCode { get; set; }
    [Required]
    public decimal Discount { get; set; }
}
