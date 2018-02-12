using System.Collections;
using UnityEngine;

public class FadeBetweenImages : MonoBehaviour {

    public Sprite backgroundOriginal;
    public Sprite backgroundAlternate;
    private float totalFadeTime = 3.0f;
    private SpriteRenderer renderer;

    // Use this for initialization
    void Start () {
        renderer = GetComponent<SpriteRenderer>();
        StartCoroutine(FadeBetweenTwoImages(backgroundOriginal, backgroundAlternate));
	}

    /*
     * Fade out the fadeFrom image, and then fade in the new
     * fadeTo, replacing the original. Call
     */
    private IEnumerator FadeBetweenTwoImages(Sprite fadeFrom, Sprite fadeTo)
    {
        //Fade out the current background
        for (float i = 0.4f; i > 0.02f; i -= 0.02f)
        {
            renderer.color = new Color(1.0f, 1.0f, 1.0f, i);
            yield return new WaitForSeconds(totalFadeTime / 16.0f);
        }

        //Replace the old background with the new one
        renderer.sprite = fadeTo;

        //Fade in the new background
        for (float i = 0.02f; i < 0.4f; i += 0.02f)
        {
            renderer.color = new Color(1.0f, 1.0f, 1.0f, i);
            yield return new WaitForSeconds(totalFadeTime / 16.0f);
        }

        StartCoroutine(FadeBetweenTwoImages(renderer.sprite, fadeFrom));
    }
}
