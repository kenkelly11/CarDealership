﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models.Queries
{
    public class SalesSearchParameters
    {
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }
        public string UserName { get; set; }
    }
}
