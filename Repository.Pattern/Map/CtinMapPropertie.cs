using System.Reflection;

namespace Repository.Pattern.Map
{
    public class CtinMapPropertie
    {
        public string colName;
        public PropertyInfo propertie;

        public CtinMapPropertie(string _colName, PropertyInfo _propertie)
        {
            colName = _colName;
            propertie = _propertie;
        }
    }
}