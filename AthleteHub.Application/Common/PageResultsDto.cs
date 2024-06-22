namespace Resturants.Application.Common;

public class PageResultsDto<T>
{
    public PageResultsDto(IEnumerable<T> items, int totalItemsCount, int pageNumber, int pageSize)
    {
        Items = items;
        TotalItemsCount = totalItemsCount;
        TotalPages = (int) Math.Ceiling((totalItemsCount / (double) pageSize));
        ItemsFrom = pageSize * (pageNumber - 1) + 1;  
        ItemsTo =  ItemsFrom + pageSize - 1;
        HasNextPage = pageNumber < TotalPages;
        HasPreviousPage = pageNumber > 1;
    }

    public IEnumerable<T> Items { get; private set; }
    public int TotalItemsCount { get; private set; }
    public int TotalPages { get; private set; }
    public int ItemsFrom {  get; private set; }
    public int ItemsTo { get; private set; }
    public bool HasNextPage {  get; private set; }
    public bool HasPreviousPage { get; private set; }
}
