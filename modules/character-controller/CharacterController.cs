using Godot;
using System;
using transform;

public class CharacterController : KinematicBody
{
    // Export Variables
    [Export(PropertyHint.Range, "0.1,10.0,0.1")] public float speed = 2.0f;
    [Export] public float acceleration = 1.0f / 0.5f;
    [Export] public float stoppingAcceleration = 10.0f;
    [Export] public float mouseSensitivity = 0.003f;

    // Internal Member
    private Vector3 velocity = Vector3.Zero;

    // Scene Tree Members
    private Camera camera = null;
    private Spatial cameraY = null;

    public override void _Ready()
        // called when the node is added and after all children node are initialized
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
        camera = GetNode<Camera>("CameraRig/YRotation/Camera");
        cameraY = GetNode<Spatial>("CameraRig/YRotation");
    }

    public override void _Process(float delta)
        // called every frame, gets the input and lerps the velocity
    {
        // Get the target input
        var input = Input.GetVector("move_left", "move_right", "move_back", "move_forward");

        // init some helper variables
        var target_velocity = new Vector3();
        var utrans = new UTransform(camera.GlobalTransform);     

        // apply translations to the target velocity to face the relative direction
        target_velocity += utrans.Right * input.x;
        target_velocity += utrans.Forward * input.y;
        target_velocity.y = 0.0f;
        target_velocity = target_velocity.Normalized() * speed;

        // set up the dynamic acceleration
        var target_acceleration = acceleration;
        if (target_velocity.LengthSquared() <= 0.1f) target_acceleration = stoppingAcceleration;

        // User interpolation to smoothly change velocity
        velocity = velocity.LinearInterpolate(target_velocity, target_acceleration * delta);

    }

    public override void _Input(InputEvent e)
        // called once for each Input Event that is generated from Godot
    {
        if (e is InputEventMouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured)
            // checks that we have mouse motion and the mouse is currently captured
        {
            var m_event = e as InputEventMouseMotion; // cast to type
            Vector2 motion = m_event.Relative * mouseSensitivity * -1.0f; // apply scale and invert vector
            
            // rotate camera rig nodes based on the scaled vector
            cameraY.RotateY(motion.x);
            camera.RotateX(motion.y);

        }
        if (e.IsActionPressed("ui_cancel"))
            // checks if we called the 'ui_cancel' input action. Same as Input.IsActionJustPressed("ui_cancel") in polling mode
        {
            // uses a ternary operator to assign the MouseMode in the Input singleton which will affect how the mouse is handled
            Input.MouseMode = (Input.MouseMode == Input.MouseModeEnum.Captured? Input.MouseModeEnum.Visible : Input.MouseModeEnum.Captured);
        }
    }


    public override void _PhysicsProcess(float delta)
        // Called once every physics frame, default is 60 frames per second. Can be changed in ProjectSettings
    {
        // uses built-in function to move the character and assign remaining velocity to the velocity member
        velocity = MoveAndSlide(velocity, Vector3.Up, true, 4, Mathf.Deg2Rad(45.0f));
    }

}
