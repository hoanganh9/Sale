using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities.Models
{
    public class DFUserRoleModel
    {
        public DFUserRoleModel()
        {
            Role_Current = new List<AspNetRole>();
            Role_NotMap = new List<AspNetRole>();
        }

        public string Id { get; set; }

        [Display(Name = "Tài khoản")]
        public string UserName { get; set; }

        [Display(Name = "Tên người dùng")]
        public string DislayName { get; set; }

        [Display(Name = "Tên đơn vị")]
        public string UnitName { get; set; }

        public string LstRole { get; set; }
        public List<AspNetRole> Role_Current { get; set; }
        public List<AspNetRole> Role_NotMap { get; set; }
    }
}