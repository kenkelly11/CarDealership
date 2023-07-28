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
    public class CustomerContactRepositoryFactory
    {
        public static ICustomerContactRepository GetRepository()
        {
            switch (Settings.GetRepositoryType())
            {
                case "Prod":
                    return new CustomerContactRepositoryADO();
                case "QA":
                    return new CustomerContactRepositoryMock();
                default:
                    throw new Exception("Could not find valid RepositoryType configuration value.");
            }
        }
    }
}
