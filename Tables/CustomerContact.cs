﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models.Tables
{
    public class CustomerContact
    {
        public int ContactId { get; set; }
        public string ContactName { get; set; }
        public string MessageBody { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
