using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class AudioDrivenEvents : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    float[] samples = new float[512];
    float[] freqBand = new float[8];
    float[] bandBuffer = new float[8];
    float[] bufferDecrease = new float[8];

    float[] freqBandHighest = new float[8];
    public float[] audioBand = new float[8];
    public float[] audioBandBuffer = new float[8];

    
    // Start is called before the first frame update
    void Start()
    {
        if(audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(audioSource != null)
        {
            audioSource.GetSpectrumData(samples, 0, FFTWindow.Blackman);
            GetFrequencyValues();
            BuildBandBuffer();
            GetAudioBands();
        }
    }

    void GetAudioBands()
    {
        for(int i = 0; i < 8; i++)
        {
            if(freqBand[i] > freqBandHighest[i])
            {
                freqBandHighest[i] = freqBand[i];
            }
            audioBand[i] = (freqBand[i] / freqBandHighest[i]);
            audioBandBuffer[i] = (bandBuffer[i] / freqBandHighest[i]);
        }
    }
    public float GetAudioBand(int index)
    {
        return audioBand[index];
    }

    public float GetAudioBandBuffer(int index)
    {
        return audioBandBuffer[index];
    }

    public float GetSpectrumValue(int index)
    {
        index = Mathf.Clamp(index, 0, 511);
        return samples[index];
    }

    public float GetBufferValue(int index)
    {
        index = Mathf.Clamp(index, 0, 7);
        return bandBuffer[index];
    }

    public float GetFrequencyValue(int index)
    {
        index = Mathf.Clamp(index, 0, 7);
        int sampleCount = (int)Mathf.Pow(2, index + 1);
        float average = 0;
        for(int i = 0; i < sampleCount; i++)
        {
            average += samples[i] * (i + 1);
        }
        average /= sampleCount;
        freqBand[index] = average;
        return freqBand[index];
    }

    public void GetFrequencyValues()
    {
        for(int i = 0; i < 8; i++)
        {
            GetFrequencyValue(i);
        }
    }

    public void BuildBandBuffer()
    {
        for(int g = 0; g < 8; g++)
        {
            if(freqBand[g] > bandBuffer[g])
            {
                bandBuffer[g] = freqBand[g];
                bufferDecrease[g] = 0.005f;
            }
            if(freqBand[g] < bandBuffer[g])
            {
                bandBuffer[g] -= bufferDecrease[g];
                bufferDecrease[g] *= 1.2f;
            }
        }
    }
}
