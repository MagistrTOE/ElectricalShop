﻿using Data.EntityFrameworkCore;
using MyElectricalShop.Domain.Interfaces;
using MyElectricalShop.Domain.Models;

namespace MyElectricalShop.Infrastructure.Data.Repositories
{
    public class VoltageLevelRepository : RepositoryGeneric<VoltageLevel, int>, IVoltageLevelRepository
    {
        public VoltageLevelRepository(MyElectricalShopContext contex) : base(contex)
        {
        }
    }
}
