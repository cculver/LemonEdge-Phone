namespace LemonEdge.Backend.Models;

public abstract class ChessPiece
{
    protected Dictionary<char, List<char>> _validMoves = new();
    public IReadOnlyDictionary<char, List<char>> ValidMoves { get { return _validMoves; } }
    public abstract string UnicodeCharacter { get; }

    protected ChessPiece()
    {
    }

    private IEnumerable<string> recurseEnumerate(string beginning)
    {
        if (beginning.Length == 7)
            yield return beginning;
        if (beginning.Length < 7)
        {
            char last = beginning[^1];
            foreach (var validMove in _validMoves[last])
            {
                foreach (var returned in recurseEnumerate(beginning + validMove))
                    yield return returned;
            }
        }
    }

    public IEnumerable<string> Enumerate()
    {
        var startingDigits = _validMoves.SelectMany(kvp => kvp.Value).Distinct().OrderBy(a => a).ToArray();
        for (int i = 0; i < startingDigits.Length; i++)
        {
            foreach (var returned in recurseEnumerate(startingDigits[i].ToString()))
            {
                yield return returned;
            }
        }
    }

    public IEnumerable<string> Enumerate(char startingDigit)
    {
        if (_validMoves.ContainsKey(startingDigit))
        {
            foreach (var returned in recurseEnumerate(startingDigit.ToString()))
            {
                yield return returned;
            }
        }
    }
}
