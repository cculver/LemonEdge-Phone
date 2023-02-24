using LemonEdge.Backend.Services;
using LemonEdge.Shared;
using Microsoft.AspNetCore.Mvc;
using System.IO.Pipelines;

namespace LemonEdge.Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PhoneListController : ControllerBase
    {
        private readonly ILogger<PhoneListController> _logger;
        private readonly IChessPieceFactory _factory;

        public PhoneListController(ILogger<PhoneListController> logger, IChessPieceFactory factory)
        {
            _logger = logger;
            _factory = factory;
        }

        [HttpGet("pieces")]
        public IEnumerable<ChessPieceDescription> GetPieces()
        {
            return _factory.GetChessPieces();
        }

        [HttpGet("numbers/{chessPiece}")]
        public IEnumerable<ChessPieceNumber> Get([FromRoute] string chessPiece)
        {
            using var logscope = _logger.BeginScope(new Dictionary<string, object>
            {
                ["ChessPiece"] = chessPiece,
            });

            if (chessPiece == "All")
            {
                foreach (var pieceType in _factory.GetChessPieces())
                {
                    foreach (var number in _factory.GetChessPiece(pieceType.Name).Enumerate())
                    {
                        yield return new ChessPieceNumber { Number = number, UnicodeCharacter = pieceType.UnicodeCharacter };
                    }
                }
            }
            else
            {
                var piece = _factory.GetChessPiece(chessPiece);
                foreach (var number in piece.Enumerate())
                {
                    yield return new ChessPieceNumber { Number = number, UnicodeCharacter = piece.UnicodeCharacter };
                }
            }
        }

        [HttpGet("numbers/{chessPiece}/{startingNumber}")]
        public IEnumerable<ChessPieceNumber> Get([FromRoute] string chessPiece, [FromRoute] string startingNumber)
        {
            using var logscope = _logger.BeginScope(new Dictionary<string, object>
            {
                ["ChessPiece"] = chessPiece,
                ["StartingNumber"] = startingNumber,
            });

            if (startingNumber.Length != 1 || !char.IsDigit(startingNumber[0]))
            {
                _logger.LogError("Invalid starting number provided");
                throw new ArgumentException("Starting number must be a single digit.", nameof(startingNumber));
            }

            if (chessPiece == "All")
            {
                foreach (var pieceType in _factory.GetChessPieces())
                {
                    foreach (var number in _factory.GetChessPiece(pieceType.Name).Enumerate(startingNumber[0]))
                    {
                        yield return new ChessPieceNumber { Number = number, UnicodeCharacter = pieceType.UnicodeCharacter };
                    }
                }
            }
            else
            {
                var piece = _factory.GetChessPiece(chessPiece);
                foreach (var number in piece.Enumerate(startingNumber[0]))
                {
                    yield return new ChessPieceNumber { Number = number, UnicodeCharacter = piece.UnicodeCharacter };
                }
            }
        }

        [HttpGet("numbers/raw/{chessPiece}")]
        public IActionResult GetRaw([FromRoute] string chessPiece)
        {
            using var logscope = _logger.BeginScope(new Dictionary<string, object>
            {
                ["ChessPiece"] = chessPiece,
            });

            HashSet<string> set = new();
            if (chessPiece == "All")
            {
                foreach (var pieceType in _factory.GetChessPieces())
                {
                    foreach (var number in _factory.GetChessPiece(pieceType.Name).Enumerate())
                    {
                        if (!set.Contains(number))
                            set.Add(number);
                    }
                }
            }
            else
            {
                var piece = _factory.GetChessPiece(chessPiece);
                foreach (var number in _factory.GetChessPiece(chessPiece).Enumerate())
                {
                    if (!set.Contains(number))
                        set.Add(number);
                }
            }

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            foreach (var number in set.OrderBy(a=>a))
            {
                writer.WriteLine(number);
            }
            writer.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "text/plain", chessPiece + ".txt");
        }

        [HttpGet("numbers/raw/{chessPiece}/{startingNumber}")]
        public IActionResult GetRaw([FromRoute] string chessPiece, [FromRoute] string startingNumber)
        {
            using var logscope = _logger.BeginScope(new Dictionary<string, object>
            {
                ["ChessPiece"] = chessPiece,
                ["StartingNumber"] = startingNumber,
            });

            if (startingNumber.Length != 1 || !char.IsDigit(startingNumber[0]))
            {
                _logger.LogError("Invalid starting number provided");
                throw new ArgumentException("Starting number must be a single digit.", nameof(startingNumber));
            }


            HashSet<string> set = new();
            if (chessPiece == "All")
            {
                foreach (var pieceType in _factory.GetChessPieces())
                {
                    foreach (var number in _factory.GetChessPiece(pieceType.Name).Enumerate(startingNumber[0]))
                    {
                        if (!set.Contains(number))
                            set.Add(number);
                    }
                }
            }
            else
            {
                var piece = _factory.GetChessPiece(chessPiece);
                foreach (var number in _factory.GetChessPiece(chessPiece).Enumerate(startingNumber[0]))
                {
                    if (!set.Contains(number))
                        set.Add(number);
                }
            }

            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            foreach (var number in set.OrderBy(a => a))
            {
                writer.WriteLine(number);
            }
            writer.Flush();
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "text/plain", chessPiece + "-" + startingNumber + ".txt");
        }
    }
}