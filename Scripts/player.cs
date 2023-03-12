using Godot;
using System;

public partial class player : Area2D
{
	/*
		Runtime shenanigans

		Background system stuff you know what i mean BOI?
	*/
	
	Vector2 ScreenSize;
    public override void _EnterTree()
    {
        ScreenSize = GetViewportRect().Size;

		Animator = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }
	
	public override void _Ready()
	{
		
	}

	int x, y;
	double dolta;
	public override void _Process(double delta)
	{
		dolta = delta;

		Movement();
		AnimationManager();
	}

	/*

		Movement and animations.

		Aka i stole the tutorial code and modified it LMFAO ECKS DEE

	*/
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

	/* 

		Gameplay mechanics and functions

		Aka very big logic

	*/

	public int weight;
	public const int startWeight = 50;
	public void Eat(int amount)
	{
		weight += amount;

		if(weight < startWeight)
		{
			Die();
		}
	}

	public void OnAreaEntered(Area2D food)
	{
		if(food.IsInGroup("Good"))
		{
			Eat(10);
		}
		else if(food.IsInGroup("Bad"))
		{
			Eat(-25);
		}
		else
		{
			GD.Print("what fuq u are?");
		}
	}

	void Die()
	{
		Hide();
	}
}
