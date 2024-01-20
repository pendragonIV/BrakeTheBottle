using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{ 
    [SerializeField]
    private Slider timeBar;

    [SerializeField]
    private Transform overlayPanel;
    [SerializeField]
    private Text startGameCountdown;
    [SerializeField]
    private Transform timeOutPanel;
    [SerializeField] 
    private Transform startGamePanel;
    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text modeText;
    [SerializeField]
    private Text orderText;

    public void SetModeUI(BottleOrderBy bottleOrderBy, Order orderMode)
    {
        modeText.text = "BY " + bottleOrderBy.ToString();
        switch (orderMode)
        {
            case Order.Asc:
                {
                    if(bottleOrderBy == BottleOrderBy.HEIGHT)
                    {
                        orderText.text = "SHORT TO TALL";
                    }
                    else if(bottleOrderBy == BottleOrderBy.RADIUS)
                    {
                        orderText.text = "SMALL TO LARGE";
                    }
                    else if(bottleOrderBy == BottleOrderBy.LID)
                    {
                        orderText.text = "LIGHT TO DARK";
                    }
                    else if(bottleOrderBy == BottleOrderBy.LIQUID)
                    {
                        orderText.text = "EMPTY TO FULL";
                    }
                    else if(bottleOrderBy == BottleOrderBy.TAG)
                    {
                        orderText.text = "LIGHT TO DARK";
                    }
                    break;
                }
            case Order.Desc:
                {
                    if(bottleOrderBy == BottleOrderBy.HEIGHT)
                    {
                        orderText.text = "TALL TO SHORT";
                    }
                    else if(bottleOrderBy == BottleOrderBy.RADIUS)
                    {
                        orderText.text = "LARGE TO SMALL";
                    }
                    else if(bottleOrderBy == BottleOrderBy.LID)
                    {
                        orderText.text = "DARK TO LIGHT";
                    }
                    else if(bottleOrderBy == BottleOrderBy.LIQUID)
                    {
                        orderText.text = "FULL TO EMPTY";
                    }
                    else if(bottleOrderBy == BottleOrderBy.TAG)
                    {
                        orderText.text = "DARK TO LIGHT";
                    }
                    break;
                }
        }
    }

    public void SetTimeBarDefault(float maxTime)
    {
        timeBar.maxValue = maxTime;
    }

    public void SetTimeBarValue(float timeLeft)
    {
        timeBar.value = timeLeft;
    }

    public void ShowTimeOutPanel()
    {
        ShowOverlayPanel();
        timeOutPanel.gameObject.SetActive(true);
    }

    public void StartGameCountdown(float timeLeft)
    {
        if (!startGameCountdown.gameObject.activeSelf)
        {
            ShowOverlayPanel();
            startGameCountdown.gameObject.SetActive(true);
        }
        startGameCountdown.text = ((int)Mathf.Round(timeLeft)).ToString();
    }

    public void SetScoreText(int score)
    {
        scoreText.text = score.ToString();
    }

    public void HideStartGamePanel()
    {
        if (startGamePanel.gameObject.activeSelf)
        {
            startGamePanel.gameObject.SetActive(false);
            HideOverlayPanel();
        }
    }

    public void HideStartGameCountdown()
    {
        if (startGameCountdown.gameObject.activeSelf)
        {
            startGameCountdown.gameObject.SetActive(false);
            HideOverlayPanel();
        }
    }

    public void ShowStartGamePanel()
    {
        if (!startGamePanel.gameObject.activeSelf)
        {
            ShowOverlayPanel();
            startGamePanel.gameObject.SetActive(true);
        }
    }

    private void ShowOverlayPanel()
    {
        overlayPanel.gameObject.SetActive(true);
    }

    private void HideOverlayPanel()
    {
        overlayPanel.gameObject.SetActive(false);
    }
}
