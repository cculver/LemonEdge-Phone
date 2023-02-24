namespace LemonEdge.Backend.Models;

public class Rook : ChessPiece
{
    public Rook()
    {
        _validMoves = new Dictionary<char, List<char>>
        {
            ['0'] = new List<char> { '2', '5', '8' },
            ['1'] = new List<char> { '2', '3', '4', '7' },
            ['2'] = new List<char> { '0', '1', '3', '5', '8' },
            ['3'] = new List<char> { '1', '2', '6', '9' },
            ['4'] = new List<char> { '1', '5', '6', '7' },
            ['5'] = new List<char> { '0', '2', '4', '6', '8' },
            ['6'] = new List<char> { '3', '4', '5', '9' },
            ['7'] = new List<char> { '1', '4', '8', '9' },
            ['8'] = new List<char> { '0', '2', '5', '7', '9' },
            ['9'] = new List<char> { '3', '6', '7', '8' },
        };
    }

    public override string UnicodeCharacter { get { return "&#9820;"; } }
}
