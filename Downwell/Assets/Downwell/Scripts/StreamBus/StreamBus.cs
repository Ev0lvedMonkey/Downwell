using R3;

public class StreamBus : IService
{
    public readonly Subject<Unit> OnShotEvent = new();
    public readonly Subject<Unit> OnFellToGroundEvent = new();
}

