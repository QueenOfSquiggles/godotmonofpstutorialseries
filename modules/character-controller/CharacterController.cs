using Godot;
using System;

public class CharacterController : KinematicBody
{

    [Export(PropertyHint.Range, "0.1,10.0,0.1")] public float speed = 2.0f;
    [Export] public float acceleration = 1.0f / 0.5f;
    [Export] public float stoppingAcceleration = 10.0f;


    private Vector3 velocity = Vector3.Zero;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        var input = Input.GetVector("move_left", "move_right", "move_back", "move_forward");
        var target_velocity = new Vector3();
        target_velocity.x = input.x * speed;
        target_velocity.z = -input.y * speed;
        var target_acceleration = acceleration;
    
        if (target_velocity.LengthSquared() <= 0.1f) target_acceleration = stoppingAcceleration;
        velocity = velocity.LinearInterpolate(target_velocity, target_acceleration * delta);

    }


    public override void _PhysicsProcess(float delta)
    {
        velocity = MoveAndSlide(velocity, Vector3.Up, true, 4, Mathf.Deg2Rad(45.0f));
    }

}
