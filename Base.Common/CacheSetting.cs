namespace Base.Common
{
    using System;

    public static class CacheSetting
    {
        public static class SysConfig
        {
            public const string Key = "SysConfig";
            public static readonly TimeSpan SlidingExpiration = TimeSpan.FromDays(1);
            public static readonly TimeSpan SlidingUpdate = TimeSpan.FromMinutes(30);
        }
        public static class Category
        {
            public const string Key = "Category";
            public static readonly TimeSpan SlidingExpiration = TimeSpan.FromDays(1);
            public static readonly TimeSpan SlidingUpdate = TimeSpan.FromMinutes(30);
        }
    }
}