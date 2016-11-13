using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities.Models
{
    public class DFMenuView
    {

        public DFMenuView()
        {
            ChildMenu = new List<DFMenuView>();
        }
        public string Area { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

        public string Id { get; set; }
        public string MenuName { get; set; }
        public List<DFMenuView> ChildMenu { get; set; }
    }
}
