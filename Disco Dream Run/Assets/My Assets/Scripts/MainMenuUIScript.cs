using UnityEngine;
using TMPro;

public class MainMenuUIScript : MonoBehaviour {

    private Transform playButtonTransform;
    private float sinMoveSpeed;
    private float time;
    private float sinPeriodLength;

    // Use this for initialization
    void Start () {
        sinMoveSpeed = 5.0f;
        time = 0f;
        sinPeriodLength = 0.2f;
        playButtonTransform = GameObject.Find("Play Button").transform;

		if (PlayerPrefs.GetInt("high_score") > 0)
        {
            GameObject.Find("High Score Text").GetComponent<TextMeshProUGUI>()
                .SetText("High Score " + PlayerPrefs.GetInt("high_score"));
        }
	}

    void Update()
    {
        time += Time.deltaTime * sinMoveSpeed;
        Vector3 sinMovement = new Vector3(0, Mathf.Sin(time) * sinPeriodLength, 0);
        playButtonTransform.Translate(sinMovement);
    }

    public void OnPlayButtonClick()
    {
        StartCoroutine(CameraFadeScript.FadeCameraOut("game"));
    }
}
