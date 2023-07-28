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
    public class PurchaseLogRepositoryFactory
    {
        public static IPurchaseLogRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "Prod":
                    return new PurchaseLogRepositoryADO();
                case "QA":
                    return new PurchaseLogRepositoryMock();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value.");
            }
        }
    }
}
