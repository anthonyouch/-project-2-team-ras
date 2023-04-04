using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public Slider volumeSlider;

    void Start() {
        volumeSlider.value = 0.5f;
    }

    public void PlayGame() 
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void QuitGame()
    {
        //Debug.Log("QUIT");
        Application.Quit();
    }
    public void SetMasterVolume() {
        //Debug.Log(volumeSlider.value);
        AudioManager.Instance.SetVolume(volumeSlider.value);
    }


}
