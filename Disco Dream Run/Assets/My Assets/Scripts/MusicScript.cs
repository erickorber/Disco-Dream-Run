using UnityEngine;

public class MusicScript : MonoBehaviour {

    private GameObject[] duplicates;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        duplicates = GameObject.FindGameObjectsWithTag("Music");
        
        //If there are duplicates
        if (duplicates.Length > 1)
        {
            //Keep the original, since you don't want music to ever restart
            for (int i = 1; i < duplicates.Length; i++)
            {
                //Destroy any duplicates
                Destroy(duplicates[i]);
            }
        }
    }
}
