using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Entities.Models
{
    public class DFB4Filter
    {
        [Display(Name = "Đơn Vị Đối Soát")]
        public string DonVi { get; set; }
    }
}
