using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoserMgr : MonoBehaviour {

    public Button playAgain;
    public Button mainMenu;

    public Text scoreMgr;

    void Start()
    {
        playAgain.onClick.AddListener(() =>
        {
            Application.LoadLevel("tap");
        });

        mainMenu.onClick.AddListener(() =>
        {
            Application.LoadLevel("menu");
        });

        scoreMgr.text = "Recent Score: " + PlayerPrefs.GetInt("recentscore") + "\n" + "High Score: " + PlayerPrefs.GetInt("highscore");
    }

}
