using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [SerializeField] private AudioSource backGround;
    [SerializeField] private Slider musicSlider;
    public void setMusic()
    {
        float volume = musicSlider.value; 
        backGround.volume = volume;
    }
}
