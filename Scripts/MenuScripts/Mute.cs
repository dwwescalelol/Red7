using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mute : MonoBehaviour
{
    // Create input values
    private Sprite soundOnImage;
    public Sprite soundOffImage;
    public Button button;
    private bool isOn = true;

    public AudioSource audioSource1;
    public AudioSource audioSource2;
    public AudioSource audioSource3;

    // Start is called before the first frame update
    void Start()
    {
        soundOnImage = button.image.sprite;
    }

    // Mutes all the audio sources when mute button is clicked
    public void ButtonClicked()
    {
        if (isOn)
        {
            button.image.sprite = soundOffImage;
            isOn = false;
            audioSource1.mute = true;
            audioSource2.mute = true;
            audioSource3.mute = true;
        }
        else
        {
            button.image.sprite = soundOnImage;
            isOn = true;
            audioSource1.mute = false;
            audioSource2.mute = false;
            audioSource3.mute = false;
        }
    }
}
