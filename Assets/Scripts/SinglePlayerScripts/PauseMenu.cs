using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    GameObject winMenu;

    [SerializeField]
    GameObject loseMenu;

    bool paused = false;

	// Update is called once per frame
	void Update () {

        if(GvrController.AppButtonUp && winMenu.active == false && loseMenu.active == false && pauseMenu.active == false)
        {
            togglePause();
        }
	
	}

    public void togglePause()
    {
        if (paused == true)
        {
            Debug.Log("Timescale set to 1f");
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
            paused = false;
        }
        else
        {
            Debug.Log("Timescale set to 0f");
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
            paused = true;
        }
    }

    public void returnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void win()
    {
        winMenu.SetActive(true);
    }

    public void lose()
    {
        loseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
}
