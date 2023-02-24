namespace LemonEdge.Backend.Models;

public class Pawn : ChessPiece
{
    public Pawn()
    {
        _validMoves = new Dictionary<char, List<char>>
        {
            ['0'] = new List<char> { '7', '9' },
            ['1'] = new List<char> { '5' },
            ['2'] = new List<char> { '4', '6' },
            ['3'] = new List<char> { '5' },
            ['4'] = new List<char> { '2', '8' },
            ['5'] = new List<char> { '1', '3', '7', '9' },
            ['6'] = new List<char> { '2', '8' },
            ['7'] = new List<char> { '0', '5' },
            ['8'] = new List<char> { '4', '6' },
            ['9'] = new List<char> { '0', '5' },
        };
    }

    public override string UnicodeCharacter { get { return "&#9823;"; } }
}
