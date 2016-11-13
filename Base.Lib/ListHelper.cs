using System;
using System.Collections.Generic;
using System.Linq;

namespace Base.Lib
{
    public static class ListHelper
    {
        /// <summary>
        /// Phân trang và trả ra số trang và danh sách trong trang
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="page">Trang can lay du lieu</param>
        /// <param name="pageSize">So ban ghi cua mot trang</param>
        /// <param name="pageCount">tra ra tong so trang</param>
        /// <param name="totalItemCount">tra ra tong so ban ghi</param>
        /// <returns></returns>
        public static List<T> buildPage<T>(IQueryable<T> query, int page, int pageSize, out int pageCount, out int totalItemCount)
        {
            totalItemCount = query.Count();
            //neu khong co du lieu thi return
            if (totalItemCount == 0)
            {
                pageCount = 0;
                return new List<T>();
            }
            pageCount = PageCount(pageSize, totalItemCount);
            //Neu trang hien tai lon hon pagecount thi thiet lap bang pageCount-1
            if (page >= pageCount)
                page = pageCount - 1;
            else
                page = page - 1;//So trang bat dau tu 0
            return query.Skip(page * pageSize).Take(pageSize).ToList();
        }

        public static int PageCount(int pageSize, int totalItem)
        {
            int pageCount = 0;
            //neu khong co du lieu thi return
            if (totalItem == 0)
            {
                return pageCount;
            }
            //tinh pagecount
            pageCount = totalItem / pageSize;
            //Neu so luong con du thi tang pagecount len 1
            if (totalItem % pageSize > 0) pageCount++;
            return pageCount;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySortedCollection"></param>
        /// <param name="key"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static int ZFindFirstIndexGreaterThanOrEqualTo<TElement, TKey>(
                        this IList<TElement> keySortedCollection,
                        TKey key,
                        Func<TElement, TKey> keySelector)
        {
            return ZFindFirstIndexGreaterThanOrEqualTo(keySortedCollection,
                                                       key,
                                                       keySelector,
                                                       Comparer<TKey>.Default);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySortedCollection"></param>
        /// <param name="key"></param>
        /// <param name="keySelector"></param>
        /// <param name="keyComparer"></param>
        /// <returns></returns>
        public static int ZFindFirstIndexGreaterThanOrEqualTo<TElement, TKey>(
                this IList<TElement> keySortedCollection,
                TKey key,
                Func<TElement, TKey> keySelector,
                IComparer<TKey> keyComparer)
        {
            int begin = 0;
            int end = keySortedCollection.Count;
            while (end > begin)
            {
                int index = (begin + end) / 2;
                TElement el = keySortedCollection[index];
                TKey currElKey = keySelector(el);
                if (keyComparer.Compare(currElKey, key) >= 0)
                    end = index;
                else
                    begin = index + 1;
            }
            return end;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySortedCollection"></param>
        /// <param name="key"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static int ZFindIndexByKey<TElement, TKey>(
                        this IList<TElement> keySortedCollection,
                        TKey key,
                        Func<TElement, TKey> keySelector)
        {
            return ZFindIndexByKey(keySortedCollection,
                                                       key,
                                                       keySelector,
                                                       Comparer<TKey>.Default);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySortedCollection"></param>
        /// <param name="key"></param>
        /// <param name="keySelector"></param>
        /// <param name="keyComparer"></param>
        /// <returns></returns>
        public static int ZFindIndexByKey<TElement, TKey>(
                this IList<TElement> keySortedCollection,
                TKey key,
                Func<TElement, TKey> keySelector,
                IComparer<TKey> keyComparer)
        {
            int begin = 0;
            int end = keySortedCollection.Count;
            while (end > begin)
            {
                int index = (begin + end) / 2;
                TElement el = keySortedCollection[index];
                TKey currElKey = keySelector(el);
                int comparer = keyComparer.Compare(currElKey, key);
                if (comparer == 0)
                    return index;
                if (keyComparer.Compare(currElKey, key) > 0)
                    end = index;
                else
                    begin = index + 1;
            }
            return -1;
        }

        public static TElement ZFindByKey<TElement, TKey>(
                        this IList<TElement> keySortedCollection,
                        TKey key,
                        Func<TElement, TKey> keySelector)
        {
            return ZFindByKey(keySortedCollection,
                                                       key,
                                                       keySelector,
                                                       Comparer<TKey>.Default);
        }

        public static TElement ZFindByKey<TElement, TKey>(
                this IList<TElement> keySortedCollection,
                TKey key,
                Func<TElement, TKey> keySelector,
                IComparer<TKey> keyComparer)
        {
            int begin = 0;
            int end = keySortedCollection.Count;
            while (end > begin)
            {
                int index = (begin + end) / 2;
                TElement el = keySortedCollection[index];
                TKey currElKey = keySelector(el);
                int comparer = keyComparer.Compare(currElKey, key);
                if (comparer == 0)
                    return el;
                if (keyComparer.Compare(currElKey, key) > 0)
                    end = index;
                else
                    begin = index + 1;
            }
            return default(TElement);
        }
    }
}