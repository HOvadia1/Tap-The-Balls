using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject ball;
    public Text timer;
    public float time;

    [HideInInspector]
    public int level;

    public int ballModifier;

    [HideInInspector]
    public int bPL;

    void Awake()
    {
        level = 1;
        bPL = level * ballModifier;
        time = bPL * 2f;
    }

    void Start()
    {
        for(int i = 0; i < bPL; i++)
        {
            GameObject b = (GameObject) Instantiate(ball, new Vector3(Random.Range(-3.7f, 3.7f), 0.1f, Random.Range(-3, 3)), Quaternion.Euler(new Vector3(90, 0, 0)));
            b.name = "ball " + i;
            b.GetComponent<SpriteRenderer>().color = new Color(Random.value, Random.value, Random.value);
        }
    }

    void Update()
    {
        time -= Time.deltaTime;

        float seconds = time % 60;
        if (time > 0)
            timer.text = string.Format("{0:00}", seconds);

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if(hit != null && hit.collider.tag == "ball")
            {
                
            }
        }
    }
    
}
