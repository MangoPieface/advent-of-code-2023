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
            fileName = "full-input.txt";
        }

        using (StreamReader reader = new StreamReader($"input-files/{fileName}"))
        {
            string? line = "";
            while ((line = reader.ReadLine()) != null) {
                count++;
                MatchCollection numbers = GetMatches(line);
                if (numbers.Count > 0) {
                    var firstAndLastNumberCombined = GetTheFirstAndLastNumber(line, numbers);
                    //Console.WriteLine($"Line number: {count} Line output: {firstAndLastNumberCombined}");
                    sum += firstAndLastNumberCombined;
                }
            }
        }

        Console.WriteLine("Total :" + sum);
    }

    public static int GetTheFirstAndLastNumber(string line, MatchCollection numbers)
    {
        int firstDigit;
        int lastDigit;

        var fullFirstNumber = GetNumberFromText(numbers[0].Value, line, true);
        var fullLastNumber = GetNumberFromText(numbers[numbers.Count - 1].Value, line, false);
        if (fullFirstNumber == null || fullLastNumber == null)
            throw new IndexOutOfRangeException();

        firstDigit = int.Parse(fullFirstNumber.Substring(0, 1));

        if (numbers.Count == 1)
        {
            if (fullFirstNumber.Length > 1)
            {
                lastDigit = int.Parse(fullFirstNumber.Substring(fullFirstNumber.Length - 1));
            }
            else
            {
                lastDigit = firstDigit;
            }
        }
        else
        {
            lastDigit = int.Parse(fullLastNumber.Substring(fullLastNumber.Length - 1));
        }

        return firstDigit * 10 + lastDigit;

    }

   

    private static string? GetNumberFromText(string number, string inputLine, bool isFirstValue) {
        
        int a;

        if (int.TryParse(number, out a))
        {
            return number;
        }

        if (!isFirstValue)
        {
            number = EdgeCaseForMissingLastNumber(number, inputLine);
        }

        switch (number.ToLower()) {
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

    /// <summary>
    /// hack to work around edge case
    /// in an example where the sequene is '126dzbvg6two4oneightntd' the regex will find 'one'
    /// but not 'eight', as 'oneight' shares the same last letter
    /// so here we just check if we really have found the last number.
    /// yeah, I should have written some tests, it would have said me a lot of hassle.
    /// </summary>
    /// <param name="number"></param>
    /// <param name="inputLine"></param>
    /// <returns></returns>
    private static string EdgeCaseForMissingLastNumber(string number, string inputLine)
    {
        var positionOfEndOfThelastNumberFoundAsTextExlcludingLastLetter = inputLine.LastIndexOf(number) + number.Length - 1;
        var remainingInputFile = inputLine.Substring(positionOfEndOfThelastNumberFoundAsTextExlcludingLastLetter, inputLine.Length - positionOfEndOfThelastNumberFoundAsTextExlcludingLastLetter);
        //still might be wrong if there are few in the string, but lets try this
        var lastNumberInInputFile = GetMatches(remainingInputFile);

        if (lastNumberInInputFile.Count > 0  && lastNumberInInputFile[0].Value != number)
        {
            number = lastNumberInInputFile[0].Value;
            //Console.WriteLine("Here is a problem line " + inputLine);

        }
        return number;
    }

    private static MatchCollection GetMatches(string line)
    {
        var pattern = @"(?:one|two|three|four|five|six|seven|eight|nine|\d+)";
        //var patternFromGPT = @"\b(?:one|two|three|four|five|six|seven|eight|nine|\d+)\b";
        var numbers = Regex.Matches(line, pattern);
        return numbers;
    }
    
}
