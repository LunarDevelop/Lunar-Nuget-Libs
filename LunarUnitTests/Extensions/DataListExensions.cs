using LunarExtensions.Data;

namespace LunarUnitTests;

public class Data_ListsExtensions
{

    private List<String> ogList;
    private List<List<String>> testListList;

    [SetUp]
    public void Setup()
    {
        ogList = new List<string>()
        {
            "1",
            "2",
            "3",
            "4",
            "5",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16"
        };

        testListList = new List<List<string>>()
        {
            new List<string>()
            {
                "1",
                "2",
                "3",
                "4",
                "5"
            },
            new List<string>()
            {
                "11",
                "12",
                "13",
                "14",
                "15"
            },
            new List<string>()
            {
                "16"
            }
        };
    }

    [Test]
    public void Test1()
    {
        List<String> curTestList = new List<string>(ogList);

        Assert.AreEqual(curTestList.SplitList(5), testListList);
    }
}