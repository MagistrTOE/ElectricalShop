﻿using Data.Entities;

namespace MyElectricalShop.Domain.Models
{
    public class VoltageLevel : ModelBase<int>
    {
        public int MinLevel { get; set; }
        public int MaxLevel { get; set; }
    }
}
