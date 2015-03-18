using System;
using System.Linq;

namespace _3DMapleSystem.Data.Common.Models
{
    public interface IOrderable
    {
        int OrderBy { get; set; }
    }
}
