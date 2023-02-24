namespace LemonEdge.Backend.Models;

public class Knight : ChessPiece
{
    public Knight()
    {
        _validMoves = new Dictionary<char, List<char>>
        {
            ['0'] = new List<char> { '4', '6' },
            ['1'] = new List<char> { '6', '8' },
            ['2'] = new List<char> { '7', '9' },
            ['3'] = new List<char> { '4', '8' },
            ['4'] = new List<char> { '0', '3', '9' },
            ['5'] = new List<char> { },
            ['6'] = new List<char> { '0', '1', '7' },
            ['7'] = new List<char> { '2', '6' },
            ['8'] = new List<char> { '1', '3' },
            ['9'] = new List<char> { '2', '4' },
        };
    }

    public override string UnicodeCharacter { get { return "&#9822;"; } }
}
