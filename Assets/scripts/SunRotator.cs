using UnityEngine;

public class SunRotator : MonoBehaviour
{
    public float rotationSpeed = 1.8f; // Speed of rotation in degrees per second

    void Update()
    {
        // Rotate the object around its local X-axis based on the rotation speed and time passed
        transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
    }
}
