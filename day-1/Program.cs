using System.Collections;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace day_1;

class Program
{
    static void Main(string[] args)
    {
        var sum = 0;
        var count =0;
        Console.WriteLine("Input please: ");
        string fileName = Console.ReadLine() ?? "";

        if (fileName == string.Empty) {
            fileName = "test-input.txt";
        }

        using (StreamReader reader = new StreamReader($"input-files/{fileName}"))
        {
            string? line = "";
            while ((line = reader.ReadLine()) != null) {
                count++;
                var firstAndLastNumberCombined = GetTheFirstAndLastNumber(line);
                Console.WriteLine($"Line number: {count} Line output: {firstAndLastNumberCombined}");
                sum += firstAndLastNumberCombined;
            }
        }

        Console.WriteLine(sum);
    }

    private static int GetTheFirstAndLastNumber(string line) {
       // var pattern = @"\d+";
        var pattern =  @"(?:one|two|three|four|five|six|seven|eight|nine|\d+)";
        //var patternFromGPT = @"\b(?:one|two|three|four|five|six|seven|eight|nine|\d+)\b";
        var numbers = Regex.Matches(line, pattern);
        int firstDigit = 0;
        int lastDigit = 0;

        
        var fullFirstNumber = GetNumberFromText(numbers[0].Value, true);
        var fullLastNumber = GetNumberFromText(numbers[numbers.Count -1].Value, false);
        if (fullFirstNumber == null || fullLastNumber == null)
            throw new IndexOutOfRangeException();

        firstDigit = int.Parse(fullFirstNumber.Substring(0,1));
    
        if (numbers.Count == 1)
        {
            if (fullFirstNumber.Length > 1) {
                lastDigit = int.Parse(fullFirstNumber.Substring(fullFirstNumber.Length - 1));
            } else {
                lastDigit = firstDigit;
            }
        } else {
            lastDigit = int.Parse(fullLastNumber.Substring(fullLastNumber.Length - 1));
        }

        return firstDigit * 10 + lastDigit;

    }

    private static string? GetNumberFromText(string number, bool isFirstValue) {
        
        int a;

        if (int.TryParse(number, out a))
        {
            return number;
        }
        string lowerCaseNumber = number.ToLower();

        switch(lowerCaseNumber) {
            case "one":
                return "1";
            case "two":
                return "2";
            case "three":
                return "3";
            case "four":
                return "4";
            case "five":
                return "5";
            case "six":
                return "6";
            case "seven":
                return "7";
            case "eight":
                return "8";
            case "nine":
                return "9";
            
        }
        return null;
    }

    
}
