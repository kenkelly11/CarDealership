using CarDealership.Data.Interfaces;
using CarDealership.Data.Repositories.ADO;
using CarDealership.Data.Repositories.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Factories
{
    public class SpecialsRepositoryFactory
    {
        public static ISpecialsRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "Prod":
                    return new SpecialsRepositoryADO();
                case "QA":
                    return new SpecialsRepositoryMock();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value.");
            }
        }
    }
}
