﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DT.Client.Entities
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int MinutesToExpire { get; set; }
    }
}
