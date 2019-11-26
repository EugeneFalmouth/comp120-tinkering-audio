using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdate : MonoBehaviour
{
    public Text labelText;
    

    // Updates the text with the value of the slider
    void Update()
    {
        labelText.text = GameObject.Find("SoundFrequency").GetComponent<Slider>().value.ToString() + " Hz";
    }
}
