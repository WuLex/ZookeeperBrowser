﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZookeeperBrowser.Message.Request
{
    public class GetChildrenRequest
    {
        public String ParentPath { get; set; }
    }
}
