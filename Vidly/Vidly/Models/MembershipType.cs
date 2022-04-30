﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class MembershipType
    {   
        public Byte Id { get; set; }
        public string Name { get; set; }
        public short SignUpFee { get; set; }
        public byte DurationInMonths { get; set; }
        public Byte DiscountRate { get; set; }

        //Refactoring magic strings
        public static readonly byte Unknown = 0;
        public static readonly byte PayAsYouGo = 1;

    }
}