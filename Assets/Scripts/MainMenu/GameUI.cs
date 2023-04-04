using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameUI : MonoBehaviour
{
    public Image fadePlane;
    public GameObject gameOverUI;
    

    [SerializeField] PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.CheckDeath()) {
            OnGameOver();
        }
    }

    void OnGameOver() 
    {
        StairFunction.currentLevel = StairFunction.START_LEVEL_INDEX;
        StartCoroutine(Fade(Color.clear, Color.black, 1));
        gameOverUI.SetActive(true);
    }


    IEnumerator Fade(Color from,Color to, float time) {
        float speed = 1 / time;
        float percent = 0;
        while (percent <1 ) {
            percent += Time.deltaTime * speed;
            fadePlane.color = Color.Lerp(from, to, percent);
            yield return null;
        }
    }

    // UI Input 
    public void StartNewGame() {
        SceneManager.LoadScene("SampleScene");
    }

    


}
