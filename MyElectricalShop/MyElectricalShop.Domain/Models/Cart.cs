﻿using Data.Entities;

namespace MyElectricalShop.Domain.Models
{
    public class Cart : ModelBase<Guid>
    {
        public Guid UserId { get; set; }
        public ICollection<CartLine> CartLines { get; set; }
    }
}
