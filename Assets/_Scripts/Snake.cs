using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Snake : MonoBehaviour
{
    // Current snake direction
    Vector2 currentDir = Vector2.right;

    [Header("Snake Parts")]
    [SerializeField] Transform headTranform;
    [SerializeField] Transform tailTranform;
    [SerializeField] Transform snakePartsKeeper;
    [SerializeField] GameObject bodyPrefab;
    List<Transform> bodyList = new List<Transform>();

    // Eat behavior
    bool ate = false;

    [Header("Movement Control")][Tooltip("Least is very fast")]
    [SerializeField] [Range(0, 1)] float speed = 0.3f;


    void Start()
    {
        InvokeRepeating("SnakeBehavior", speed, speed);

        SnakeStartPosition();
    }

    void SnakeStartPosition()
    {
        headTranform.eulerAngles = new Vector3(0, 0, -90);
        tailTranform.eulerAngles = new Vector3(0, 0, -90);
    }

    void Update()
    {
        MovementControlInput();
    }

    void MovementControlInput()
    {
        // Movement Control
        if (GameManager.instance.currentGameState == GameState.Playing)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                if (currentDir != Vector2.down)
                {
                    currentDir = Vector2.up;
                    headTranform.eulerAngles = new Vector3(0, 0, 0);

                    AudioManager.instance.PlaySFX(AudioManager.SFXType.iPop);
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                if (currentDir != Vector2.up)
                {
                    currentDir = Vector2.down;
                    headTranform.eulerAngles = new Vector3(0, 0, 180);

                    AudioManager.instance.PlaySFX(AudioManager.SFXType.iPop);
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if (currentDir != Vector2.left)
                {
                    currentDir = Vector2.right;
                    headTranform.eulerAngles = new Vector3(0, 0, -90);

                    AudioManager.instance.PlaySFX(AudioManager.SFXType.iBeep);
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                if (currentDir != Vector2.right)
                {
                    currentDir = Vector2.left;
                    headTranform.eulerAngles = new Vector3(0, 0, 90);

                    AudioManager.instance.PlaySFX(AudioManager.SFXType.iBeep);
                }
            }
        }

        // Press to start play
        if(GameManager.instance.currentGameState == GameState.Stop && !UIManager.instance.IsGameMenuActive)
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

            AudioManager.instance.PlaySFX(AudioManager.SFXType.Eat);

            Destroy(collision.gameObject);
        }
        else
        {
            GameManager.instance.GameOver();
        }

    }

    void SnakeBehavior()
    {
        Vector2 currentPosition = transform.position;
        Vector3 currentRotation = headTranform.eulerAngles;

        Moving(currentPosition, currentRotation);

        if (ate)
        {
            GrowUp(currentPosition);
        }
        else if (bodyList.Count > 0)
        {
            TailFollowing();

            BodyFollowing(currentPosition, currentRotation);
        }
    }

    void Moving(Vector2 currentPosition, Vector3 currentRotation)
    {
        transform.Translate(currentDir * 2);

        tailTranform.position = currentPosition;
        tailTranform.eulerAngles = currentRotation;
    }

    void GrowUp(Vector2 currentPosition)
    {
        ScoreBoard.instance.AddScore(1);

        GameObject spawnedBody = Instantiate(bodyPrefab, currentPosition, headTranform.rotation);

        spawnedBody.transform.parent = snakePartsKeeper;

        bodyList.Insert(0, spawnedBody.transform);

        ate = false;
    }

    void TailFollowing ()
    {
        {
            if (bodyList.Last().eulerAngles.z == 0)
            {
                tailTranform.position = bodyList.Last().position;
                tailTranform.eulerAngles = new Vector3(0, 0, 0);
            }
            else if (bodyList.Last().eulerAngles.z == 180)
            {
                tailTranform.position = bodyList.Last().position;
                tailTranform.eulerAngles = new Vector3(0, 0, 180);
            }
            else if (bodyList.Last().eulerAngles.z == 90)
            {
                tailTranform.position = bodyList.Last().position;
                tailTranform.eulerAngles = new Vector3(0, 0, 90);
            }
            else
            {
                tailTranform.position = bodyList.Last().position;
                tailTranform.eulerAngles = new Vector3(0, 0, -90);
            }
        }
    }

    void BodyFollowing(Vector2 currentPosition, Vector3 currentRotation)
    {
        bodyList.Last().position = currentPosition;
        bodyList.Last().eulerAngles = currentRotation;

        bodyList.Insert(0, bodyList.Last());
        bodyList.RemoveAt(bodyList.Count - 1);
    }
}