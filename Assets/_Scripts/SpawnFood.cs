using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    // Food
    [SerializeField] GameObject foodPrefab;

    // Borders
    [SerializeField] Transform borderTop;
    [SerializeField] Transform borderBottom;
    [SerializeField] Transform borderLeft;
    [SerializeField] Transform borderRight;

    void Start()
    {
        InvokeRepeating("Spawn",3,4);
    }

    void Spawn()
    {
        int x = (int)Random.Range(borderLeft.position.x+1, borderRight.position.x-1);
        int y = (int)Random.Range(borderTop.position.y-1, borderBottom.position.y+1);

        Instantiate(foodPrefab, new Vector2(x, y), Quaternion.identity);
    }

}
