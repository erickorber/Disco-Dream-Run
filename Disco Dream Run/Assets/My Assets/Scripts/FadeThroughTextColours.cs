using UnityEngine;
using TMPro;

public class FadeThroughTextColours : MonoBehaviour {

    private TextMeshProUGUI textMesh;
    private float sinAlternatingSpeed;
    private float time;
    private float sinPeriodLength;
    private VertexGradient colorGradientCopy;

    // Use this for initialization
    void Start () {
        textMesh = GetComponent<TextMeshProUGUI>();
        sinAlternatingSpeed = 5.0f;
        time = 0f;
        sinPeriodLength = 0.08f;
        colorGradientCopy = textMesh.colorGradient;
    }

    void Update()
    {
        time += Time.deltaTime * sinAlternatingSpeed;

        float amountToAddToColorValues = -(Mathf.Sin(time) * sinPeriodLength);

        Color newBottomLeftColor = new Color(colorGradientCopy.bottomLeft.r
            + amountToAddToColorValues, colorGradientCopy.bottomLeft.g
            + amountToAddToColorValues, colorGradientCopy.bottomLeft.b
            + amountToAddToColorValues);

        Color newBottomRightColor = new Color(colorGradientCopy.bottomRight.r
            + amountToAddToColorValues, colorGradientCopy.bottomRight.g
            + amountToAddToColorValues, colorGradientCopy.bottomRight.b
            + amountToAddToColorValues);

        textMesh.colorGradient = new VertexGradient(colorGradientCopy.topLeft,
            colorGradientCopy.topRight, newBottomLeftColor,
            newBottomRightColor);
    }
}
