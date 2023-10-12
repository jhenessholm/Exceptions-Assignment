// See https://aka.ms/new-console-template for more information


using System;
using System.Collections.Generic;
using System.IO;

public class WordsDTO
{
    public string[] StringArray { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
}
class Program
{
    static void Main()
    {
        string input = "10,20,30,40,50,60,70,80,90,100";
        List<int> numbers = ParseStringToIntList(input);

        Console.WriteLine("Parsed numbers in descending order:");
        foreach (int number in numbers)
        {
            Console.WriteLine(number);
        }
        string filePath = "Sample.txt";
        WordsDTO result = ReadFileAndCreateWordsDTO(filePath);

        if (result.Success)
        {
            Console.WriteLine("\nFile Read Success");
            Console.WriteLine();
            Console.WriteLine("Words:");
            Console.WriteLine();
            string[] words = result.StringArray;
            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];

                string cleanedWord = word
                    .Replace("{", "")
                    .Replace("}", "")
                    .Replace("\"", "")
                    .Replace(",", "\n")
                    .Replace(":", ": ");

                if (i > 0)
                {
                    Console.WriteLine();
                }
                Console.WriteLine(cleanedWord);
            }
        }
        else
        {
            Console.WriteLine("\nFile Read Failed");
            Console.WriteLine();
            Console.WriteLine("Message: " + result.Message);
        }
        Console.WriteLine();
        Console.WriteLine("Press Enter to exit...");
        Console.ReadLine();
    }
    static List<int> ParseStringToIntList(string input)
    {
        List<int> numbers = new List<int>();
        string[] parts = input.Split(',');
        foreach (string part in parts)
        {
            try
            {
                int number = int.Parse(part);
                numbers.Add(number);
            }
            catch (FormatException)
            {
                Console.WriteLine("Entry discarded");
            }
        }
        numbers.Sort((a, b) => b.CompareTo(a));
        return numbers;
    }
    static WordsDTO ReadFileAndCreateWordsDTO(string filePath)
    {
        WordsDTO dto = new WordsDTO();
        try
        {
            string text = File.ReadAllText(filePath);
            string[] words = text.Split('\n');
            dto.StringArray = words;
            dto.Success = true;
            dto.Message = "Success";
        }
        catch (FileNotFoundException)
        {
            dto.Success = false;
            dto.Message = "File not found";
        }
        return dto;
    }
}




