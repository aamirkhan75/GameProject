using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour {

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Debug.Log("Exiting Application...");
        Application.Quit();
    }


}