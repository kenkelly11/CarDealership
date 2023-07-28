using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Interfaces
{
    public interface ICustomerContactRepository
    {
        IEnumerable<CustomerContact> GetAllContacts();
        CustomerContact GetContactById(int ContactId);
        void Insert(CustomerContact CustomerContact);
    }
}
