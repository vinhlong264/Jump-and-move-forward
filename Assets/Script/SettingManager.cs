using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private GameObject pauseGameObj;

    private void Start()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            loadVolume();
        }
        else
        {
            setMusic();
            setSFX();
        }
    }
    public void setMusic()
    {
        float volumeMusic = musicSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(volumeMusic) * 20);
        PlayerPrefs.SetFloat("musicVolume" , volumeMusic);
    }

    public void setSFX()
    {
        float volumeSFX = sfxSlider.value;
        myMixer.SetFloat("SFX" ,Mathf.Log10(volumeSFX) * 20);
        PlayerPrefs.SetFloat("musicSFX" , volumeSFX);
    }

    public void PauseGame()
    {
        if(pauseGameObj != null)
        {
            pauseGameObj.SetActive(true);
            PlayerStats.Instance.noJump = true;
            Time.timeScale = 0f;
        }
    }

    public void ReseumeGame()
    {
        if(pauseGameObj != null)
        {
            pauseGameObj.SetActive(false);
            Time.timeScale = 1f;
            PlayerStats.Instance.noJump = false;
        }
    }


    public void loadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("musicSFX");
    }
}
