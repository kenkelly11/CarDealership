﻿using CarDealership.Data.Interfaces;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Repositories.Mock
{
    public class BodyStyleRepositoryMock : IBodyStyleRepository
    {
        private static List<BodyStyle> _bodyStyles = new List<BodyStyle>();

        private static BodyStyle Truck = new BodyStyle
        {
            BodyStyleId = 1,
            BodyStyleType = "Truck"
        };

        private static BodyStyle Car = new BodyStyle
        {
            BodyStyleId = 2,
            BodyStyleType = "Car"
        };

        private static BodyStyle SUV = new BodyStyle
        {
            BodyStyleId = 3,
            BodyStyleType = "SUV"
        };

        private static BodyStyle Van = new BodyStyle
        {
            BodyStyleId = 4,
            BodyStyleType = "Van"
        };

        public BodyStyleRepositoryMock()
        {
            if (_bodyStyles.Count() == 0)
            {
                _bodyStyles.Add(Truck);
                _bodyStyles.Add(Car);
                _bodyStyles.Add(SUV);
                _bodyStyles.Add(Van);
            }
        }

        public IEnumerable<BodyStyle> GetAll()
        {
            return _bodyStyles;
        }

        public BodyStyle GetBodyStyleById(int BodyStyleId)
        {
            return _bodyStyles.FirstOrDefault(b => b.BodyStyleId == BodyStyleId);
        }
    }
}
