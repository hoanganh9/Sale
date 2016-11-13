namespace DAL.Entities
{
    using Base.Common;
    using Newtonsoft.Json;
    using Repository.Pattern.Ef6;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Linq;

    public partial class SaleSeafoodEntities
    {
        //[DbFunction("SaleSeafoodEntities.Store", "sosanhstring")]
        //public static bool sosanhstring(string txt, string txt2)
        //{
        //    throw new NotSupportedException("Direct calls are not supported.");
        //}

        //internal object getDistrictByProvince()
        //{
        //    throw new NotImplementedException();
        //}
        
        //public int pr_data_mapping(string syncTableName, string newTableName, out string error)
        //{
        //    ObjectParameter perror = new ObjectParameter("Error", typeof(string));

        //    int result = pr_data_mapping(syncTableName, newTableName, perror);
        //    error = perror.Value.ToString();
        //    return result;
        //}
    }
}