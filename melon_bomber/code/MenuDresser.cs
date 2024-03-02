using Sandbox;

public sealed class MenuDresser : Component
{

	protected override void OnAwake()
	{
		base.OnAwake();
		if(Components.TryGet<SkinnedModelRenderer>(out var model))
		{
			var clothing = ClothingContainer.CreateFromLocalUser();
			clothing.Apply(model);
		}
	}
	protected override void OnUpdate()
	{

	}
}