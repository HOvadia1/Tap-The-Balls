using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuMgr : MonoBehaviour {

    public Button play;
    public Button how;

    void Start()
    {
        play.onClick.AddListener(() =>
        {
            Application.LoadLevel("tap");
        });

        how.onClick.AddListener(() =>
        {
            Application.LoadLevel("how-to");
        });
    }

}
