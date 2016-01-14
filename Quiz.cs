using System;
using System.Collections.Generic;
using System.Linq;


interface IDataItem
{
    int Data { get; set; }
}

struct DataItem : IDataItem
{
    private int data; // Unused field
    public int Data

    {
        get { return Data; } // Recursion instead of using the currently unused field
        set { data = value; }
    }
}

interface ICalculator<T> where T : IDataItem
{
    void Calculate(T item);
    event Action<int> OnCalculated;
}

abstract class BaseCalculator<TItem> : ICalculator<TItem> where TItem : IDataItem
{
    private readonly TItem recentItem = default(TItem); // Redundant initialization?
    public virtual event Action<int> OnCalculated;


    public void Calculate(TItem item)
    {
        int d = 0;
        for (int i = 0; i < recentItem.Data; i++)
        {
            for (int j = 0; j < item.Data; i++) // 'j' is not incremented
            {
                d += i * j;
            }
        }

        recentItem.Data = item.Data; // Assignment to a property of a readonly field can be useless. Field type is not known to be reference type

        if (OnCalculated != null)
            OnCalculated(d); // Invocation of polymorphic field-like event
    }
}

class Calculator : BaseCalculator<DataItem>
{
    public override event Action<int> OnCalculated;
}

class MainClass
{
    public static void Main()
    {
        var calculators = new List<ICalculator<DataItem>>();
        for (int i = 0; i < 10; i++)
        {
            calculators.Add(new Calculator());
            calculators.Last().OnCalculated +=
                result => Console.WriteLine("Result for {1}-th caluclator is {2}", i, result); // Format string errors + access to modified closure
        }

        calculators.ForEach(calculator => calculator.Calculate(new DataItem {Data = 0}));
    }
}