using Godot;
using System;

public partial class nutrients : Node
{
	public void GetEaten()
	{
		QueueFree();
	}
}
