using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float delay = 5f;
    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGameScene()
    {
        if(FindObjectOfType<MusicPlayer>() != null)
            FindObjectOfType<MusicPlayer>().Destroy();
        SceneManager.LoadScene("Level");
    }

    public void LoadGameOverScene()
    {
        StartCoroutine(ShortDelay());
       
    }

    IEnumerator ShortDelay()
    {
        yield return new WaitForSeconds(delay); //wait 3 seconds before loading game over screen
        FindObjectOfType<MusicPlayer>().Destroy();
        SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
