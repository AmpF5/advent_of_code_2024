var filePath = "input.txt";
char[][] fileContent = [];
char[][] outputContent = [];

try
{
    fileContent = File.ReadAllLines(filePath).ToList().Select(x => x.ToCharArray()).ToArray();
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
    return;
}

var xmas = new char[] { 'X', 'M', 'A', 'S' };
var mas = new char[] { 'M', 'A', 'S' };

System.Console.WriteLine("Part 1: " + CeresSearchPart1(xmas, GetCharacter8NeigboursDirections));
System.Console.WriteLine("Part 2: " + CeresSearchPart2(mas, GetCharacterXNeigboursDirections));

int CeresSearchPart1(char[] wordToSearch, Func<int, int, char, IEnumerable<(int, int)>> SearchNeighboursFunc)
{
    var count = 0;
    for (int i = 0; i < fileContent.Length; i++)
    {
        for (int j = 0; j < fileContent[i].Length; j++)
        {
            if (IsCharacterAtIndex(i, j, wordToSearch[0]))
            {
                var directions = SearchNeighboursFunc(i, j, wordToSearch[1]);

                foreach (var direction in directions)
                {
                    if (IsCharacterAtIndex(i + 2 * direction.Item1, j + 2 * direction.Item2, wordToSearch[2]))
                    {
                        if (IsCharacterAtIndex(i + 3 * direction.Item1, j + 3 * direction.Item2, wordToSearch[3]))
                            count++;
                    }
                }
            }
        }
    }

    return count;
}

int CeresSearchPart2(char[] wordToSearch, Func<int, int, char, IEnumerable<(int, int)>> SearchNeighboursFunc)
{
    var count = 0;
    for (int i = 0; i < fileContent.Length; i++)
    {
        for (int j = 0; j < fileContent[i].Length; j++)
        {
            if (IsCharacterAtIndex(i, j, wordToSearch[1]))
            {
                var directions = SearchNeighboursFunc(i, j, wordToSearch[0]);

                if (directions.Count() != 2)
                    continue;

                if (directions.All(direction => IsCharacterAtIndex(i + (-1 * direction.Item1), j + (-1 * direction.Item2), wordToSearch[2])))
                    count++;
            }
        }
    }

    return count;
}

bool IsInBounds(int i, int j) => i >= 0 && i < fileContent.Length && j >= 0 && j < fileContent[i].Length;

bool IsCharacterAtIndex(int i, int j, char character) => IsInBounds(i, j) && fileContent[i][j] == character;

IEnumerable<(int i, int j)> GetCharacter8NeigboursDirections(int i, int j, char neighbourChar)
{
    var directions = new List<(int i, int j)>();

    if (IsInBounds(i, j + 1) && fileContent[i][j + 1] == neighbourChar) directions.Add((0, 1));
    if (IsInBounds(i + 1, j + 1) && fileContent[i + 1][j + 1] == neighbourChar) directions.Add((1, 1));
    if (IsInBounds(i + 1, j) && fileContent[i + 1][j] == neighbourChar) directions.Add((1, 0));
    if (IsInBounds(i + 1, j - 1) && fileContent[i + 1][j - 1] == neighbourChar) directions.Add((1, -1));
    if (IsInBounds(i, j - 1) && fileContent[i][j - 1] == neighbourChar) directions.Add((0, -1));
    if (IsInBounds(i - 1, j - 1) && fileContent[i - 1][j - 1] == neighbourChar) directions.Add((-1, -1));
    if (IsInBounds(i - 1, j) && fileContent[i - 1][j] == neighbourChar) directions.Add((-1, 0));
    if (IsInBounds(i - 1, j + 1) && fileContent[i - 1][j + 1] == neighbourChar) directions.Add((-1, 1));

    return directions;
}

IEnumerable<(int i, int j)> GetCharacterXNeigboursDirections(int i, int j, char neighbourChar)
{
    var directions = new List<(int i, int j)>();

    if (IsInBounds(i + 1, j + 1) && fileContent[i + 1][j + 1] == neighbourChar) directions.Add((1, 1));
    if (IsInBounds(i + 1, j - 1) && fileContent[i + 1][j - 1] == neighbourChar) directions.Add((1, -1));
    if (IsInBounds(i - 1, j - 1) && fileContent[i - 1][j - 1] == neighbourChar) directions.Add((-1, -1));
    if (IsInBounds(i - 1, j + 1) && fileContent[i - 1][j + 1] == neighbourChar) directions.Add((-1, 1));

    return directions;
}



