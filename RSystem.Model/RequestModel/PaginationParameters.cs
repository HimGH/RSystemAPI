﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSystem.Model.RequestModel
{
    public class PaginationParameters
    {
        public int PageNumber { get; set; } = 1; 
        public int PageSize { get; set; } = 20; 
        public string Search { get; set; } = "";

    }
}
