﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_5
{
    internal interface IRepository<T>
    {
        List<T> GetAll();
    }
}
