using System.Collections;
using UnityEngine;

public class FloorBlockScript : MonoBehaviour {

    private SpriteRenderer renderer;

    // Use this for initialization
    void Start () {
        renderer = GetComponent<SpriteRenderer>();
        Invoke("ChangeFloorTileColor", 0.001f);
    }

    void ChangeFloorTileColor()
    {
        int randomColor = Random.Range(1, 8);

        switch (randomColor)
        {
            case 1:
                StartCoroutine(FadeToOpaque(Color.white));
                break;
            case 2:
                StartCoroutine(FadeToOpaque(Color.red));
                break;
            case 3:
                StartCoroutine(FadeToOpaque(Color.blue));
                break;
            case 4:
                StartCoroutine(FadeToOpaque(Color.green));
                break;
            case 5:
                StartCoroutine(FadeToOpaque(Color.yellow));
                break;
            case 6:
                StartCoroutine(FadeToOpaque(Color.magenta));
                break;
            case 7:
                StartCoroutine(FadeToOpaque(Color.cyan));
                break;
            case 8:
                //Orange
                StartCoroutine(FadeToOpaque(new Color(255, 150, 0)));
                break;
        }
    }

    IEnumerator FadeToOpaque(Color colorToFadeInto)
    {
        Color c = colorToFadeInto;
        for (float f = 0f; f <= 0.9; f += 0.1f)
        {
            c.a = f;
            renderer.color = c;
            yield return null;
        }
    }
}
