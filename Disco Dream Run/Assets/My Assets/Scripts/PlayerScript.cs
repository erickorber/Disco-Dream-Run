using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour {
    
    //The UI text for displaying progess to the user
    private TextMeshProUGUI distanceText;

    //Movement speeds on both applicable axis
    public static float xSpeed = 15.0f, ySpeed = 9.0f;

    //Variables for player death
    private bool isPlayerAlive = true;
    public static bool playerDiedFromBackBound;

    //The Collider2D that will store info for all player collisions
    private Collider2D playerHit;

    //For sprite color/opacity manipulations
    private SpriteRenderer renderer;

    //Audio Objects
    private AudioSource audioSource;
    public AudioClip disintegratingSoundEffect;
    public AudioClip fallingSoundEffect;
    public AudioClip deathFromBackBoundSoundEffect;

    private void Awake()
    {
        //Another script is dependant on this boolean, so it should be called
        //before the Start function, using this Awake function
        playerDiedFromBackBound = false;
    }

    // Use this for initialization
    void Start () {
        distanceText = GameObject.FindGameObjectWithTag("Distance Text")
            .GetComponent<TextMeshProUGUI>();
        renderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        //Only execute if the player is alive
        if (isPlayerAlive)
        {
            //Update the counter for keeping track of distance, along with the
            //corresponding UI text component
            DifficultyScript.DISTANCE_TRAVELLED++;
            distanceText.SetText("" + DifficultyScript.DISTANCE_TRAVELLED);

            //Handle any and all player movement;
            MovePlayer();

            //Keep updating the array, since the player is always moving around
            playerHit = Physics2D.OverlapCircle(transform.position, 2.0f);

            //If one of relevant colliders happens to from a
            //'game boundary' GameObject, then kill the player
            if (playerHit != null && (playerHit.tag.Contains("Game Bounds")
                || playerHit.tag == "Bullet"))
            {
                Die(playerHit.tag);
            } 
        }
    }

    /*
     * Handle the user input that deals with player movement, and move the
     * player GameObject accordingly, along with any non-user related
     * movement, except when dealing with death, as that will be dealt with
     * seperately.
     */
    private void MovePlayer()
    {

#if UNITY_EDITOR

        //Calculate which direction to move in (if any)
        //on the x-axis, and to what degree
        float xInput = Input.GetAxis("Horizontal");

        //Calculate which direction to move in (if any)
        //on the y-axis, and to what degree
        float yInput = Input.GetAxis("Vertical");

        //Make the player move more slowly when moving up,
        //as opposed to moving down, to make the running look
        //more natural, since the player is supposed to be constantly
        //running forward and upward, regardless of input.
        if (yInput > 0)
        {
            yInput *= 0.25f;
        }
        else
        {
            yInput *= 0.5f;
        }

        //The same thinking from above, regarding the y-axis,
        //applies to the x-axis. The player should move more
        //quickly while moving backwards, to make the running
        //look more natural overall.
        if (xInput > 0)
        {
            xInput *= 0.25f;
        }
        else
        {
            xInput *= 0.5f;
        }

        //Move the player GameObject
        transform.Translate(new Vector2((xSpeed * Time.deltaTime) + xInput,
            (ySpeed * Time.deltaTime) + yInput));


#elif UNITY_ANDROID

        Vector2 mobileTilt = new Vector2();

        mobileTilt.x = Input.acceleration.x;
        mobileTilt.y = Input.acceleration.y;

        // clamp acceleration vector to the unit sphere
        if (mobileTilt.sqrMagnitude > 1)
        {
            mobileTilt.Normalize();
        }

        //Move the player GameObject
        transform.Translate(new Vector2(xSpeed + (mobileTilt.x * 40.0f),
            ySpeed + (mobileTilt.y * 40.0f)) * Time.deltaTime);
#endif
    }

    /*
     * Called when the player dies. The colliderTag parameter gives clarity to
     * determine what should happen upon death.
     */
    private void Die(string colliderTag)
    {
        //Set this boolean as false to prevent any further player movement,
        //score increase, or collision detection.
        isPlayerAlive = false;

        //If a new high score has been achieved
        if (DifficultyScript.DISTANCE_TRAVELLED >
            PlayerPrefs.GetInt("high_score"))
        {
            //Lock in the new high score to be saved for the next playthrough
            PlayerPrefs.SetInt("high_score",
                DifficultyScript.DISTANCE_TRAVELLED);
        }

        //Make player fall off the map, changing his perceived visual distance
        //from the camera, so he looks like he is behind the floor tiles.
        if (colliderTag == "Far Game Bounds")
        {
            StartCoroutine(FallToDeath(false));
        }

        //Make player fall off the map, with no changes to his perceived visual
        //distance from the camera.
        if (colliderTag == "Near Game Bounds")
        {
            StartCoroutine(FallToDeath());
        }

        //Since the player has dissapeared off camera in this case, just delete
        //all existing 3-piece floor tile segments and fade out to the
        //main menu.
        if (colliderTag == "Back Game Bounds")
        {
            playerDiedFromBackBound = true;
            audioSource.clip = deathFromBackBoundSoundEffect;
            audioSource.Play();
            StartCoroutine(LoadMainMenuAfterDelay(1.3f));
        }

        //Make the player disintegrate with a increasingly red (bullet
        //coloured) tinge to the sprite.
        if (colliderTag == "Bullet")
        {
            StartCoroutine(DisintegratePlayer());
        }
    }

    /*
     * Wait a brief moment and then fades the camera out, subsequently loading
     * the main menu.
     */
    private IEnumerator LoadMainMenuAfterDelay(float timeToWait)
    {
        //Delay
        yield return new WaitForSeconds(timeToWait);

        //Fade out to the main menu
        StartCoroutine(CameraFadeScript.FadeCameraOut("main-menu"));
    }

    /*
     * Visually simulate the player's "disintegration" by fading the sprite's
     * overall colour to red, and then fade the opacity to transparent.
     */
    private IEnumerator DisintegratePlayer()
    {
        audioSource.clip = disintegratingSoundEffect;
        audioSource.Play();

        //Keep the player moving forward depsite death (where normally
        //the player would stop moving).
        StartCoroutine(MovePlayerForwardDuringDeath());

        //Gradually change the player's sprite colour to a full red
        for (float i = 1.0f; i > 0; i -= 0.1f)
        {
            renderer.color = new Color(1.0f, i, i, 1.0f);
            yield return new WaitForSeconds(0.05f);         
        }

        //Gradually change the player's sprite opacity to transparent
        for (float i = 1.0f; i > -0.1; i -= 0.1f)
        {
            renderer.color = new Color(1.0f, 0f, 0f, i);
            yield return new WaitForSeconds(0.05f);
        }

        StartCoroutine(LoadMainMenuAfterDelay(0.01f));
    }

    /*
     * Keeps the player moving forward, while in the process of dying.
     */
    private IEnumerator MovePlayerForwardDuringDeath()
    {
        //So long as the sprite isn't completely transparent, keep moving
        //it forward.
        while(renderer.color.a > 0f)
        {
            transform.Translate(new Vector2(xSpeed, ySpeed)
                * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    /*
     * Make the player fall to his death, since he stepped off of the path.
     * The boolean parameter is just used to differentiate between which side
     * of the path he stepped off from, thus determining how far away from the
     * camera he should placed (using the player's sorting layer).
     */
    private IEnumerator FallToDeath(bool inFrontOfFloorTiles = true)
    {
        audioSource.clip = fallingSoundEffect;
        audioSource.Play();

        if (!inFrontOfFloorTiles)
        {
            renderer.sortingOrder = -1;
        }

        //The time that will be spent falling
        float timeLeftFalling = 0.5f;
        
        //Keep looping until the countdown has been completed
        while (timeLeftFalling > 0)
        {
            //Make the player sprite fall down
            transform.Translate(new Vector2(xSpeed,
                -ySpeed - 3.0f + ((timeLeftFalling - 0.4f) * 120.0f))
                * Time.deltaTime);

            //Reduce the time left to fall
            timeLeftFalling -= Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(LoadMainMenuAfterDelay(0.5f));
    }
}
