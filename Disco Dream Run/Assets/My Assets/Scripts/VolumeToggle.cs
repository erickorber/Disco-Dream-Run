using UnityEngine;
using UnityEngine.UI;

public class VolumeToggle : MonoBehaviour {

    public Sprite volumeOnSprite;
    public Sprite volumeOffSprite;
    private Image volumeImage;
    private AudioSource audioSource;

    private void Awake()
    {
        volumeImage = GetComponent<Image>();

        audioSource = GameObject.FindGameObjectWithTag("Music")
            .GetComponent<AudioSource>();

        //If no key exists yet
        if (!PlayerPrefs.HasKey("audio_enabled"))
        {
            //Set enabled
            PlayerPrefs.SetInt("audio_enabled", 1);
        }

        if (PlayerPrefs.GetInt("audio_enabled") == 1)
        {
            volumeImage.sprite = volumeOnSprite;
            audioSource.mute = false;
        }
        else
        {
            volumeImage.sprite = volumeOffSprite;
            audioSource.mute = true;
        }
    }

    /*
     * Called by clicking the toggle-volume button on the main menu's UI.
     */
    public void ToggleVolume()
    {
        //If audio is currently disabled
        if (PlayerPrefs.GetInt("audio_enabled") == 0)
        {
            volumeImage.sprite = volumeOnSprite;
            audioSource.mute = false;
            PlayerPrefs.SetInt("audio_enabled", 1);
        }
        //If audio is currently enabled
        else
        {
            volumeImage.sprite = volumeOffSprite;
            audioSource.mute = true;
            PlayerPrefs.SetInt("audio_enabled", 0);
        }
    }
}
