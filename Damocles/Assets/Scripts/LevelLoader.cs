using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public GameObject player;

    public GameObject set1;
    public GameObject set2;

    public VideoPlayer Vp;

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
            StartCoroutine(Cutscene1Timer(12));
            StartCoroutine(ObjectDelTimer(7.5f));
        }
        if(SceneManager.GetActiveScene().buildIndex == 2 && 478 < player.transform.position.x
            && 43.78 < player.transform.position.y)
        {
            LoadNextLevel();
        }
        if(SceneManager.GetActiveScene().buildIndex == 3 && 338 < player.transform.position.x)
        {
            LoadNextLevel();
        }
        if(SceneManager.GetActiveScene().buildIndex == 4 && 423 < player.transform.position.x)
        {
            LoadNextLevel();
        }
        if(SceneManager.GetActiveScene().buildIndex == 5 && 132 < player.transform.position.y &&
            220 < player.transform.position.x)
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

    IEnumerator Cutscene1Timer (int time)
    {
        yield return new WaitForSeconds(time);
        LoadNextLevel();
    }
    
    IEnumerator ObjectDelTimer (float delTime)
    {
        Vp.Prepare();
        yield return new WaitForSeconds(delTime);
        Destroy(set1);
        Destroy(set2);
        yield return new WaitForSeconds(0.5f);
        Vp.Play();
    }
    

}
