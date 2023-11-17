using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioClip buttonClickClip;

    [SerializeField] private AudioClip gameWinClip;
    [SerializeField] private AudioClip gameOverClip;

    [SerializeField] private List<AudioClip> musicList;

    private AudioSource audioSource;

    private void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();

        foreach (var source in FindObjectsOfType<AudioSource>(true))
        {
            source.mute = !GameManager.EnabledSFX;
        }

        audioSource.mute = !GameManager.EnabledMusic;
    }

    private void Start()
    {
        var selectedBgMusic = musicList[Random.Range(0, musicList.Count)];
        audioSource.clip = selectedBgMusic;
        audioSource.loop = true;
        audioSource.Play();

    }

    public void PlayButtonClickSFX()
    {
        audioSource.PlayOneShot(buttonClickClip);
    }

    public void PlayGameWinSFX() => audioSource.PlayOneShot(gameWinClip, 0.1f);

    public void PlayGameOverSFX() => audioSource.PlayOneShot(gameOverClip, 0.1f);
}
