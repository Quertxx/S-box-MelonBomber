using System;
using Microsoft.VisualBasic;
using Sandbox;

public sealed class PowerUP : Component, Component.ITriggerListener
{
		enum Powerup
	{
		speed,
		power,
		bomb
	}
	
	[Property]Powerup upgrade {get;set;} = Powerup.speed;
	private int randomIndex;
	protected override void OnStart()
	{
		base.OnStart();
		Random r = new Random();
		randomIndex = r.Next(0, 3);

		switch(randomIndex)	
		{
			case 0:
			upgrade = Powerup.speed;
			break;
			case 1:
			upgrade = Powerup.power;
			break;
			case 2:
			upgrade = Powerup.bomb;
			break;
		}
	}
	protected override void OnUpdate()
	{

	}

	public void OnTriggerEnter(Collider other)
	{
		if(other.GameObject.Tags.Has("player"))
		{
			var script = other.GameObject.Components.Get<CharacterScript>();
			if(script != null)
			{
				switch(upgrade)
				{
					case Powerup.speed:
					script.speed = script.speed + 100;
					break;
					case Powerup.power:
					script.power++;
					script.bomblength = script.bomblength + 50;
					break;
					case Powerup.bomb:
					script.bombs++;
					break;
				}
				GameObject.Destroy();
			}

		}
	}
	public void OnTriggerExit(Collider other)
	{
		
	}



}