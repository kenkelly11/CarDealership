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
    public class BodyStyleRepositoryFactory
    {
        public static IBodyStyleRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "Prod":
                    return new BodyStyleRepositoryADO();
                case "QA":
                    return new BodyStyleRepositoryMock();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value.");
            }
        }
    }
}
