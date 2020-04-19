using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test.Models.ViewModels
{
    public class Checkout
    {
        public List<Busket> Busket { get; set; }
        public Users User { get; set; }
    }
}
