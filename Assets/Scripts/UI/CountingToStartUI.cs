using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountingToStartUI : MonoBehaviour
{
    private const string POP_UP_NUMBER = "PopUpNumber";

    [SerializeField] TextMeshProUGUI countdownText;
    private Animator animator;
    private int countdownNumber;
    private int previousNumber;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        Hide();
    }

    private void Update()
    {
        countdownNumber = Mathf.CeilToInt(GameManager.Instance.GetCountingToStartTimer());
        countdownText.text = countdownNumber.ToString();

        if (previousNumber != countdownNumber)
        {
            previousNumber = countdownNumber;
            animator.SetTrigger(POP_UP_NUMBER);
            SoundManager.Instance.PlayCountdownSound();
        }
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
