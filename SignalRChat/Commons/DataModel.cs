using System.Data;

namespace Commons;

public class DataModel
{
    public string User { get; set; }
    public string Message { get; set; }
    public DateTime Time { get; set; }
    public bool IsAdmin { get; set; }
}