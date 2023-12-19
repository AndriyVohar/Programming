using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7.Models
{
    internal interface IRepository<T>
    {
        List<T> GetAll();
    }
}