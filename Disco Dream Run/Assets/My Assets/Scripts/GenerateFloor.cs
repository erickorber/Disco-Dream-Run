using UnityEngine;

public class GenerateFloor : MonoBehaviour {

    //The prefab to be generated repeatedly
    public GameObject floorSegment;

    //Used in calculating the position for the new floor segment
    private int numFloorsGenerated = 0;

    // Use this for initialization
    void Start () {
        //Generate a new floor segment every 0.333 seconds, after
        //a slight initial delay (doesn't seem to work without
        //that tiny delay)
        InvokeRepeating("GenerateFloorSegment", 0.001f, 0.333f);
    }

    /*
     * Generate a new three-piece floor segment via the corresponding
     * prefab.
     */
    void GenerateFloorSegment()
    {
        //This condition is here so that floor segments can stop being
        //generated once the player has died in a particular way.
        if (!PlayerScript.playerDiedFromBackBound)
        {
            //Generate the floor segment and determine its vector position
            Instantiate(floorSegment,
                new Vector2(20.0f + (5.0f * numFloorsGenerated),
                12.0f + (3.0f * numFloorsGenerated)), Quaternion.identity);

            //Update this int, so it can be used accurately in determing the
            //position for the next, new floor segment
            numFloorsGenerated++;
        }
    }
}
