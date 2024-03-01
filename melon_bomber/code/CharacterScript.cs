using System;
using System.Numerics;
using Sandbox;
using Sandbox.Citizen;

public sealed class CharacterScript : Component, Component.ICollisionListener
{

	[Category("Stats")]
	[Property]public float speed {get; set;} = 100;
	[Property]public bool canMove {get;set;} = true;
	private float defaultSpeed {get; set;} = 100;
	[Category("Stats")]
	[Property] public int bombs {get;set;} = 1;
	private int defaultBombs {get;set;} = 1;
	[Category("Stats")]
	[Property]public int power {get;set;} = 1;
	[Property]public GameObject Melon{get;set;}
	private int defaultPower {get;set;} = 1;
	[Category("Stats")]
	[Property] public int bomblength {get;set;} = 50;
	private int bomblengthDefault {get;set;} = 50;
	[Category("Body")]
	[Property]public GameObject body;
	[Property]public GameObject head;
	private CharacterController characterCC;
	public CitizenAnimationHelper animator;
	public SkinnedModelRenderer model;
	private Vector3 wishVelocity;
	[Property]public int placedBombs {get;set;} = 0;
	protected override void OnAwake()
	
	{
		if(IsProxy)
		return;
		base.OnAwake();
		characterCC = GameObject.Components.Get<CharacterController>();
		animator = GameObject.Components.Get<CitizenAnimationHelper>();
		model = body.Components.Get<SkinnedModelRenderer>();
		
	}
	protected override void OnStart()
	{
		if(IsProxy)
		return;
		base.OnStart();
		if(model!=null)
		{
			var clothing = ClothingContainer.CreateFromLocalUser();
			clothing.Apply(model);
			
		}
		speed = defaultSpeed;
		power = defaultPower;
		bombs = defaultBombs;
		bomblength = bomblengthDefault;
		
		
	}
	protected override void OnUpdate()
	{
		if(IsProxy)
		return;
		if(characterCC.IsOnGround)
		{
			characterCC.Acceleration = 10f;
			characterCC.ApplyFriction(5f);
		}
		else
		{
			characterCC.ApplyFriction(5f);
			characterCC.Velocity += Scene.PhysicsWorld.Gravity * Time.Delta;
		}
				if(animator != null)
		{
			animator.WithVelocity(characterCC.Velocity);
			animator.WithWishVelocity(wishVelocity);
			//animator.WithLook(head.Transform.Rotation.Forward, 1f, 0.75f, 0.5f);
			
		}
		
		if(Input.Pressed("attack1"))
		{
			if(placedBombs < bombs)
			{
				placedBombs++;
				GameObject melonbomb = Melon.Clone(new Vector3(GameObject.Transform.Position.x, GameObject.Transform.Position.y, GameObject.Transform.Position.z + 30));
				melonbomb.NetworkSpawn();
				
				melonbomb.Components.Get<MelonScript>().script = this;
				melonbomb.Components.Get<MelonScript>().bombLength = bomblength;
			}

		}
		
	}
	protected override void OnFixedUpdate()
	{
		if(IsProxy)
		return;
		base.OnFixedUpdate();
		wishVelocity = Vector3.Zero;
		wishVelocity = Input.AnalogMove.Normal * speed;
		if(canMove)
		{
			characterCC.Accelerate(wishVelocity);
			characterCC.Move();
		}
		
		
		rotate();
	}

	public void rotate()
	{
		if(wishVelocity != Vector3.Zero)
		{
			var movementDir = new Vector3(Input.AnalogMove.x,Input.AnalogMove.y, 0);
			movementDir = movementDir.Normal;
			var newnewRot = Rotation.LookAt(movementDir.Normal, GameObject.Transform.Rotation.Up);
			newnewRot = Rotation.Slerp(body.Transform.Rotation, newnewRot, 5f * Time.Delta);
			body.Transform.Rotation = newnewRot;
		}
		
		
	}

	
	public void OnCollisionStart(Collision other)
	{
		if(IsProxy)
		return;
	}
	public void OnCollisionStop(CollisionStop other)
	{
		if(IsProxy)
		return;
	}
	public void OnCollisionUpdate(Collision other)
	{
		if(IsProxy)
		return;
	}
}