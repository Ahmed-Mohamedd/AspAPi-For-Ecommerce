using System.Collections.Generic;


namespace SmartCartApi.Helpers
{
    public class Pagination<T>
    {
        

        public Pagination(int pageIndex, int pageSize, int count , IReadOnlyList<T> data)
        {
            PageIndex=pageIndex;
            PageSize=pageSize;
            Count=count;
            this.Data=data;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data{ get; set; }
    }
}
