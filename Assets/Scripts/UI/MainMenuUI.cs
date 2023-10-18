using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button quitButton;

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            Loader.LoadScene(Loader.Scene.MainScene);
        });

        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
