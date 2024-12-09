using System.Text.RegularExpressions;

var filePath = "input.txt";
string[] fileContent = [];

try {
    fileContent = File.ReadAllLines(filePath);
}
catch (Exception ex) {
    Console.WriteLine($"An error occurred: {ex.Message}");
    return;
}

var sum = fileContent.SelectMany(ParseLine).Select(p => p.a * p.b).Sum();
Console.WriteLine(sum);

static IEnumerable<(int a, int b)> ParseLine(string line) =>
    Regex.Matches(line, @"mul\((\d+),(\d+)\)").Select(m => (int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value)));