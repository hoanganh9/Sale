using Repository.Pattern.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Pattern.Infrastructure
{
    public class EntityModel<TEntity>  where TEntity : class, IObjectState
    {
        public TEntity _entity;
        public EntityModel()
        {
        }
        public EntityModel(TEntity entity)
        {
            _entity = entity;
        }
        public string _UserId { set; get; }

        public string _UserProvinceCode { set; get; }

        public string _UserDistrictCode { get; set; }

        public string _UserCommuneCode { get; set; }

        public string _UserUnitCode { set; get; }

        public string _UserPosCode { get; set; }

        public DateTime _DateChanged { get; set; }

        public TypeAction _TypeAction { get; set; }
    }
    public class ModelSearch
    {

        public ModelSearch()
        {
            LstProvinceCode = new List<string>();
            LstDistrictCode = new List<string>();
            LstUnitCode = new List<string>();
            LstPosCode = new List<string>();
            LstUserId = new List<string>();
        }

        public string Search { get; set; }

        public List<string> LstProvinceCode { get; set; }

        public List<string> LstDistrictCode { get; set; }

        public List<string> LstUnitCode { get; set; }

        public List<string> LstPosCode { get; set; }

        public List<string> LstUserId { get; set; }

        public string UserId { set; get; }

        public string UserProvinceCode { set; get; }

        public string UserDistrictCode { get; set; }

        public string UserUnitCode { set; get; }

        public string UserPosCode { get; set; }
    }

    public enum TypeAction : byte
    {
        Edit = 1,
        CapNhat = 2,
        XacNhan = 3,
    }
}
