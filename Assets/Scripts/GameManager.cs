using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject ball;

    public Text timer;
    public Text ballsLeft;
    public Text curLevel;

    public float time;

    [HideInInspector]
    public int level;

    public int ballModifier;
    public float timeModifier;

    [HideInInspector]
    public int bPL;

    int currentBalls;

    void Awake()
    {
        level = 1;
        bPL = level * ballModifier;
        time = bPL * timeModifier;
    }

    public void DrawBalls()
    {
        for (int i = 0; i < bPL; i++)
        {
            GameObject b = (GameObject)Instantiate(ball, new Vector3(Random.Range(-3.5f, 3.5f), Random.Range(-1.7f, 2.77f), 0.0f), Quaternion.identity);
            b.name = "ball " + i;
            b.GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
        }
        time = bPL * timeModifier;
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
    }

    void Update()
    {
        bPL = level * ballModifier;

        Mathf.Clamp(time, 0, 1000000);

        float seconds = time % 60;
        if (time >= 0)
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
                    Destroy(hit.collider.gameObject);
                    currentBalls--;
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
                        Destroy(hit.collider.gameObject);
                        currentBalls--;
                    }
                }
            }
        }


            if (currentBalls == 0)
            {
                NewLevel();
            }

        if(currentBalls > 0 && time == 0)
        {
            Application.LoadLevel("Loser");
        }
        ballsLeft.text = "Balls Left: " + currentBalls;
    }
}