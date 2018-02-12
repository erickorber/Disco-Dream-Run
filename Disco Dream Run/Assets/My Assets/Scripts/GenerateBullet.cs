using UnityEngine;

public class GenerateBullet : MonoBehaviour {

    //The prefab to be generated
    public GameObject bullet;

    public static float REPEAT_RATE;
    private float countdown;
    private Transform cameraTransform;

    // Use this for initialization
    void Start () {
        //The repeat rate, in seconds
        REPEAT_RATE = 1.0f / DifficultyScript.ENEMY_SPAWN_FREQUENCY_MULTIPLIER;
        countdown = REPEAT_RATE;
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera")
            .transform;
    }
	
	// Update is called once per frame
	void Update () {

        //Always count down
        countdown -= Time.deltaTime;

        //Once the countdown has reached zero
        if (countdown < 0)
        {
            float random = Random.value;

            //Randomly pick between one of two possible outcomes
            if (random < 0.33)
            {
                //Generate bullet in the far lane
                Instantiate(bullet,
                    new Vector2(cameraTransform.position.x + 17.0f,
                    cameraTransform.position.y + 18.0f), Quaternion.identity);
            }
            else if (random >= 0.33 && random < 0.66)
            {
                //Generate bullet in the middler lane
                Instantiate(bullet,
                    new Vector2(cameraTransform.position.x + 24.0f,
                    cameraTransform.position.y + 16.0f), Quaternion.identity);
            }
            else
            {
                //Generate bullet in the near lane
                Instantiate(bullet,
                    new Vector2(cameraTransform.position.x + 29.0f,
                    cameraTransform.position.y + 12.5f), Quaternion.identity);
            }
            
            //Reset the countdown
            countdown = REPEAT_RATE;
        }
    }
}
