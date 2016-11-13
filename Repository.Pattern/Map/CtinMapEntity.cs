using System.Collections.Generic;
using System.Data;

namespace Repository.Pattern.Map
{
    public class CtinMapEntity
    {
        private readonly List<CtinMapPropertie> lstMap;
        private readonly DataTable dt;
        private string sCrtTemp;
        private string sIst;
        private string sUp;
        private string sUpOrIst;
        private string sDel;

        public CtinMapEntity(DataTable _dt, List<CtinMapPropertie> _lstMap,
            string _sCrtTemp, string _sIst, string _sUp, string _sUpOrIst, string _sDel)
        {
            dt = _dt;
            lstMap = _lstMap;
            sCrtTemp = _sCrtTemp;
            sIst = _sIst;
            sUp = _sUp;
            sUpOrIst = _sUpOrIst;
            sDel = _sDel;
        }

        public List<CtinMapPropertie> MapPropertie
        {
            get
            {
                return lstMap;
            }
        }

        public DataTable MapTable
        {
            get
            {
                return dt.Clone();
            }
        }

        public string getScriptTable(string tempTableName)
        {
            return string.Format(sCrtTemp, tempTableName);
        }

        public string getScriptInsert(string tempTableName)
        {
            return string.Format(sIst, tempTableName);
        }

        public string getScriptUpdate(string tempTableName)
        {
            return string.Format(sUp, tempTableName);
        }

        public string getScriptUpdateInsert(string tempTableName)
        {
            return string.Format(sUpOrIst, tempTableName);
        }

        public string getScriptDelete(string tempTableName)
        {
            return string.Format(sDel, tempTableName);
        }
    }
}