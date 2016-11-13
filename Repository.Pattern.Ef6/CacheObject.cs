using System;

namespace Repository.Pattern.Ef6
{
    public class CacheObject<T>
    {
        public DateTime CreateDate { get; set; }
        public DateTime LastUpdate { get; set; }

        public T ObjVal { get; set; }

        public CacheObject(T objVal)
        {
            LastUpdate = CreateDate = DateTime.Now;
            ObjVal = objVal;
        }

        public CacheObject(DateTime creteDate, T objVal)
        {
            LastUpdate = CreateDate = creteDate;
            ObjVal = objVal;
        }
    }
}