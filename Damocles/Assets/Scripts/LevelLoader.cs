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
        }
        if(SceneManager.GetActiveScene().buildIndex == 2 && 478 < player.transform.position.x
            && 43.78 < player.transform.position.y)
        {
            LoadNextLevel();
        }
        if(SceneManager.GetActiveScene().buildIndex == 3 && /*add boss trigger*/)
        {
            LoadNextLevel();
        }
        if(SceneManager.GetActiveScene().buildIndex == 4 && /*another boss trigger*/)
        {
            LoadNextLevel();
        }
        if(SceneManager.GetActiveScene().buildIndex == 5 && 423 < player.transform.position.x)
        {
            LoadNextLevel();
        }
        if(SceneManager.GetActiveScene().buildIndex == 6 && /*boss trigger*/)
        {
            LoadNextLevel();
        }
        if(SceneManager.GetActiveScene().buildIndex == 7 && /*find player location*/)
        {
            LoadNextLevel();
        }
        if(SceneManager.GetActiveScene().buildIndex == 8 && /*find player location*/)
        {
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
        LoadNextLevel();
    }
}
