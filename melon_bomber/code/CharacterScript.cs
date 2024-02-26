using System;
using System.Numerics;
using Sandbox;
using Sandbox.Citizen;

public sealed class CharacterScript : Component, Component.ICollisionListener
{

	[Category("Stats")]
	[Property]public float speed {get; set;} = 100;
	[Category("Body")]
	[Property]public GameObject body;
	[Property]public GameObject head;
	private CharacterController characterCC;
	private CitizenAnimationHelper animator;
	private ModelRenderer model;
	private Vector3 wishVelocity;
	protected override void OnAwake()
	{
		base.OnAwake();
		characterCC = GameObject.Components.Get<CharacterController>();
		animator = GameObject.Components.Get<CitizenAnimationHelper>();
		model = body.Components.Get<ModelRenderer>();
	}
	protected override void OnStart()
	{
		base.OnStart();
	}
	protected override void OnUpdate()
	{
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
			animator.WithLook(head.Transform.Rotation.Forward, 1f, 0.75f, 0.5f);
			
		}
		
		
	}
	protected override void OnFixedUpdate()
	{
		base.OnFixedUpdate();
		wishVelocity = Vector3.Zero;
		wishVelocity = Input.AnalogMove * speed;
		characterCC.Accelerate(wishVelocity);
		characterCC.Move();
		
		rotate();
	}

	public void rotate()
	{
		var movementDir = new Vector3(Input.AnalogMove.x,Input.AnalogMove.y, 0);
		movementDir = movementDir.Normal;
		var newnewRot = Rotation.LookAt(movementDir.Normal, GameObject.Transform.Rotation.Up);
		newnewRot = Rotation.Slerp(body.Transform.Rotation, newnewRot, 5f * Time.Delta);
		body.Transform.Rotation = newnewRot;
		
	}
	public void OnCollisionStart(Collision other){}
	public void OnCollisionStop(CollisionStop other){}
	public void OnCollisionUpdate(Collision other){}
}