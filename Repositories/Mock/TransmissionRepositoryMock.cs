﻿using CarDealership.Data.Interfaces;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Repositories.Mock
{
    public class TransmissionRepositoryMock : ITransmissionRepository
    {
        private static List<Transmission> _Transmissions = new List<Transmission>();

        private static Transmission Auto = new Transmission
        {
            TransmissionId = 1,
            TransmissionType = "Automatic"
        };

        private static Transmission Manual = new Transmission
        {
            TransmissionId = 2,
            TransmissionType = "Manual"
        };

        public TransmissionRepositoryMock()
        {
            if (_Transmissions.Count() == 0)
            {
                _Transmissions.Add(Auto);
                _Transmissions.Add(Manual);
            }
        }

        public IEnumerable<Transmission> GetAll()
        {
            return _Transmissions;
        }

        public Transmission GetTransmissionById(int TransmissionId)
        {
            return _Transmissions.FirstOrDefault(b => b.TransmissionId == TransmissionId);
        }
    }
}
