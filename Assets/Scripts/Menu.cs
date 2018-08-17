using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    public Button btnStart;
    public Text txtScore;
    public Text txtHighScore;

    // Use this for initialization
    void Start () {
        btnStart.onClick.AddListener(StartGame);
        if (Player.score > Player.Highscore) Player.Highscore = Player.score;

        if (txtScore!=null)
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
}
