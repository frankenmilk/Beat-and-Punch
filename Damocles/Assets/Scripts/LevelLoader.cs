using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public GameObject player;

    public float transitionTime = 1f;

    // Update is called once per frame
    void Update()
    {

        if(230 < player.transform.position.x && 5 < player.transform.position.y && 
            SceneManager.GetActiveScene().buildIndex == 0)
        {
            LoadNextLevel();
        }
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            StartCoroutine(Timer(100));
            LoadNextLevel();

        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex) 
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator Timer (int time)
    {
        yield return new WaitForSeconds(time);
    }
}
