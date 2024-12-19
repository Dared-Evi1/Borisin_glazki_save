using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Borisin_glazki_save
{
    public class AgentProductsViewModel
    {
        public CollectionViewSource Products { get; set; }
        public Agent CurrentAgent { get; set; }
    }
}
