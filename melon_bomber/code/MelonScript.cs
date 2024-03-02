using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Sandbox;

public sealed class MelonScript : Component
{
	public CharacterScript script;
	private Capsule capsule;
	[Property]public GameObject powerUp;
	public int bombLength;
	
	protected override async void OnStart()
	{
		base.OnStart();
		capsule.Radius = 15f;
		await explosionLogic();
		
	}
	protected override void OnUpdate()
	{
		base.OnUpdate();
		Gizmo.Draw.Line(GameObject.Transform.Position, (Vector3.Forward * bombLength) + GameObject.Transform.Position);
		Gizmo.Draw.Line(GameObject.Transform.Position, (Vector3.Left * bombLength) + GameObject.Transform.Position);
		Gizmo.Draw.Line(GameObject.Transform.Position, (Vector3.Right * bombLength) + GameObject.Transform.Position);
		Gizmo.Draw.Line(GameObject.Transform.Position, (Vector3.Backward * bombLength) + GameObject.Transform.Position);
	}

	protected override void OnEnabled()
	{
		base.OnEnabled();
		
	}
	private async Task explosionLogic()
	{
		await GameTask.Delay(3000);
		explosion();
		script.placedBombs--;
		this.GameObject.Destroy();
	}

	private void explosion()
	{
		var forwardRay2 = Scene.Trace.Capsule(capsule,GameObject.Transform.Position, (Vector3.Forward*bombLength) + GameObject.Transform.Position)
		.RunAll();
		var leftRay2 = Scene.Trace.Capsule(capsule,GameObject.Transform.Position, (Vector3.Left*bombLength) + GameObject.Transform.Position)
		.RunAll();
		var rightRay2 = Scene.Trace.Capsule(capsule,GameObject.Transform.Position, (Vector3.Right*bombLength) + GameObject.Transform.Position)
		.RunAll();
		var backRay2 = Scene.Trace.Capsule(capsule,GameObject.Transform.Position, (Vector3.Backward*bombLength) + GameObject.Transform.Position)
		.RunAll();
		
		if(forwardRay2.ToArray().Length > 0)
		{
			destroyMultipleObjects(forwardRay2.ToArray());
		}
		if(leftRay2.ToArray().Length > 0)
		{
			destroyMultipleObjects(leftRay2.ToArray());
		}
		if(rightRay2.ToArray().Length > 0)
		{
			destroyMultipleObjects(rightRay2.ToArray());
		}
		if(backRay2.ToArray().Length > 0)
		{
			destroyMultipleObjects(backRay2.ToArray());
		}

		
	}


	private void destroyMultipleObjects(SceneTraceResult[] hitResults)
	{
		foreach(SceneTraceResult hitresult in hitResults)
		{
			if(hitResults[0].GameObject.Tags.Has("wall"))
			{
				if(hitresult.GameObject.Tags.Has("player"))
				{
					Log.Info("Dead");
					
					break;
				}
				if(hitresult.GameObject.Tags.Has("wall"))
				{
					destroyNetworkobjects(hitresult);
					break;
				}
				if(hitresult.GameObject.Tags.Has("player"))
				{
					
					Log.Info("Dead");
				}
			}
			else if(hitResults[0].GameObject.Tags.Has("player"))
			{
				
				Log.Info("Dead");
			}
			else
			{
				Log.Info(hitresult.GameObject);
				Log.Info("Hit nothing of concequence");
			}
		}
		
	}

	
	private void destroyNetworkobjects(SceneTraceResult hitresult)
	{
		Random r = new Random();
		var randomIndex = r.Next(0, 6);
		switch(randomIndex)
		{
		case 0:
		GameObject power = powerUp.Clone(hitresult.GameObject.Transform.Position);
		power.NetworkSpawn();
		hitresult.GameObject.Destroy();
		break;
		case 1:
		power = powerUp.Clone(hitresult.GameObject.Transform.Position);
		power.NetworkSpawn();
		hitresult.GameObject.Destroy();
		break;
		case 2:
		power = powerUp.Clone(hitresult.GameObject.Transform.Position);
		power.NetworkSpawn();
		hitresult.GameObject.Destroy();
		break;
		case 3:
		hitresult.GameObject.Destroy();
		break;
		case 4:
		hitresult.GameObject.Destroy();
		break;
		case 5:
		hitresult.GameObject.Destroy();
		break;
		}
	}

}