using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{

    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(spawnPoints.Length);

        int spawnPointNum = 0;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int randEnemy = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randEnemy], spawnPoints[spawnPointNum].position, transform.rotation);

            spawnPointNum++;
        }
        
        

    }

}
