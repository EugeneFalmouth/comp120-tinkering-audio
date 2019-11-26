using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    //Takes the value of the slider next to the button clicked in the scene, creates an audio clip and plays it
    public void ButtonClick()
    {
        int frequency = (int)GameObject.Find("SoundFrequency").GetComponent<Slider>().value;
        audioSource.clip = CreateSound(frequency);
        audioSource.Play();
    }

    //Basic single tone sound clip generation
    private AudioClip CreateSound(int frequency)
    {
        int sampleDurationSecs = 5;
        int sampleRate = 44100;
        int sampleLength = sampleRate * sampleDurationSecs;
        float maxValue = 1f / 4f;

        var audioClip = AudioClip.Create("footsteps", sampleLength, 1, sampleRate, false);

        float[] samples = new float[sampleLength];
        for (int i = 0; i < sampleLength; i++)
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));
            float v = s * maxValue;
            samples[i] = v;
        }

        audioClip.SetData(samples, 0);
        return audioClip;
    }
}
