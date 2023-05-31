using UnityEngine;

public class Player : MonoBehaviour
{
    //have player check for closet object only when it moved
    //could continuously check but this can be a little but of optimization 
    [SerializeField] private ObjectManager objectManager;
    
    private void Update()
    {
        if (transform.hasChanged)
        {
            objectManager.HighlightClosestObject(transform.position);
        }
    }
}
