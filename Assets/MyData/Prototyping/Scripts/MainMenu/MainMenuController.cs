using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject quitMenu;

    public void LoadGameScene()
    {
        LeanTween.delayedCall(.7f, () => SceneManager.LoadScene("PrototypeGame"));
    }

    public void ActivateGodMode()
    {
        GameManager.IsGodMode = true;
        LoadGameScene();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quitMenu.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetMusicToggle(bool isOn) => GameManager.EnabledMusic = isOn;

    public void SetSFXToggle(bool isOn) => GameManager.EnabledSFX = isOn;
}
