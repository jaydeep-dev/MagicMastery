using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseHandler : MonoBehaviour
{
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject gameInfoUI;
    [SerializeField] private GameObject winUi;

    private bool isPaused;
    private bool isGameStarted;

    private void Start()
    {
        Time.timeScale = 0;
    }

    private void OnEnable()
    {
        EnemyController.OnBossKilled += OnBossKilled;
    }

    private void OnBossKilled()
    {
        SoundManager.Instance.PlayGameWinSFX();
        winUi.SetActive(true);
        Time.timeScale = 0;
    }

    private void OnDisable()
    {
        EnemyController.OnBossKilled -= OnBossKilled;
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
        Time.timeScale = 1;
        var buildIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        UnityEngine.SceneManagement.SceneManager.LoadScene(buildIndex);
    }

    public void OnMainMenuClicked()
    {
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
