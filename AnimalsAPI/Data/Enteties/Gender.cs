using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Enteties
{
    public class Gender
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Animal>? Animals { get; set; }
    }
}
