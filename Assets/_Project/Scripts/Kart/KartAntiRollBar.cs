using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class KartAntiRollBar : MonoBehaviour
{
    public WheelCollider wheelL;
    public WheelCollider wheelR;
    public float antiRollForce = 5000f;

    private Rigidbody kartRigidBody;

    void Start()
    {
        kartRigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        ApplyAntiRoll(wheelL, wheelR);
    }

    private void ApplyAntiRoll(WheelCollider left, WheelCollider right)
    {
        WheelHit hit;
        float travelL = 1.0f;
        float travelR = 1.0f;

        // Check how compressed the left suspension is
        bool groundedL = left.GetGroundHit(out hit);
        if (groundedL)
            travelL = (-left.transform.InverseTransformPoint(hit.point).y - left.radius) / left.suspensionDistance;

        // Check how compressed the right suspension is
        bool groundedR = right.GetGroundHit(out hit);
        if (groundedR)
            travelR = (-right.transform.InverseTransformPoint(hit.point).y - right.radius) / right.suspensionDistance;

        // Calculate the difference and apply force to keep the kart flat
        float antiRollForceFactor = (travelL - travelR) * antiRollForce;

        if (groundedL)
            kartRigidBody.AddForceAtPosition(left.transform.up * -antiRollForceFactor, left.transform.position); 
        if (groundedR)
            kartRigidBody.AddForceAtPosition(right.transform.up * antiRollForceFactor, right.transform.position); 
    }
}