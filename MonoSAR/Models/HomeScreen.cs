﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonoSAR.Models
{
    public class HomeScreen
    {
        public String LogoPath { get; set; }

        public String CurrentYearPretty { get { return DateTime.UtcNow.Year.ToString(); } }
    }
}
