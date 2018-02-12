using UnityEngine;

public class DifficultyScript : MonoBehaviour {

    //The gauge for determining when difficulty is increased,
    //though not necessarily used as an actual "distance"
    //calculator per se, but rather a incrementing ticker
    //that goes up continuously as the player keeps travelling.
    public static int DISTANCE_TRAVELLED;

    //Global multipliers that will be the source of difficulty increases
    //over time
    public static float ENEMY_SPAWN_FREQUENCY_MULTIPLIER;
    public static float ENEMY_BULLET_SPEED_MULTIPLIER;

    //The number of times the distance has reached a multiple of 500
    private int num250Iterations;

    // Use this for initialization
    void Start () {
        DISTANCE_TRAVELLED = 1;
        ENEMY_SPAWN_FREQUENCY_MULTIPLIER = 1.0f;
        ENEMY_BULLET_SPEED_MULTIPLIER = 1.0f;
        num250Iterations = 0;
    }
	
	// Update is called once per frame
	void Update () {

        //Every 250 units travelled, increase difficulty
		if (DISTANCE_TRAVELLED % 250 == 0)
        {
            num250Iterations++;

            //Increase difficulty in one of three potential ways,
            //which depend on how far the player has travelled
            //overall, in multiples of 250 units

            if (num250Iterations <= 5)
            {
                //Have the enemies spawn more frequently
                ENEMY_SPAWN_FREQUENCY_MULTIPLIER += 0.1f;
                //Update the necessary spawn rate float
                GenerateBullet.REPEAT_RATE = 1.0f
                    / ENEMY_SPAWN_FREQUENCY_MULTIPLIER;
            }

            if (num250Iterations > 5 && num250Iterations <= 20)
            {
                //Increase the speed of the enemy's travelling bullet
                ENEMY_BULLET_SPEED_MULTIPLIER += 0.05f;
            }

            if (num250Iterations > 20)
            {
                //Increase the speed of the enemy's travelling bullet
                ENEMY_BULLET_SPEED_MULTIPLIER += 0.1f;
            }
        }
	}
}
