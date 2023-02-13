namespace LunarExtensions.Data;

public static class ListExtensions
{
    /// <summary>
    /// Splits a list into several smaller list, splitting the list
    /// </summary>
    /// <param name="size">Size of each list</param>
    /// <returns>List of split lists</returns>
    public static List<List<T>> SplitList<T>(this List<T> oldList, int size = 50)
    {
        var splitList = new List<List<T>>();
        for (var i = 0; i < oldList.Count; i += size)
            splitList.Add(oldList.GetRange(i, Math.Min(size, oldList.Count - i)));
        return splitList;
    }
}