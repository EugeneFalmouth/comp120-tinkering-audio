using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUpdate : MonoBehaviour
{
    public Text labelText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        labelText.text = GameObject.Find("SoundFrequency").GetComponent<Slider>().value.ToString() + " Hz";
    }
}
