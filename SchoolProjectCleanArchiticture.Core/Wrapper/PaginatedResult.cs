using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProjectCleanArchiticture.Core.Wrapper
{
    public class PaginatedResult<T>
    {
        public int pageNumber {  get; set; }
        public int pageSize { get; set; }
        public int currentPage { get; set; }
        public int totalPage { get; set; }
        public int totalPageNumber { get; set; }
        public bool hasPreviousPage=>currentPage > 1;
        public bool hasNextPage=>currentPage < totalPage;
        public List<T>_data { get; set; }
        public bool _succeeded { get; set; }
        public object Meta { get; set; }
        public List<string> messages { get; set; } = new List<string>();
        
        public PaginatedResult(List<T>data)
        {
            _data = data;
        }

        public  PaginatedResult(bool succeeded, List<T> data = default, List<string> messages = null, int count = 0, int page = 1, int pageSize = 10)
        {
            _data = data;
            currentPage = page;
             _succeeded= succeeded;
            this.pageSize = pageSize;
            this.totalPage = (int)Math.Ceiling(count / (double)pageSize);
            this.totalPageNumber = count;
        }
        public static PaginatedResult<T> Success(List<T> data, int count, int page, int pageSize)
        {
            return new(true, data, null, count, page, pageSize);
        }




    }
}
