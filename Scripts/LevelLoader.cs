using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    // Exits the application
    public static void QuitGame ()
    {
        Application.Quit();
    }


    // Loads the level according to the scene index number
    public static void LoadLevel (int sceneIndex)
    {
        //StartCoroutine(LoadAsynchronously(sceneIndex));
        SceneManager.LoadScene(sceneIndex);
    }


}
