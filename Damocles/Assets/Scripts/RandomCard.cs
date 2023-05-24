using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RandomCard : MonoBehaviour
{

    public Transform[] spawnPoints;
    List<int> cardNumbers = new List<int> {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24};
    public GameObject[] cardObjects;


    // Start is called before the first frame update
    void Start()
    {

        int spawnPointNum = 0;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            int randCard = Random.Range(1, cardNumbers.Count);

            cardObjects[randCard].transform.position = new Vector2(spawnPoints[spawnPointNum].position.x, spawnPoints[spawnPointNum].position.y);
            cardNumbers.RemoveAt(randCard);

            spawnPointNum++;
        }
    }

}
