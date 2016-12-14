using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNet.Components.Extensions
{
    public static class CollectionExtension
    {
        public static bool IsEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || collection.Count() < 1;
        }

        public static bool IsNotEmpty<T>(this IEnumerable<T> collection)
        {
            return !IsEmpty(collection);
        }

        public static void DeleteBatch<T>(this IList<T> src, IEnumerable<T> lstToDel)
        {
            if (src.IsEmpty() || lstToDel.IsEmpty())
            {
                return;
            }

            var lst = lstToDel.ToList();
            var delTotal = lst.Count;
            for (var idx = 0; idx < delTotal; idx++)
            {
                if (src.Contains(lst[idx]))
                {
                    src.Remove(lst[idx]);
                }
            }
        }

        public static void DeleteBatch<T>(this IList<T> src, Func<IEnumerable<T>> lstToDelFunc)
        {
            IEnumerable<T> lstToDel = null;
            if (lstToDelFunc != null)
            {
                lstToDel = lstToDelFunc();
            }
            DeleteBatch(src, lstToDel);
        }

        public static void AddRange<T>(this IList<T> target, IEnumerable<T> lstToAdd)
        {
            if (target == null || lstToAdd.IsEmpty())
            {
                return;
            }
            if (target is List<T>)
            {
                (target as List<T>).AddRange(lstToAdd);
                return;
            }
            foreach (T t in lstToAdd)
            {
                if (!target.Contains(t))
                {
                    target.Add(t);
                }
            }
        }

        public static void AddRange<T>(this IList<T> src, Func<IEnumerable<T>> lstToAddFunc)
        {
            IEnumerable<T> lstToAdd = null;
            if (lstToAddFunc != null)
            {
                lstToAdd = lstToAddFunc();
            }
            AddRange(src, lstToAdd);
        }
    }
}
