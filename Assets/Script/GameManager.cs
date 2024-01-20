using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BottleOrderBy{
    HEIGHT,
    RADIUS,
    LID,
    LIQUID,
    TAG
}

public enum Order
{
    Asc,
    Desc
}

public class GameManager : MonoBehaviour
{   
    public static GameManager instance;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;    
        }
    }

    public GameScene gameScene;
    public BottleManager bottleManager;
    private Explodable _explodable;
    public SceneChanger sceneChanger;
    #region Game Status


    public float currentIndex;

    private const float MAX_TIME = 60f;

    private float countDown = 5f;
    private float timeLeft = 0f;
    private int score = 0;
    private bool isStarted = false; 
    private bool isPaused = false;
    private bool isGameOver = false;
    private bool isGameWon = false;

    #endregion

    #region Game Mode
    [SerializeField]
    private BottleOrderBy bottleOrderBy;
    [SerializeField]
    private Order orderMode;

    private int numberOfBottle = 4;

    #endregion

    private void Start()
    {
        timeLeft = MAX_TIME;
        gameScene.SetTimeBarDefault(MAX_TIME);
        Time.timeScale = 0f;
        SetGameDefault();
    }

    private void Update()
    {
        if(bottleManager.IsAllBottleBreaked())
        {
            RandomGameMode();
            GenerateFirstIndex();
            bottleManager.DestroyAllBottle();
            bottleManager.InitBottle(bottleOrderBy, numberOfBottle); 
            IncreaseScore();
            IncreaseTime();
        }

        CountDown();
        CheckStartGame();
        SetGameTime();
    }

    private void FixedUpdate()
    {
        TimeManager();
    }

    public void CheckIndex(int index, GameObject bottle)
    {
        if(orderMode == Order.Asc)
        {
            CheckAsc(index, bottle, bottle.transform.parent);
        }
        else
        {
            CheckDesc(index, bottle, bottle.transform.parent);
        }
    }

    private void SetGameDefault()
    {
        RandomGameMode();
        GenerateFirstIndex();
        bottleManager.DestroyAllBottle();
        bottleManager.InitBottle(bottleOrderBy, numberOfBottle);

        ResetScore();
    }

    private void IncreaseScore() 
    { 
        score += 10;
        ScoreManager.instance.lastScore = score;
        gameScene.SetScoreText(score);
    }

    private void ResetScore()
    {
        score = 0;
        ScoreManager.instance.lastScore = 0;
        gameScene.SetScoreText(score);
    } 

    private void GenerateFirstIndex()
    {
        if (orderMode == Order.Asc)
        {
            currentIndex = -1;
        }
        else
        {
            switch(bottleOrderBy)
            {
                case BottleOrderBy.HEIGHT:
                    currentIndex = Enum.GetValues(typeof(BottleHeight)).Length;
                    break;
                case BottleOrderBy.RADIUS:
                    currentIndex = Enum.GetValues(typeof(BottleRadius)).Length;
                    break;
                case BottleOrderBy.LID:
                    currentIndex = Enum.GetValues(typeof(BottleLid)).Length;
                    break;
                case BottleOrderBy.LIQUID:
                    currentIndex = Enum.GetValues(typeof(LiquidAmount)).Length;
                    break;
                case BottleOrderBy.TAG:
                    currentIndex = Enum.GetValues(typeof(BottleTag)).Length;
                    break;
            }
        }
    }

    private void CheckDesc(int index, GameObject bottle, Transform container)
    {
        if (index == currentIndex || index == currentIndex - 1)
        {
            currentIndex = index;
            bottle.GetComponent<Bottle>().Correct();

            _explodable = bottle.GetComponent<Explodable>();
            _explodable.explode();
            ExplosionForce ef = GameObject.FindObjectOfType<ExplosionForce>();
            ef.doExplosion(bottle.transform.position);
        }
        else
        {
            bottle.GetComponent<Bottle>().Wrong();
            DecreaseTime();
        }
    }

    private void CheckAsc(int index, GameObject bottle, Transform container)
    {
        if (index == currentIndex || index == currentIndex + 1)
        {
            currentIndex = index;
            bottle.GetComponent<Bottle>().Correct();

            _explodable = bottle.GetComponent<Explodable>();
            _explodable.explode();
            ExplosionForce ef = GameObject.FindObjectOfType<ExplosionForce>();
            ef.doExplosion(bottle.transform.position);
        }
        else
        {
            bottle.GetComponent<Bottle>().Wrong();
            DecreaseTime();
        }
    }

    private void IncreaseTime()
    {
        timeLeft += 5f;
        if(timeLeft > MAX_TIME)
        {
            timeLeft = MAX_TIME;
        }
    }

    private void DecreaseTime()
    {
        if(timeLeft > 0)
        {
            timeLeft -= 5f;
        }
        else
        {
            timeLeft = 0;
        }
    }

    private void RandomGameMode()
    {
        Array values = Enum.GetValues(typeof(BottleOrderBy));
        System.Random random = new System.Random();
        bottleOrderBy = (BottleOrderBy)values.GetValue(random.Next(values.Length));
        
        values = Enum.GetValues(typeof(Order));
        orderMode = (Order)values.GetValue(random.Next(values.Length));

        gameScene.SetModeUI(bottleOrderBy, orderMode);
    }

    public BottleOrderBy GetGameMode()
    {
        return bottleOrderBy;
    }

    public Order GetOrderMode()
    {
        return orderMode;
    }

    private void GameOver()
    {
        isGameOver = true;
        if(ScoreManager.instance.scoreData[0].highScore < score)
        {
            ScoreManager.instance.scoreData[0].highScore = score;
        }
        gameScene.ShowTimeOutPanel();
        sceneChanger.ChangeToMenu();
    }

    private void SetGameTime()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else
        {
            timeLeft = 0;
        }
    }

    private void TimeManager()
    {
        if (timeLeft > 0)
        {
            gameScene.SetTimeBarValue(timeLeft);
        }
        else
        {
            if (!isGameOver)
            {
                gameScene.SetTimeBarValue(0);
                GameOver();
            }
        }
    }
    
    private void CountDown()
    {

        if (!isStarted && countDown > 0)
        {
            countDown -= Time.unscaledDeltaTime;
            gameScene.StartGameCountdown(countDown);
        }
    }

    private void CheckStartGame()
    {
        if (!isStarted)
        {
            if (countDown <= 0)
            {
                Time.timeScale = 1f;
                isStarted = true;
                gameScene.HideStartGameCountdown();
            }
        }
    }

    public bool IsGameStart()
    {
        return isStarted;
    }


}
