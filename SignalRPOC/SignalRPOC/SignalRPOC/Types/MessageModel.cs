namespace SignalRPOC.Commons.Types;

public class MessageModel
{
    public string User { get; set; }
    public object Payload { get; set; }
    public DateTime Time { get; set; }
}