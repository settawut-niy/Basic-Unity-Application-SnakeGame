using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    [Header("Food")]
    [SerializeField] GameObject foodPrefab;

    [Header("Border")]
    [SerializeField] Transform borderTop;
    [SerializeField] Transform borderBottom;
    [SerializeField] Transform borderLeft;
    [SerializeField] Transform borderRight;

    void Start()
    {
        InvokeRepeating("Spawn", 3,4);
    }

    void Spawn()
    {
        int x = (int)Random.Range(borderLeft.position.x + 2f, borderRight.position.x - 2f);
        int y = (int)Random.Range(borderTop.position.y - 2f, borderBottom.position.y + 2f);

        if (x % 2 == 0)
        {
            x++;
        }

        if (y % 2 == 0)
        {
            y++;
        }

        var spawnedFood = Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity);
        spawnedFood.transform.parent = transform;
    }

}
