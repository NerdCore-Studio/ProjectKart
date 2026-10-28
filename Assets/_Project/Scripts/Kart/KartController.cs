using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(KartInput))]
public class KartController : MonoBehaviour
{
    [Header("Wheel Colliders")]
    public WheelCollider frontLeftWheel;
    public WheelCollider frontRightWheel;
    public WheelCollider rearLeftWheel;
    public WheelCollider rearRightWheel;

    [Header("Engine & Handling")]
    public float maxMotorTorque = 300f; // Adjusted for kart weight
    public float maxSteeringAngle = 30f;
    public float brakeTorque = 1000f;

    private Rigidbody kartRigidbody;
    private KartInput kartInput;

    void Start()
    {
        kartRigidbody = GetComponent<Rigidbody>();
        kartInput = GetComponent<KartInput>();
        
        // Sim-cade secret: Lower the center of mass dramatically so the kart doesn't flip in corners
        kartRigidbody.centerOfMass = new Vector3(0, -0.5f, 0);
    }

    void FixedUpdate()
    {
        // Physics calculations must happen in FixedUpdate
        HandleMotor();
        HandleSteering();
        HandleBraking();
    }

    private void HandleMotor()
    {
        // Rear-wheel drive
        float torque = kartInput.Acceleration * maxMotorTorque;
        rearLeftWheel.motorTorque = torque;
        rearRightWheel.motorTorque = torque;
    }

    private void HandleSteering()
    {
        float steering = kartInput.Steering * maxSteeringAngle;
        frontLeftWheel.steerAngle = steering;
        frontRightWheel.steerAngle = steering;
    }

    private void HandleBraking()
    {
        float currentBrake = kartInput.IsBraking ? brakeTorque : 0f;
        
        // Apply brakes to all four wheels for stability
        frontLeftWheel.brakeTorque = currentBrake;
        frontRightWheel.brakeTorque = currentBrake;
        rearLeftWheel.brakeTorque = currentBrake;
        rearRightWheel.brakeTorque = currentBrake;
    }
}