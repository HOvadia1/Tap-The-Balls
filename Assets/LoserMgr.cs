using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoserMgr : MonoBehaviour {

    public Button playAgain;
    public Button mainMenu;

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
    }

}
