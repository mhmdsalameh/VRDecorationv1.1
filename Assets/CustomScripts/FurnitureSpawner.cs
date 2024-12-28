using UnityEngine;
using UnityEngine.UI;

public class FurnitureSpawner : MonoBehaviour
{
    public GameObject[] furniturePrefabs; // Assign prefabs in Inspector
    public Transform mainCamera;         // Assign the main camera (user's head in VR)

    public void SpawnFurniture(int prefabIndex)
    {
        if (prefabIndex >= furniturePrefabs.Length || prefabIndex < 0)
        {
            Debug.LogWarning("Invalid prefab index.");
            return;
        }

        // Perform a raycast from the user's gaze direction
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.position, mainCamera.forward, out hit, 10f)) // Adjust raycast distance
        {
            Instantiate(furniturePrefabs[prefabIndex], hit.point, Quaternion.identity);
            Debug.Log($"Spawned {furniturePrefabs[prefabIndex].name} at {hit.point}");
        }
        else
        {
            Debug.LogWarning("Raycast did not hit anything.");
        }
    }

    void OnDrawGizmos()
    {
        if (mainCamera != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(mainCamera.position, mainCamera.forward * 10);
        }
    }
}
