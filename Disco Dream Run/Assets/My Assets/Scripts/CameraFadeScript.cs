using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraFadeScript : MonoBehaviour {
    
    private static Image cameraOverlay;
    public static float totalFadeTime = 0.3f;

    // Use this for initialization
    void Start () {
        cameraOverlay = GameObject.Find("Black Overlay").GetComponent<Image>();
        StartCoroutine(FadeCameraIn());
	}

    /*
     * Simulate a Camera fade-in, by actually fading OUT a black overlay
     * that covers all other UI elements and gameobjects, transforming it
     * from opaque to transparent.
     */
	private IEnumerator FadeCameraIn()
    {
        for (float i = 1.0f; i > 0; i -= 0.1f)
        {
            cameraOverlay.color = new Color(0, 0, 0, i);
            yield return new WaitForSeconds(totalFadeTime / 10.0f);
        }

        //Disable this overlay so that UI buttons that would normally
        //be covered up (and therefore rendered essentially useless)
        //can actually be clicked.
        cameraOverlay.enabled = false;
    }

    /*
     * Simulate a Camera fade-out, by actually fading IN a black overlay
     * that covers all other UI elements and gameobjects, transforming it
     * from transparent to opaque.
     */
    public static IEnumerator FadeCameraOut(string sceneToFadeTo)
    {
        //Enable this overlay, since the overlay is always disabled upon
        //finishing a fade out.
        cameraOverlay.enabled = true;

        for (float i = 0.0f; i < 1.0f; i += 0.1f)
        {
            cameraOverlay.color = new Color(0, 0, 0, i);
            yield return new WaitForSeconds(totalFadeTime / 10.0f);
        }

        //Fade out to a new scene
        SceneManager.LoadScene(sceneToFadeTo);
    }
}
