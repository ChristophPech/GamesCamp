using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
    public Button btnStart;
    public Text txtScore;

    // Use this for initialization
    void Start () {
        btnStart.onClick.AddListener(StartGame);

        if(txtScore!=null)
        {
            txtScore.text = "Score: " + Player.score;
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
