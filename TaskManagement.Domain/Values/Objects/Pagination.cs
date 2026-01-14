namespace TaskManagement.Domain.Values.Objects
{
    public class Pagination
    {
        public Pagination() { }

        public Pagination(int page, int size, int filteredItems)
        {
            Page = page;
            Size = size;
            FilteredItems = filteredItems;
        }

        public int Page { get; set; }
        public int Size { get; set; }
        public int FilteredItems { get; set; }
    }
}
