﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ProductSpecificationsParamters
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Sort { get; set; }

        public string? Search { get; set; }

        private int _pageIndex = 1 ;
        private int _pageSize = 5;

        public int pageSize
        {
            get { return _pageSize = 5; }
            set { _pageSize = value; }
        }


        public int pageIndex
        {
            get { return _pageIndex = 1 ; }
            set { _pageIndex = value; }
        }



    }
}
