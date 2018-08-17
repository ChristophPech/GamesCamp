using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    public Button btnStart;
    public Button btnQuit;
    public Text txtGameOver;
    public Text txtScore;
    public Text txtHighScore;
    public AudioClip sndGameOver;

    static bool firstStart = true;

    // Use this for initialization
    void Start () {
        btnStart.onClick.AddListener(StartGame);
        if(btnQuit != null) btnQuit.onClick.AddListener(QuitGame);

        txtGameOver.gameObject.SetActive(true);
        txtScore.gameObject.SetActive(true);
        if (firstStart)
        {
            firstStart = false;
            Player.Highscore = PlayerPrefs.GetInt("highestScore", Player.Highscore);
            Camera.main.GetComponent<AudioSource>().PlayOneShot(sndGameOver);
            txtScore.gameObject.SetActive(false);
            txtGameOver.gameObject.SetActive(false);
        }

        if (Player.score > Player.Highscore)
        {
            Player.Highscore = Player.score;
            PlayerPrefs.SetInt("highestScore", Player.Highscore);
        }

        if (txtScore != null)
        {
            txtScore.text = "Score: " + Player.score;
        }
        if(txtHighScore != null)
        {
            txtHighScore.text = "Highscore: " + Player.Highscore;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        //Debug.Log("Quit");
        Application.Quit();
    }
}
