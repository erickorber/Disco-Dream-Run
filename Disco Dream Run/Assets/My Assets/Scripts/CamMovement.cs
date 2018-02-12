using UnityEngine;

public class CamMovement : MonoBehaviour {
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = new Vector3(transform.position.x + (15.0f * Time.deltaTime),
            transform.position.y + (9.0f * Time.deltaTime), -30f);
	}
}
