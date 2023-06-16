using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CameraController : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset; 
        /*if (SceneManager.GetActiveScene().buildIndex == 5 && 
        38.5 < player.transform.position.x && 37.1 < player.transform.position.y)
        {
            offset.y = 3.5;
        }*/
    }
}
