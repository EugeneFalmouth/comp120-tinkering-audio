using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdate : MonoBehaviour
{
    public Text labelText;
    public Slider source;
    

    // Updates the text with the value of the attached slider
    void Update()
    {
        labelText.text = source.value.ToString() + " Hz";
    }
}
