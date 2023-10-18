using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountingToStartUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;

    private void Awake()
    {
        
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        Hide();
    }

    private void Update()
    {
        countdownText.text = Mathf.Ceil(GameManager.Instance.GetCountingToStartTimer()).ToString();
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.isCountingToStart())
        {
            Show();
        }

        else
        {
            Hide();
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
