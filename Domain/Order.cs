using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABOOP_1.Domain
{
    internal class Order
    {
        string _companyName;
        uint _amountOfdDetails;
        Detail _reqiredDetail;
        uint _payment;

        public Order(string companyName, uint amountOfdDetails, Detail reqiredDetail, uint payment)
        {
            _companyName = companyName;
            _amountOfdDetails = amountOfdDetails;
            _payment = payment;
            _reqiredDetail = reqiredDetail;
        }

        public Order(Order other)
        {
            _companyName = other._companyName;
            _amountOfdDetails = other._amountOfdDetails;
            _payment = other._payment;
            _reqiredDetail = other._reqiredDetail;
        }

        public string CompanyName => _companyName;
        public uint AmountOfDetails => _amountOfdDetails;
        public uint Payment => _payment;
        public Detail ReqiredDetail => _reqiredDetail;
    }
}
