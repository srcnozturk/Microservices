﻿using Services.Order.Domain.Core;
using System.Collections.Generic;

namespace Services.Order.Domain.OrderAggregate
{
    public class Address : ValueObject
    {
        public string Province { get;private set; }
        public string District { get;private set; }
        public string Street { get;private set; }
        public string ZipCode { get;private set; }
        public string Line { get;private set; }

        public Address(string province, string district, string street, string zipCode, string line)
        {
            Province = province;
            District = district;
            Street   = street;
            ZipCode  = zipCode;
            Line     = line;
        }

        //public void SetZipCode(string zipCode)
        //{
        //    // business code
        //    ZipCode = zipCode;
        //}

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Province;
            yield return District;
            yield return Street; 
            yield return ZipCode;
            yield return Line;
        }
    }
}
