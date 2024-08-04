
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Wrapper
{
    public  static  class ExtensionMethods
    {
        public static    PaginatedResult<T> ToPaginatedResultAsync<T>(this IQueryable<T>source,int pageNumber,int pageSize) where T : class
        {
            if(source == null) throw new ArgumentNullException("EmptySourceCannotConvert");
            pageNumber =pageNumber<= 0 ? 1 : pageNumber;
            pageSize = pageSize == 0 ? 10:pageSize;
            int count =   source.AsNoTracking().Count();
            if (count == 0) 
                return PaginatedResult<T>.Success(new List<T>(), count, pageNumber, pageSize);

            pageNumber = pageNumber <= 0 ? 1 : pageNumber;

            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var itemsToList=items.ToList();
            return  PaginatedResult<T>.Success(itemsToList, count, pageNumber, pageSize);

        }
        


    }
}
