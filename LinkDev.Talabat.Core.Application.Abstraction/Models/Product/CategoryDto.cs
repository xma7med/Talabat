using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Abstraction.Models.Product
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
