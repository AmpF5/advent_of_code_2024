
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

var count = 0;

for(int i = 0; i < fileContent.Length; i++)
{
    for(int j = 0; j < fileContent[i].Length; j++)
    {
        if(IsCharacterAtIndex(i, j, 'X')) {
            // 'M'
            var directions = GetCharacterNeigboursDirections(i, j, 'M');

            foreach(var direction in directions)
            {
                // 'A'
                if(IsCharacterAtIndex(i + 2*direction.i,j + 2*direction.j, 'A'))
                {
                    if(IsCharacterAtIndex(i + 3*direction.i,  j + 3 * direction.j, 'S'))
                        count++;
                }
            }
        }
    }
}

System.Console.WriteLine(count);


bool IsInBounds(int i, int j) => i >= 0 && i < fileContent.Length && j >= 0 && j < fileContent[i].Length;

bool IsCharacterAtIndex(int i, int j, char character) => IsInBounds(i, j) && fileContent[i][j] == character;

IEnumerable<(int i, int j)> GetCharacterNeigboursDirections(int i, int j, char neighbourChar)
{
    var directions = new List<(int i, int j)>();

    if(IsInBounds(i, j + 1) && fileContent[i][j + 1] == neighbourChar) directions.Add((0, 1));
    if(IsInBounds(i + 1, j + 1) && fileContent[i + 1][j + 1] == neighbourChar) directions.Add((1, 1));
    if(IsInBounds(i + 1, j) && fileContent[i + 1][j] == neighbourChar) directions.Add((1, 0));
    if(IsInBounds(i + 1, j - 1) && fileContent[i + 1][j - 1] == neighbourChar) directions.Add((1, -1));
    if(IsInBounds(i, j - 1) && fileContent[i][j - 1] == neighbourChar) directions.Add((0, -1));
    if(IsInBounds(i - 1, j - 1) && fileContent[i - 1][j - 1] == neighbourChar) directions.Add((-1, -1));
    if(IsInBounds(i - 1, j) && fileContent[i - 1][j] == neighbourChar) directions.Add((-1, 0));
    if(IsInBounds(i - 1, j + 1) && fileContent[i - 1][j + 1] == neighbourChar) directions.Add((-1, 1));

    return directions;
}



