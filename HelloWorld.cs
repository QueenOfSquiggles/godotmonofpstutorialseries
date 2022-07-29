using Godot;
using System;

public class HelloWorld : Spatial
{
    public override void _Ready()
    {
        GD.Print("Hello, world! <3");
    }

}
