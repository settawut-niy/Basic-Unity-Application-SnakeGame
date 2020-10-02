using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Snake : MonoBehaviour
{
    // Current snake direction
    Vector2 currentDir = Vector2.right;

    // About tail
    List<Transform> tail = new List<Transform>();
    [SerializeField] GameObject tailPrefab;

    // Eat behavior
    bool ate = false;

    // Movement
    [Tooltip("Least is very fast")]
    [SerializeField] [Range(0, 1)] float speed = 0.3f;


    void Start()
    {
        InvokeRepeating("Move", speed, speed);
    }

    void Update()
    {
        InputManager();
    }

    void InputManager()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (currentDir != Vector2.down)
            {
                currentDir = Vector2.up;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            if (currentDir != Vector2.up)
            {
                currentDir = Vector2.down;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (currentDir != Vector2.left)
            {
                currentDir = Vector2.right;
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (currentDir != Vector2.right)
            {
                currentDir = Vector2.left;
            }
        }

        if(GameManager.instance.currentGameState == GameState.Stop)
        {
            if (Input.anyKeyDown)
            {
                GameManager.instance.StartPlay();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.StartsWith("Food"))
        {
            ate = true;

            Destroy(collision.gameObject);
        }
        else
        {
            GameManager.instance.GameOver();
        }

    }

    void Move()
    {
        Vector2 currentPosition = transform.position;

        transform.Translate(currentDir);

        if (ate)
        {
            ScoreBoard.instance.AddScore(1);

            GameObject body = (GameObject)Instantiate(tailPrefab, currentPosition, Quaternion.identity);

            tail.Insert(0,body.transform);

            ate = false;
        }
        else if (tail.Count > 0)
        {
            tail.Last().position = currentPosition;

            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count-1);
        }
    }
}
