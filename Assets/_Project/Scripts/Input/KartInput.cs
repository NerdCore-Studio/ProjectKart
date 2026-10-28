using UnityEngine;

public class KartInput : MonoBehaviour
{
    public float Steering { get; private set; }
    public float Acceleration { get; private set; }
    public bool IsBraking { get; private set; }

    void Update()
    {
        // Reads WASD or Left Stick / Triggers on a controller
        Steering = Input.GetAxis("Horizontal");
        Acceleration = Input.GetAxis("Vertical");
        
        // Spacebar or controller button for handbrake
        IsBraking = Input.GetKey(KeyCode.Space);
    }
}