using UnityEngine;

public class FurnitureSpawner : MonoBehaviour
{
    public GameObject[] furniturePrefabs; // Assign furniture prefabs in the Inspector
    public Transform rightController; // Reference to the right controller
    public LayerMask groundLayer; // Set a layer for the ground

    private GameObject selectedFurniture;

    // Select the furniture to spawn (triggered by catalog buttons)
    public void SelectFurniture(int index)
    {
        selectedFurniture = furniturePrefabs[index];
    }

    // Spawn the selected furniture
    public void SpawnFurniture()
    {
        if (selectedFurniture != null)
        {
            // Raycast to find ground
            Ray ray = new Ray(rightController.position, rightController.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
            {
                // Spawn the furniture at the hit point
                Vector3 spawnPosition = hit.point;

                // Adjust the Y position to ensure the furniture is on the ground
                spawnPosition.y += selectedFurniture.GetComponent<Collider>().bounds.extents.y;

                Instantiate(selectedFurniture, spawnPosition, Quaternion.identity);
            }
        }
    }
}
