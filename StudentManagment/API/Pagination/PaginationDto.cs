using System;
using System.Linq;

namespace API.Pagination
{
    public class PaginationDto<T>
    {
        public PaginationDto(IQueryable <T> item, int page, int size)
        {
            if (page < 1) page = 1;
            if (size < 1) size = 10;
            Items = item.Skip(size * (page - 1)).Take(size);
            TotalPages = (int)Math.Ceiling((double)item.Count() / size);
            HasBefore = page > 1;
            HasAfter = page <TotalPages;
        }

        public IQueryable<T> Items { get; set; }
        public int Size { get; set; }
        public int TotalPages { get; set; }
        public bool HasBefore { get; set; }
        public bool HasAfter { get; set; }
    }
}
