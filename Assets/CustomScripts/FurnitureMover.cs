using UnityEngine;

public class FurnitureMover : MonoBehaviour
{
    private bool isGrabbed = false; // Indicates if the furniture is being grabbed
    private Transform grabber; // Reference to the controller grabbing the furniture
    private Vector3 initialOffset; // Offset between furniture and controller

    public float rotationStep = 45f; // Rotation angle (45 degrees)
    private bool rotateLeft = false; // Flag for rotating left
    private bool rotateRight = false; // Flag for rotating right

    private void Update()
    {
        if (isGrabbed && grabber != null)
        {
            // Keep the furniture aligned to the controller's position and rotation
            Vector3 newPosition = grabber.position + initialOffset;
            newPosition.y = 0; // Lock furniture to the ground
            transform.position = newPosition;

            // Optional: Align rotation to the controller (without flipping)
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

            // Apply rotation when triggered
            if (rotateLeft)
            {
                RotateFurniture(-rotationStep);
                rotateLeft = false;
            }

            if (rotateRight)
            {
                RotateFurniture(rotationStep);
                rotateRight = false;
            }
        }
    }

    public void Grab(Transform controller)
    {
        if (!isGrabbed)
        {
            isGrabbed = true;
            grabber = controller;

            // Calculate initial offset when grabbed
            initialOffset = transform.position - controller.position;
        }
    }

    public void Release()
    {
        isGrabbed = false;
        grabber = null;
    }

    public void TriggerRotateLeft()
    {
        rotateLeft = true;
    }

    public void TriggerRotateRight()
    {
        rotateRight = true;
    }

    private void RotateFurniture(float angle)
    {
        // Rotate around the Y-axis
        transform.Rotate(0, angle, 0, Space.World);
    }
}
