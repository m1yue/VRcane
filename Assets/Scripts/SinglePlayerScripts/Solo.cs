using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Solo : MonoBehaviour {

	public void enterSinglePlayerMode()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
        Time.timeScale = 1f;
    }

}
