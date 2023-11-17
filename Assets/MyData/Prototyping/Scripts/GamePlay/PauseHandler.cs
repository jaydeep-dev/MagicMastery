using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseHandler : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject gameInfoUI;

    private bool isPaused;
    private bool isGameStarted;

    private void Start()
    {
        Time.timeScale = 0;
    }

    public void OnPause(InputAction.CallbackContext ctx)
    {
        if (SkillUIController.IsShowingUI)
            return;

        if (ctx.action.triggered)
            isPaused = !isPaused;

        if (isPaused )
        {
            Time.timeScale = 0;
            pauseUI.SetActive(true);
            pauseUI.GetComponentInChildren<Button>().Select();
        }
        else
        {
            Time.timeScale = 1;
            pauseUI.SetActive(false);
        }
    }

    public void OnSubmit(InputAction.CallbackContext ctx)
    {
        if(ctx.action.triggered && !isGameStarted)
        {
            isGameStarted = true;
            gameInfoUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void OnResumeClicked()
    {
        Time.timeScale = 1;
        pauseUI.SetActive(false);
    }

    public void OnRestartClicked()
    {
        var buildIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        UnityEngine.SceneManagement.SceneManager.LoadScene(buildIndex);
    }

    public void OnMainMenuClicked()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
