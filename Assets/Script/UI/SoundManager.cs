using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Slider audioSlider;

    public void AudioControl()
    {
        float sound = audioSlider.value;

        if (sound == audioSlider.minValue)
            masterMixer.SetFloat("BGM", -80); 
        // -40일때 -80을 설정한 이유는 음소거 효과를 내기 위함
        else
            masterMixer.SetFloat("BGM", sound);
    }
}
