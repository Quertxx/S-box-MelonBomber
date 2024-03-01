using Sandbox;

public sealed class PlayerDresser : Component, Component.INetworkSpawn
{

	[Property]public SkinnedModelRenderer playerBody {get;set;}
	public void OnNetworkSpawn(Connection owner)
	{
		var clothing = new ClothingContainer();
		clothing.Deserialize(owner.GetUserData("avatar"));
		clothing.Apply(playerBody);
	}
}