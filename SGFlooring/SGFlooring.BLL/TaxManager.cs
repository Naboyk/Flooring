using SGFlooring.Models;
using SGFlooring.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGFlooring.BLL
{
    public class TaxManager
    {
        private ITaxRepository _taxRepository;

        public TaxManager(ITaxRepository taxRepository)
        {
            _taxRepository = taxRepository;
        }

        public Tax GetStateTax(string state)
        {
            return _taxRepository.LoadTax().FirstOrDefault(t => t.StateAbbreviation == state || t.StateName == state);
        }

        public List<Tax> ListAllStateTax()
        {
            return _taxRepository.LoadTax();
        }
    }
}
