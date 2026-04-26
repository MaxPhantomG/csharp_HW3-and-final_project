using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Тестируем Distinct
        List<int> numbers = new List<int> { 1, 2, 2, 3, 4, 4, 5 };
        List<int> distinctNumbers = CollectionUtils.Distinct(numbers);
        Console.WriteLine("Distinct numbers: " + string.Join(", ", distinctNumbers));  // Выведет: 1, 2, 3, 4, 5

        List<string> words = new List<string> { "apple", "banana", "apple", "orange" };
        List<string> distinctWords = CollectionUtils.Distinct(words);
        Console.WriteLine("Distinct words: " + string.Join(", ", distinctWords));  // Выведет: apple, banana, orange

        // Тестируем GroupBy
        List<string> wordList = new List<string> { "apple", "banana", "cherry", "date", "fig", "grape" };
        var groupedByLength = CollectionUtils.GroupBy(wordList, w => w.Length);
        foreach (var group in groupedByLength)
        {
            Console.WriteLine($"Length {group.Key}: {string.Join(", ", group.Value)}");
        }

        // Тестируем Merge
        Dictionary<string, int> firstDict = new Dictionary<string, int> { { "apple", 3 }, { "banana", 2 } };
        Dictionary<string, int> secondDict = new Dictionary<string, int> { { "banana", 4 }, { "cherry", 5 } };
        var mergedDict = CollectionUtils.Merge(firstDict, secondDict, (firstValue, secondValue) => firstValue + secondValue);

        Console.WriteLine("Merged Dictionary:");
        foreach (var kvp in mergedDict)
        {
            Console.WriteLine($"{kvp.Key}: {kvp.Value}");
        }

        // Тестируем MaxBy
        List<Product> products = new List<Product>
        {
            new Product { Name = "Product1", Price = 10 },
            new Product { Name = "Product2", Price = 20 },
            new Product { Name = "Product3", Price = 15 }
        };

        var mostExpensive = CollectionUtils.MaxBy(products, p => p.Price);
        Console.WriteLine($"Most expensive product: {mostExpensive.Name} - ${mostExpensive.Price}");
    }
}
