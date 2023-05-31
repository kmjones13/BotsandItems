using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private ObjectManager objectManager;

    private void Update()
    {
        // just went with a simple inputs for this 
        if (Input.GetKeyDown(KeyCode.B))
        {
            objectManager.SpawnBot();
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            objectManager.SpawnItem();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            objectManager.Save();
        }
    }
}
