﻿using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Interfaces
{
    public interface IBodyStyleRepository
    {
        IEnumerable<BodyStyle> GetAll();
        BodyStyle GetBodyStyleById(int BodyStyleId);
    }
}
