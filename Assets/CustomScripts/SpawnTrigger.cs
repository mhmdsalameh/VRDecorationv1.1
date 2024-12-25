using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnTrigger : MonoBehaviour
{
    public InputActionReference triggerAction; // For grabbing furniture
    public InputActionReference trackpadPressAction; // For detecting trackpad press
    public InputActionReference trackpadPosition; // For detecting trackpad input (direction)
    public float trackpadDeadzone = 0.5f; // Sensitivity for detecting left/right presses

    private FurnitureMover grabbedObject = null; // The currently grabbed furniture object

    void Update()
    {
        // Detect trigger press for grabbing
        if (triggerAction.action.triggered)
        {
            if (grabbedObject == null)
            {
                TryGrabFurniture();
            }
            else
            {
                grabbedObject.Release();
                grabbedObject = null;
            }
        }

        // Detect trackpad press for rotation
        if (grabbedObject != null && trackpadPressAction.action.triggered)
        {
            Vector2 trackpadInput = trackpadPosition.action.ReadValue<Vector2>();

            // Check if the trackpad input is left or right based on the X-axis value
            if (trackpadInput.x > trackpadDeadzone)
            {
                grabbedObject.TriggerRotateRight(); // Rotate 45 degrees to the right
            }
            else if (trackpadInput.x < -trackpadDeadzone)
            {
                grabbedObject.TriggerRotateLeft(); // Rotate 45 degrees to the left
            }
        }
    }

    private void TryGrabFurniture()
    {
        // Raycast from the controller to detect furniture
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 2f)) // Adjust range if needed
        {
            // Check if the hit object has a FurnitureMover component
            FurnitureMover furniture = hit.collider.GetComponent<FurnitureMover>();
            if (furniture != null)
            {
                // Grab the furniture
                grabbedObject = furniture;
                grabbedObject.Grab(transform);
            }
        }
    }
}
