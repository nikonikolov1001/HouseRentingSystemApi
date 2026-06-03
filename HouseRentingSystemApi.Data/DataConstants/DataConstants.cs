using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseRentingSystemApi.Data.DataConstants
{
    public class DataConstants
    {
        public class House
        {
            public const int TitleMinLength = 10;
            public const int TitleMaxLength = 50;
            public const int AddressMinLength = 30;
            public const int AddressMaxLength = 150;
            public const int DescriptionMinLength = 50;
            public const int DescriptionMaxLength = 500;
            public const double PricePerMonthMinValue = 0;
            public const double PricePerMonthMaxValue = 2000;
        }

        public class Category
        {
            public const int NameMaxLength = 50;
            public readonly string[] ValidCategories = new string[]
            {
                "Cottage",
                "Single-Family",
                "Duplex"
            };
        }

        public class Agent
        {
            public const int PhoneNumberMaxLength = 15;
        }

    }
}
