using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCard : MonoBehaviour
{

    public Transform[] spawnPoints;
    List<int> cardNumbers = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25};
    public GameObject[] cardPrefabs;


    // Start is called before the first frame update
    void Start()
    {

        int spawnPointNum = 0;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int randCard = Random.Range(0, cardNumbers.Count);
            cardNumbers.RemoveAt(randCard);
            Instantiate(cardPrefabs[randCard], spawnPoints[spawnPointNum].position, transform.rotation);

            spawnPointNum++;
        }
        
        

    }

}
