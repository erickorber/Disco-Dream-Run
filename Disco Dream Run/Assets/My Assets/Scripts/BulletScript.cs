using UnityEngine;

public class BulletScript : MonoBehaviour {

    private float bulletSpeedX;
    private float bulletSpeedY;

    // Use this for initialization
    void Awake () {
        Invoke("Die", 2.5f);
        bulletSpeedX = -7.5f
            * DifficultyScript.ENEMY_BULLET_SPEED_MULTIPLIER;
        bulletSpeedY = -4.5f
            * DifficultyScript.ENEMY_BULLET_SPEED_MULTIPLIER;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector2(bulletSpeedX, bulletSpeedY)
            * Time.deltaTime);
	}

    // Bullet gets destroyed
    private void Die()
    {
        Destroy(gameObject);
    }
}
