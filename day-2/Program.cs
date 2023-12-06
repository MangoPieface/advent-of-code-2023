using System.Collections;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Day2;

public class Program
{
    static void Main(string[] args)
    {
        

        var sum = 0;
        var count = 0;
        Console.WriteLine("Input please: ");
        string fileName = Console.ReadLine() ?? "";

        if (fileName == string.Empty)
        {
            fileName = "full-input.txt";
        }


        using (StreamReader reader = new StreamReader($"input-files/{fileName}"))
        {
            string? line = "";
            while ((line = reader.ReadLine()) != null)
            {
                count++;
                if (IsValidGame(line))
                {
                    sum += count;
                }
            }
        }
        Console.WriteLine(sum.ToString());
    }

    public static bool IsValidGame(string game)
    {

        int maxRedCubes = 12;
        int maxGreenCubes = 13;
        int maxBlueCubes = 14;

        var lines = game.Split(';');
        string pattern = @"(\d+)\s*([a-zA-Z]+)";

        foreach (string line in lines)
        {
            List<string> cubes = new List<string>();
            MatchCollection matches = Regex.Matches(line, pattern);

            foreach (Match match in matches)
            {
                int number = Int32.Parse(match.Groups[1].Value);
                string colour = match.Groups[2].Value;

                for (int i = 0; i < number; i++)
                {
                    cubes.Add(colour);
                }
            }
            var tooManyRedCubes = cubes.Count(x => x == "red") > maxRedCubes;
            var tooManyGreenCubes = cubes.Count(x => x == "green") > maxGreenCubes;
            var tooManyBlueCubes = cubes.Count(x => x == "blue") > maxBlueCubes;

            if (tooManyRedCubes || tooManyGreenCubes || tooManyBlueCubes)
            {
                return false;
            }
        }

        return true;
    }
    
}
