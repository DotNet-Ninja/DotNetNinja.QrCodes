namespace DotNetNinja.QrCodes.Models;

public abstract class Page
{
    public int ItemCount { get; set; }
    public int Number { get; set; }
    public int Size { get; set; }

    public int Count
    {
        get
        {
            if (Size == 0) return 0;
            return (ItemCount % Size == 0) ? ItemCount / Size : ItemCount / Size + 1;
        }
    }

    public bool CanGoForward => Number < Count;
    public bool CanGoBack => Number > 1;

    public IEnumerable<int> GeneratePageNumbers(int maxPagerCount=5)
    {
        if (maxPagerCount >= Count)
        {
            return Enumerable.Range(1, Count);
        }

        var min = Number - (maxPagerCount / 2);
        var max = Number + (maxPagerCount / 2);
        if (min < 1)
        {
            min = 1;
            max = maxPagerCount;
        }

        if (max > Count)
        {
            max = Count;
            min = max - maxPagerCount;
        }
        return Enumerable.Range(min, max);
    }
}

public class Page<T> : Page where T : class
{
    public Page()
    {
        Items=new List<T>();
    }

    public Page(int itemCount, int number, int size)
    {
        ItemCount = itemCount;
        Number = number;
        Size = size;
        Items = new List<T>();
    }

    public Page(int itemCount, int number, int size, List<T> items)
    {
        ItemCount = itemCount;
        Number = number;
        Size = size;
        Items = items;
    }

    public List<T> Items { get; set; }
}