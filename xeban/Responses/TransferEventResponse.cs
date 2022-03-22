using xeban.Models;

namespace xeban.Responses;

public class TransferEventResponse
{
    public Account? Destination { get; set; }
    public Account? Origin { get; set; }
}