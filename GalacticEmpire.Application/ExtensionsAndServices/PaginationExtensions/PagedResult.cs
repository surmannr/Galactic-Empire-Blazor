using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GalacticEmpire.Application.PaginationExtensions
{
    public class PagedResult<T>
    {
        public PagedResult(IEnumerable<T> results, int allResultsCount, int pageNumber, int pageSize)
        {
            Results = results;
            AllResultsCount = allResultsCount;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public IEnumerable<T> Results { get; set; }
        public int AllResultsCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
