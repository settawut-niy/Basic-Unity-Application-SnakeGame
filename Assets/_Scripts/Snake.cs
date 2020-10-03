using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Snake : MonoBehaviour
{
    // Current snake direction
    Vector2 currentDir = Vector2.right;


    // Snake parts
    [SerializeField] Transform headTranform;
    [SerializeField] Transform tailTranform;
    [SerializeField] GameObject bodyPrefab;
    List<Transform> body = new List<Transform>();

    // Eat behavior
    bool ate = false;

    // Movement
    [Tooltip("Least is very fast")]
    [SerializeField] [Range(0, 1)] float speed = 0.3f;


    void Start()
    {
        InvokeRepeating("Move", speed, speed);

        headTranform.eulerAngles = new Vector3(0,0,-90);
        tailTranform.eulerAngles = new Vector3(0, 0, -90);
    }

    void Update()
    {
        InputManager();
    }

    void InputManager()
    {
        if (GameManager.instance.currentGameState != GameState.GameOver)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                if (currentDir != Vector2.down)
                {
                    currentDir = Vector2.up;
                    headTranform.eulerAngles = new Vector3(0, 0, 0);

                    AudioManager.instance.PlayInteractSound("pop");
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                if (currentDir != Vector2.up)
                {
                    currentDir = Vector2.down;
                    headTranform.eulerAngles = new Vector3(0, 0, 180);

                    AudioManager.instance.PlayInteractSound("pop");
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if (currentDir != Vector2.left)
                {
                    currentDir = Vector2.right;
                    headTranform.eulerAngles = new Vector3(0, 0, -90);

                    AudioManager.instance.PlayInteractSound("beep");
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                if (currentDir != Vector2.right)
                {
                    currentDir = Vector2.left;
                    headTranform.eulerAngles = new Vector3(0, 0, 90);

                    AudioManager.instance.PlayInteractSound("beep");
                }
            }
        }

        if(GameManager.instance.currentGameState == GameState.Stop)
        {
            if 
                (
                Input.GetKeyDown(KeyCode.UpArrow) || 
                Input.GetKeyDown(KeyCode.DownArrow) ||
                Input.GetKeyDown(KeyCode.LeftArrow) ||
                Input.GetKeyDown(KeyCode.RightArrow) ||
                Input.GetKeyDown(KeyCode.W) ||
                Input.GetKeyDown(KeyCode.S) ||
                Input.GetKeyDown(KeyCode.D) ||
                Input.GetKeyDown(KeyCode.A)
                )
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

            AudioManager.instance.PlayEatSound();

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
        Vector3 currentRotation = headTranform.eulerAngles;

        transform.Translate(currentDir*2);

        tailTranform.position = currentPosition;
        tailTranform.eulerAngles = currentRotation;

        if (ate)
        {
            ScoreBoard.instance.AddScore(1);

            GameObject body = (GameObject)Instantiate(bodyPrefab, currentPosition, headTranform.rotation);

            this.body.Insert(0, body.transform);

            ate = false;
        }
        else if (body.Count > 0)
        {
            TailFollowing(body);

            body.Last().position = currentPosition;
            body.Last().eulerAngles = currentRotation;

            body.Insert(0, body.Last());
            body.RemoveAt(body.Count-1);
        }
    }

    void TailFollowing (List<Transform> body)
    {
        {
            if (body.Last().eulerAngles.z == 0)
            {
                tailTranform.position = body.Last().position;
                tailTranform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (body.Last().eulerAngles.z == 180)
            {
                tailTranform.position = body.Last().position;
                tailTranform.eulerAngles = new Vector3(0, 0, 180);
            }
            else if (body.Last().eulerAngles.z == 90)
            {
                tailTranform.position = body.Last().position;
                tailTranform.eulerAngles = new Vector3(0, 0, 90);
            }
            else
            {
                tailTranform.position = body.Last().position;
                tailTranform.eulerAngles = new Vector3(0, 0, -90);
            }
        }
    }
}