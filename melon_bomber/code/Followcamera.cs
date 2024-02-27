using Sandbox;

public sealed class Followcamera : Component
{
	private CameraComponent mainCam;

	protected override void OnStart()
	{
		base.OnStart();
		mainCam = Scene.Camera;
	}
	protected override void OnUpdate()
	{

	}

	protected override void OnFixedUpdate()
	{
		base.OnFixedUpdate();
		if(IsProxy)
		return;
		mainCam.Transform.Position = new Vector3(GameObject.Transform.Position.x, GameObject.Transform.Position.y, GameObject.Transform.Position.z + 400);

	}
}