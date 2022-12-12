using Netcode.Client;

namespace Netcode;

public abstract class APacketServer : APacket
{
    /// <summary>
    /// The packet handled client-side (Godot thread)
    /// </summary>
    public virtual async Task Handle() => await Task.FromResult(0);
}
