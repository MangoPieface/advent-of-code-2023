using System;
using System.Collections;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using static System.Formats.Asn1.AsnWriter;

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
            fileName = "full";
        }

        using (StreamReader reader = new StreamReader($"input-files/{fileName}-input.txt"))
        {
            string? game = "";
            while ((game = reader.ReadLine()) != null)
            {
                count++;
                sum += GetCubeGames(game);
            }
        }

        Console.WriteLine("The total is " + sum);
        
    }

    public static int GetCubeGames(string game)
    {
        int redCubes = 0;
        int greenCubes = 0;
        int blueCubes = 0;

        string pattern = @"(\d+)\s*([a-zA-Z]+)";
              
        MatchCollection matches = Regex.Matches(game, pattern);

        foreach (Match match in matches)
        {
            int number = Int32.Parse(match.Groups[1].Value);
            string colour = match.Groups[2].Value;

            switch (colour.ToLower())
            {
                case "blue":
                    if (number > blueCubes)
                        blueCubes = number;
                    break;
                case "green":
                    if (number > greenCubes)
                        greenCubes = number;
                    break;
                default:
                    if (number > redCubes)
                        redCubes = number;
                    break;
            }

        }

        return redCubes * blueCubes * greenCubes;

    }    
}
