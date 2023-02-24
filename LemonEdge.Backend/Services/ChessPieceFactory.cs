using LemonEdge.Backend.Models;
using LemonEdge.Shared;

namespace LemonEdge.Backend.Services;

public interface IChessPieceFactory
{
    List<ChessPieceDescription> GetChessPieces();
    ChessPiece GetChessPiece(string? type);
}

public class ChessPieceFactory : IChessPieceFactory
{
    private readonly ILogger<ChessPieceFactory> _logger;
    public ChessPieceFactory(ILogger<ChessPieceFactory> logger)
    {
        _logger = logger;
    }

    public List<ChessPieceDescription> GetChessPieces()
    {
        return new List<ChessPieceDescription>
        {
            new ChessPieceDescription { Name = "King", UnicodeCharacter = "&#9818;" },
            new ChessPieceDescription { Name = "Queen", UnicodeCharacter = "&#9819;" },
            new ChessPieceDescription { Name = "Knight",UnicodeCharacter = "&#9822;" },
            new ChessPieceDescription { Name = "Bishop",UnicodeCharacter = "&#9821;" },
            new ChessPieceDescription { Name = "Rook",UnicodeCharacter = "&#9820;" },
            new ChessPieceDescription { Name = "Pawn", UnicodeCharacter = "&#9823;" },
        };
    }

    public ChessPiece GetChessPiece(string? type)
    {
        _logger.LogInformation("Received request for chess piece");
        return type switch
        {
            "King" => new King(),
            "Queen" => new Queen(),
            "Knight" => new Knight(),
            "Bishop" => new Bishop(),
            "Rook" => new Rook(),
            "Pawn" => new Pawn(),
            _ => throw new NotSupportedException("Type is not supported"),
        };
    }
}
