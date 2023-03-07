using Godot;
using System;

public partial class player : Area2D
{
	Vector2 ScreenSize;
    public override void _EnterTree()
    {
        ScreenSize = GetViewportRect().Size;

		Animator = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	int x, y;
	double dolta;
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		dolta = delta;

		Movement();
		AnimationManager();
	}

	Vector2 velocity;
	public int Speed = 500;
	void Movement()
	{
		velocity = Vector2.Zero;
		
		//Input 
		if(Input.IsActionPressed("move_right"))
		{
			velocity.X += 1;
			x = 1;
		}
		if(Input.IsActionPressed("move_left"))
		{
			velocity.X -= 1;
			x = 0;
		}
		if(Input.IsActionPressed("move_up"))
		{
			velocity.Y -= 1;
			y = 1;
		}
		if(Input.IsActionPressed("move_down"))
		{
			velocity.Y += 1;
			y = 0;
		}

		//Movement
		Position += velocity * (float)dolta * Speed;
		Position = new Vector2
		(
			//Restricts movement to screen size
			x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
			y: Mathf.Clamp(Position.Y, 0, ScreenSize.Y)
		);
	}

	AnimatedSprite2D Animator;
	void AnimationManager()
	{
		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
			Animator.Play();
		}
		else
		{
			Animator.Play("rotate");
		}

		//For flipping
		if (velocity.X != 0)
		{
			Animator.Animation = "side";
			Animator.FlipH = x <= 0;
		}
		else if (velocity.Y != 0)
		{
			if(y == 0)
			{
				Animator.Animation = "front";
			}

			else if(y == 1)
			{
				Animator.Animation = "back";
			}
		}
		else
		{
			Animator.Animation = "rotate";
		}
	}
}
