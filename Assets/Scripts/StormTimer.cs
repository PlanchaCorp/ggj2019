using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StormTimer : MonoBehaviour
{
    [SerializeField]
    private float initialStormTimer = 10;
    private float INITIALGAMEOVERWAITTIMER = 1.5f;
    private float INITIALGAMEOVERAPPARITIONTIMER = 5;
    private float INITIALGAMEOVERRESTARTWAITTIMER = 2;
    private float INITIALGAMEOVERRESTARTAPPARITIONTIMER = 7;

    private SpriteRenderer stormSprite;
    private TextMeshProUGUI gameOverTextMesh;
    private TextMeshProUGUI gameOverRestartTextMesh;
    private float currentStormTimer;
    private float currentGameOverWaitTimer;
    private float currentGameOverApparitionTimer;
    private float currentGamerOverRestartWaitTimer;
    private float currentGamerOverRestartApparitionTimer;
    private bool runningOutOfTime;
    private bool gameOverIsFadingIn;
    private bool gameOverIsDisplayed;
    private bool gameOverRestartIsFadingIn;
    private bool gameOverRestartIsDisplayed;


    void Start()
    {
        runningOutOfTime = false;
        gameOverIsFadingIn = false;
        gameOverIsDisplayed = false;
        gameOverRestartIsFadingIn = false;
        gameOverRestartIsDisplayed = false;
        currentStormTimer = initialStormTimer;
        stormSprite = gameObject.GetComponent<SpriteRenderer>();
        gameOverTextMesh = GameObject.FindGameObjectWithTag("GameOverText").GetComponent<TextMeshProUGUI>();
        gameOverRestartTextMesh = GameObject.FindGameObjectWithTag("GameOverRestartText").GetComponent<TextMeshProUGUI>();
        if (stormSprite == null)
        {
            Debug.LogError("Storm sprite not attached to Storm object !");
        }
        stormSprite.color = new Color(1f, 1f, 1f, 0f);
    }
    
    void Update()
    {
        if (currentStormTimer > 0)
        {
            currentStormTimer -= Time.deltaTime;
            if (currentStormTimer < initialStormTimer / 2)
            {
                stormSprite.color = new Color(1f, 1f, 1f, 1 - currentStormTimer / (initialStormTimer / 2));
            }
        } else if (!runningOutOfTime)
        {
            runningOutOfTime = true;
            TriggerGameOver();
        }
        if (runningOutOfTime && !gameOverIsDisplayed)
        {
            if (gameOverIsFadingIn)
            {
                currentGameOverApparitionTimer -= Time.deltaTime;
                Color gameOverTextColor = gameOverTextMesh.color;
                gameOverTextMesh.color = new Color(gameOverTextColor.r, gameOverTextColor.g, gameOverTextColor.b, 1 - currentGameOverApparitionTimer / INITIALGAMEOVERAPPARITIONTIMER);
                if (currentGameOverApparitionTimer < 0)
                {
                    gameOverIsDisplayed = true;
                }
            } else
            {
                currentGameOverWaitTimer -= Time.deltaTime;
                if (currentGameOverWaitTimer < 0)
                {
                    gameOverIsFadingIn = true;
                }
            }
        }
        if (gameOverIsDisplayed && !gameOverRestartIsDisplayed)
        {
            if (gameOverRestartIsFadingIn)
            {
                currentGamerOverRestartApparitionTimer -= Time.deltaTime;
                Color gameOverTextColor = gameOverTextMesh.color;
                gameOverRestartTextMesh.color = new Color(gameOverTextColor.r, gameOverTextColor.g, gameOverTextColor.b, 1 - currentGamerOverRestartApparitionTimer / INITIALGAMEOVERRESTARTAPPARITIONTIMER);
                if (currentGamerOverRestartApparitionTimer < 0)
                {
                    gameOverRestartIsDisplayed = true;
                }
            } else
            {
                currentGamerOverRestartWaitTimer -= Time.deltaTime;
                if (currentGamerOverRestartWaitTimer < 0)
                {
                    gameOverRestartIsFadingIn = true;
                }
            }
        }
    }

    private void TriggerGameOver()
    {
        currentGameOverApparitionTimer = INITIALGAMEOVERAPPARITIONTIMER;
        currentGameOverWaitTimer = INITIALGAMEOVERWAITTIMER;
        currentGamerOverRestartApparitionTimer = INITIALGAMEOVERRESTARTAPPARITIONTIMER;
        currentGamerOverRestartWaitTimer = INITIALGAMEOVERRESTARTWAITTIMER;
    }

    public bool HasRunnedOutOfTime()
    {
        return runningOutOfTime;
    }
}
