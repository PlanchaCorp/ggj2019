using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StormTimer : MonoBehaviour
{
    [SerializeField]
    private float initialStormTimer = 10;
    private float INITIALGAMEOVERWAITTIMER = 3;
    private float INITIALGAMEOVERAPPARITIONTIMER = 5;

    private SpriteRenderer stormSprite;
    private TextMeshProUGUI gameOverTextMesh;
    private float currentStormTimer;
    private float currentGameOverWaitTimer;
    private float currentGameOverApparitionTimer;
    private bool runningOutOfTime;
    private bool gameOverIsFadingIn;
    
    void Start()
    {
        runningOutOfTime = false;
        gameOverIsFadingIn = false;
        currentStormTimer = initialStormTimer;
        stormSprite = gameObject.GetComponent<SpriteRenderer>();
        gameOverTextMesh = transform.Find("GameOverCanvas").GetComponentInChildren<TextMeshProUGUI>();
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
        if (runningOutOfTime)
        {
            if (gameOverIsFadingIn)
            {
                currentGameOverApparitionTimer -= Time.deltaTime;
                Color gameOverTextColor = gameOverTextMesh.color;
                gameOverTextMesh.color = new Color(gameOverTextColor.r, gameOverTextColor.g, gameOverTextColor.b, 1 - currentGameOverApparitionTimer / INITIALGAMEOVERAPPARITIONTIMER);
            } else
            {
                currentGameOverWaitTimer -= Time.deltaTime;
                if (currentGameOverWaitTimer < 0)
                {
                    gameOverIsFadingIn = true;
                }
            }
        }
    }

    private void TriggerGameOver()
    {
        currentGameOverApparitionTimer = INITIALGAMEOVERAPPARITIONTIMER;
        currentGameOverWaitTimer = INITIALGAMEOVERWAITTIMER;
    }

    public bool HasRunnedOutOfTime()
    {
        return runningOutOfTime;
    }
}
