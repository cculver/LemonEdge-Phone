namespace LemonEdge.Backend.Models;

public class Queen : ChessPiece
{
    public Queen()
    {
        _validMoves = new Dictionary<char, List<char>>
        {
            ['0'] = new List<char> { '2', '5', '7', '8', '9' },
            ['1'] = new List<char> { '2', '3', '4', '5', '7', '9' },
            ['2'] = new List<char> { '0', '1', '3', '4', '5', '6', '8' },
            ['3'] = new List<char> { '1', '2', '5', '6', '7', '9' },
            ['4'] = new List<char> { '1', '2', '5', '6', '7', '8' },
            ['5'] = new List<char> { '0', '1', '2', '3', '4', '6', '7', '8', '9' },
            ['6'] = new List<char> { '2', '3', '4', '5', '8', '9' },
            ['7'] = new List<char> { '0', '1', '3', '4', '5', '8', '9' },
            ['8'] = new List<char> { '0', '2', '4', '5', '6', '7', '9' },
            ['9'] = new List<char> { '0', '1', '3', '5', '6', '7', '8' },
        };
    }

    public override string UnicodeCharacter { get { return "&#9819;"; } }
}