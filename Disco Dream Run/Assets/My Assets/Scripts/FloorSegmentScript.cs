using UnityEngine;

public class FloorSegmentScript : MonoBehaviour {
    
    // Use this for initialization
    void Start () {
        Invoke("Die", 2.2f);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
