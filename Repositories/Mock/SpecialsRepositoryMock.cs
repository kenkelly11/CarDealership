using CarDealership.Data.Interfaces;
using CarDealership.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Data.Repositories.Mock
{
    public class SpecialsRepositoryMock : ISpecialsRepository
    {
        private static List<Specials> _specials = new List<Specials>();

        private static Specials _specialOne = new Specials
        {
            SpecialId = 1,
            Title = "$1000 Rebate Special!",
            SpecialDetails = "$1000 Rebate on all Toyota SUVs!"
        };

        private static Specials _specialTwo = new Specials
        {
            Title = "Free tank of gas!",
            SpecialId = 2,
            SpecialDetails = "Free tank of gas with every purchase!"
        };


        private static Specials _specialThree = new Specials
        {
            Title = "Ford free warranty special!",
            SpecialId = 3,
            SpecialDetails = "Free extended Warranty on all Ford models!"
        };

        private static Specials _specialFour = new Specials
        {
            Title = "1% Financing!",
            SpecialId = 4,
            SpecialDetails = "1% Financing special all Summer Long!"
        };

        public SpecialsRepositoryMock()
        {
            if (_specials.Count != 0)
            {
                _specials.Clear();
            }

            _specials.Add(_specialOne);
            _specials.Add(_specialTwo);
            _specials.Add(_specialThree);
            _specials.Add(_specialFour);
        }

        public void Delete(int SpecialId)
        {
            Specials special = _specials.FirstOrDefault(s => s.SpecialId == SpecialId);

            _specials.Remove(special);
        }

        public IEnumerable<Specials> GetAll()
        {
            return _specials;
        }

        public void Insert(Specials special)
        {
            special.SpecialId = _specials.Max(s => s.SpecialId) + 1;

            _specials.Add(special);
        }

        public void SpecialsClearList()
        {
            _specials.Clear();
        }
    }
}
