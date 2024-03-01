using Sandbox;
using Sandbox.Network;

public sealed class NetworkManager : Component
{
	/*
	protected override async void OnStart()
	{
		base.OnStart();
		
		var list = await GameNetworkSystem.QueryLobbies();
		if (list.ToArray().Length>0)
		{
			GameNetworkSystem.Connect(list.First().LobbyId);
		}
		else
		{
			GameNetworkSystem.CreateLobby();
		}
	}
	protected override void OnUpdate()
	{

	}*/
}