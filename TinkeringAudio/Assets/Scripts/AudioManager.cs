using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    public Slider powerUpSlider;
    public Slider footStepSlider;
    public Slider menuClickSlider;
    public InputField customName;

    private List<char> illegalCharacters;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        illegalCharacters = new List<char>();
        //Symbols in this list are usually not allowed in file names
        illegalCharacters.AddRange(new char[] {'/', '\\', ':', '?', '*', '"', '<', '>', '|' });
    }

    //Takes the value of the slider next to the button clicked in the scene, creates the appropriate audio clip and plays it
    public void ButtonClickPowerUp()
    {
        float frequency = powerUpSlider.value;
        audioSource.clip = CreateSoundPowerUp(frequency);
        audioSource.Play();
    }

    public void ButtonClickFootStep()
    {
        float frequency = footStepSlider.value;
        audioSource.clip = CreateSoundFootStep(frequency);
        audioSource.Play();
    }

    public void ButtonClickMenuClickSound()
    {
        float frequency = menuClickSlider.value;
        audioSource.clip = CreateSoundMenuClick(frequency);
        audioSource.Play();
    }

    //Saves the last played sound clip to %userprofile%\AppData\Local\Packages\<productname>\LocalState as a .wav file if the inputted name is legal
    public void SaveCurrentSoundClip()
    {
        if (audioSource.clip != null)
        {
            string filename;
            bool nameIsLegal = true;
            if (!string.IsNullOrWhiteSpace(customName.text))
            {
                //Check for symbols that are not allowed in file names
                foreach (var letter in customName.text)
                {
                    if (illegalCharacters.Contains(letter))
                    {
                        nameIsLegal = false;
                        break;
                    }
                }
                if (nameIsLegal)
                {
                    filename = customName.text;
                    if (File.Exists(Application.persistentDataPath + "\\" + customName.text + ".wav"))
                    {
                        customName.text += "(1)";
                        filename += "(1)";
                    }
                    SaveWav.Save(filename, audioSource.clip);
                    EditorUtility.DisplayDialog("File Saved", "File succesfully saved to " + Application.persistentDataPath, "Ok");
                }
                else
                {
                    EditorUtility.DisplayDialog("Illegal Name", "The name you put it contains an illegal character. Please input a different name.", "Ok");
                }
            }
            else
            {
                filename = audioSource.clip.name;
                SaveWav.Save(filename, audioSource.clip);
                EditorUtility.DisplayDialog("File Saved", "File succesfully saved to " + Application.persistentDataPath, "Ok");
            }
        }
    }

    //Generating the Power Up sound
    private AudioClip CreateSoundPowerUp(float frequency)
    {
        int sampleDurationSecs = 5;
        int sampleRate = 44100;
        int sampleLength = sampleRate * sampleDurationSecs;
        float maxValue = 1f / 4f;

        var audioClip = AudioClip.Create("powerup", sampleLength, 1, sampleRate, false);

        float[] samples = new float[sampleLength];
        for (int i = (int)(sampleLength * 0.2f); i < (int)(sampleLength * 0.35f); i++)
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * Mathf.Abs(frequency) * ((float)i / (float)sampleRate));
            float v = s * maxValue;
            samples[i] = v;
            frequency += 0.005f;
        }

        audioClip.SetData(samples, 0);
        return audioClip;
    }

    //Generating the Footstep sound
    private AudioClip CreateSoundFootStep(float frequency)
    {
        int sampleDurationSecs = 5;
        int sampleRate = 44100;
        int sampleLength = sampleRate * sampleDurationSecs;
        float maxValue = 1f / 4f;

        var audioClip = AudioClip.Create("footsteps", sampleLength, 1, sampleRate, false);

        float[] samples = new float[sampleLength];
        for (int i = (int)(sampleLength * 0.1f); i < (int)(sampleLength * 0.15f); i++)
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));
            float v = s * maxValue;
            samples[i] = v;
        }
        for (int i = (int)(sampleLength * 0.15f); i < (int)(sampleLength * 0.175f); i++)
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * (frequency * 2) * ((float)i / (float)sampleRate));
            float v = s * maxValue;
            samples[i] = v;
        }

        for (int i = (int)(sampleLength * 0.3f); i < (int)(sampleLength * 0.35f); i++)
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));
            float v = s * maxValue;
            samples[i] = v;
        }
        for (int i = (int)(sampleLength * 0.35f); i < (int)(sampleLength * 0.375f); i++)
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * (frequency * 2) * ((float)i / (float)sampleRate));
            float v = s * maxValue;
            samples[i] = v;
        }

        for (int i = (int)(sampleLength * 0.6f); i < (int)(sampleLength * 0.65f); i++)
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * frequency * ((float)i / (float)sampleRate));
            float v = s * maxValue;
            samples[i] = v;
        }
        for (int i = (int)(sampleLength * 0.65f); i < (int)(sampleLength * 0.675f); i++)
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * (frequency * 2) * ((float)i / (float)sampleRate));
            float v = s * maxValue;
            samples[i] = v;
        }

        audioClip.SetData(samples, 0);
        return audioClip;
    }

    //Generating the Menu Click sound
    private AudioClip CreateSoundMenuClick(float frequency)
    {
        int sampleDurationSecs = 5;
        int sampleRate = 44100;
        int sampleLength = sampleRate * sampleDurationSecs;
        float maxValue = 1f / 4f;

        var audioClip = AudioClip.Create("click", sampleLength, 1, sampleRate, false);

        float[] samples = new float[sampleLength];
        for (int i = (int)(sampleLength * 0.2f); i < (int)(sampleLength * 0.25f); i++)
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * Mathf.Abs(frequency) * ((float)i / (float)sampleRate));
            float v = s * maxValue;
            samples[i] = v;
            frequency -= 0.001f;
        }
        for (int i = (int)(sampleLength * 0.25f); i < (int)(sampleLength * 0.3f); i++)
        {
            float s = Mathf.Sin(2.0f * Mathf.PI * Mathf.Abs(frequency) * ((float)i / (float)sampleRate));
            float v = s * maxValue;
            samples[i] = v;
            frequency += 0.001f;
        }

        audioClip.SetData(samples, 0);
        return audioClip;
    }
}
