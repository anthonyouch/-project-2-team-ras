using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{

    void Start() {
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
    

}
