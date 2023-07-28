using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Interfaces
{
    public interface IModelRepository
    {
        IEnumerable<Model> GetAll();
        Model GetModelById(int ModelId);
        List<Model> GetModelsByMakeId(int MakeId);
        void Insert(Model model);
    }
}
