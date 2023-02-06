using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrediPayPublic.Views.Shared.Responses
{
    public class PageInfoResponse
    {
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int TotalCount { get; set; }
        public PageInfoResponse(int page, int pageSize, int totalCount)
        {
            CurrentPage = page;
            TotalPages = totalCount % pageSize > 0 ? totalCount / pageSize + 1 : totalCount / pageSize;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
    }

    public class GenericResponse<T>
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public T Data { get; set; }
    }
    public class AppResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
        public string ResponseCode { get; set; }
        public T Value { get; set; }
    }

}