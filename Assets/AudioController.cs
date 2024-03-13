using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{

    AudioSource audioSource;
    [SerializeField]
    AudioClip[] audioClips;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void SetAudio(AudioClip clip)
    {
        audioSource.clip = clip;
    }

    void OnGUI()
    {
        if (audioClips != null)
        {
            for (int i = 0; i < audioClips.Length; i++)
            {
                if (GUI.Button(new Rect(10, 10 + (40 * i), 250, 30), "Play " + audioClips[i].name))
                {
                    SetAudio(audioClips[i]);
                    audioSource.Play();
                }
            }
        }
    }
}

