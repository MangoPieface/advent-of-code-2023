using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace day_1;

class Program
{
    static void Main(string[] args)
    {
        var count = 0;
        Console.WriteLine("Input please: ");
        string fileName = Console.ReadLine() ?? "";

        if (fileName == string.Empty) {
            fileName = "full-input.txt";
        }

        using (StreamReader reader = new StreamReader($"input-files/{fileName}"))
        {
            string? line = "";
            while ((line = reader.ReadLine()) != null) {
                
                var finalNumber = GetTheFirstAndLastNumber(line);
                Console.WriteLine(finalNumber);
                count += finalNumber;
            }
        }

        Console.WriteLine(count);
    }

    private static int GetTheFirstAndLastNumber(string line) {
        var pattern = @"\d+";

        var numbers = Regex.Matches(line, pattern);
        int firstDigit = 0;
        int lastDigit = 0;

        var fullFirstNumber = numbers[0].Value;
        firstDigit = int.Parse(fullFirstNumber.Substring(0,1));
    
        if (numbers.Count == 1)
        {
            if (fullFirstNumber.Length > 1) {
                lastDigit = int.Parse(fullFirstNumber.Substring(fullFirstNumber.Length - 1));
            } else {
                lastDigit = firstDigit;
            }
        } else {
            var fullLastNumber = numbers[numbers.Count -1].Value;
            lastDigit = int.Parse(fullLastNumber.Substring(fullLastNumber.Length - 1));
        }

        return firstDigit * 10 + lastDigit;

    }

    
}
