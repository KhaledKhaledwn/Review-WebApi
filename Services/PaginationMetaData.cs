namespace ReviewApiApp.Services
{
    public class PaginationMetaData
    {
        public int pageCurrent {  get; set; }
        public int pageSize { get; set; }
        public int TotalItemsCount { get; set; }

        public int TotalPagesCount { get; set; }

        public PaginationMetaData(int pageCurrent, int pageSize, int totalItemsCount)
        {
            this.pageCurrent = pageCurrent;
            this.pageSize = pageSize;
            this.TotalItemsCount = totalItemsCount;
            this.TotalPagesCount = (int) Math.Ceiling(totalItemsCount/(double) pageSize);
        }

    }
}
