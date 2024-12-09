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
bool enabled = true;
int sumPart1 = 0, sumPart2 = 0;

foreach(var match in fileContent.SelectMany(ParseLine)) {
    if (match.Groups[1].Success && match.Groups[2].Success) {
        int value = int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
        // Part 1
        sumPart1 += value;

        // Part 2
        if (enabled)
            sumPart2 += value;
    }
    else if (match.Groups[3].Success)
        enabled = false;
        
    else if (match.Groups[4].Success)
        enabled = true;
}

System.Console.WriteLine("Part 1: " + sumPart1);
System.Console.WriteLine("Part 2: " + sumPart2);

static MatchCollection ParseLine(string line) =>
    Regex.Matches(line, @"mul\((\d+),(\d+)\)|(don't)\(\)|(do)\(\)");