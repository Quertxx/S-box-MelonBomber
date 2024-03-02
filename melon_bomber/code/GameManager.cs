using Sandbox;

public sealed class GameManager : Component, Component.INetworkListener
{
	protected override void OnUpdate()
	{

	}

	void OnConnected(Connection conn)
    {
    }

    void OnDisconnected(Connection conn)
    {
    }

    void OnActive(Connection conn)
    {
    }

    void OnBecameHost(Connection previousHost)
    {
    }


}