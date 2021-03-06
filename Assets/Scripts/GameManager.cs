﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public AudioClip audio;
    public GameObject ball;

    public Text timer;
    public Text ballsLeft;
    public Text curLevel;
    public Text pText;
    public float time;

    [HideInInspector]
    public int level;

    public int ballModifier;
    public float timeModifier;

    [HideInInspector]
    public int bPL;

    int currentBalls;
    public int points;

    void Awake()
    {
        level = 1;
        bPL = level * ballModifier;
        time = bPL * level;
    }

    public void DrawBalls()
    {
        for (int i = 0; i < bPL; i++)
        {
            //GameObject b = (GameObject)Instantiate(ball, new Vector3(Random.Range(-3.5f, 3.5f), Random.Range(-1.7f, 2.77f), 0.0f), Quaternion.identity);
            GameObject b = (GameObject)Instantiate(ball, new Vector3(0, 0, 0), Quaternion.identity);

            b.name = "ball " + i;
            b.GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
            b.GetComponent<Rigidbody2D>().velocity = Random.insideUnitCircle * 5;
        }

        time = bPL * level;
        curLevel.text = "Level: " + level;
        currentBalls = bPL;
    }

    public void NewLevel()
    {
        level++;
        bPL = level * ballModifier;
        DrawBalls();
    }

    void Start()
    {
        DrawBalls();
        if(PlayerPrefs.GetInt("highscore") == null)
        {
            PlayerPrefs.SetInt("highscore", 0);
        }
    }

    void Update()
    {
        int seconds = (int)(time);
        if (time > 0)
        {
            time -= Time.deltaTime;
            timer.text = string.Format("{0:00}", seconds);
        }
        else
        {
            time = 0;
        }


        //Mouse Logic
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if(hit.collider)
            {
                if(hit.collider.tag == "ball")
                {
                    if(gameObject.GetComponent<AudioSource>() != null)
                    {
                        gameObject.GetComponent<AudioSource>().clip = audio;
                        gameObject.GetComponent<AudioSource>().Play();
                    }
                    Destroy(hit.collider.gameObject);
                    currentBalls--;
                    points += 1;
                }
            }
        }

        //Touch phone logic
        for (int i = 0; i < Input.touchCount; ++i)
        {
            if(Input.GetTouch(i).phase == TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position), Vector2.zero);
                if(hit.collider)
                {
                    if(hit.collider.tag == "ball")
                    {
                        if (gameObject.GetComponent<AudioSource>() != null)
                        {
                            gameObject.GetComponent<AudioSource>().clip = audio;
                            gameObject.GetComponent<AudioSource>().Play();
                        }

                        Destroy(hit.collider.gameObject);
                        currentBalls--;
                        points += 1;
                    }
                }
            }
        }


            if (currentBalls == 0)
            {
                NewLevel();
            }

        if(currentBalls > 0 && time <= 0)
        {
            PlayerPrefs.SetInt("recentscore", points);
            Debug.Log(PlayerPrefs.GetInt("recentscore"));

            if(PlayerPrefs.GetInt("recentscore") > PlayerPrefs.GetInt("highscore"))
            {
                PlayerPrefs.SetInt("highscore", points);
                Debug.Log("beat high score!");
            }

            Application.LoadLevel("Loser");
        }

        ballsLeft.text = "Circles Left: " + currentBalls;
        pText.text = "Points: " + points;
    }
}