using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Storm : MonoBehaviour
{
    [SerializeField]
    private float initialStormTimer = 10;
    private float INITIALGAMEOVERWAITTIMER = 1f;
    private float INITIALGAMEOVERAPPARITIONTIMER = 3;
    private float INITIALGAMEOVERRESTARTWAITTIMER = 2;
    private float INITIALGAMEOVERRESTARTAPPARITIONTIMER = 4;
    private float INITIALFALLINGTIMER = 3.0f;

    private GameObject player;
    private Image stormImage;
    private TextMeshProUGUI gameOverTextMesh;
    private TextMeshProUGUI gameOverRestartTextMesh;
    private float currentStormTimer;
    private float currentGameOverWaitTimer;
    private float currentGameOverApparitionTimer;
    private float currentGamerOverRestartWaitTimer;
    private float currentGamerOverRestartApparitionTimer;
    private bool stormHasStarted;
    private bool runningOutOfTime;
    private bool gameOverIsFadingIn;
    private bool gameOverIsDisplayed;
    private bool gameOverRestartIsFadingIn;
    private bool gameOverRestartIsDisplayed;

    private float fallingTimer;
    private bool deathByFall;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        deathByFall = false;
        stormHasStarted = false;
        currentStormTimer = initialStormTimer;
        stormImage = transform.Find("GameOverCanvas").GetComponentInChildren<Image>();
        gameOverTextMesh = GameObject.FindGameObjectWithTag("GameOverText").GetComponent<TextMeshProUGUI>();
        gameOverRestartTextMesh = GameObject.FindGameObjectWithTag("GameOverRestartText").GetComponent<TextMeshProUGUI>();
        if (stormImage == null)
        {
            Debug.LogError("Storm sprite not attached to Storm object !");
        }
    }

    void Update()
    {
        if (!stormHasStarted && deathByFall)
        {
            fallingTimer -= Time.deltaTime;
            float newScale = fallingTimer / INITIALFALLINGTIMER;
            player.transform.localScale = new Vector3(newScale, newScale, newScale);
            if (fallingTimer < 0)
            {
                stormHasStarted = true;
            }
        }
        if (currentStormTimer > 0 && stormHasStarted)
        {
            if (!deathByFall)
            {
                currentStormTimer -= Time.deltaTime;
            }
            else
            {
                currentStormTimer -= Time.deltaTime * (initialStormTimer / 4);
            }
            if (currentStormTimer < initialStormTimer / 2)
            {
                stormImage.color = new Color(deathByFall ? 0 : 1, deathByFall ? 0 : 1, deathByFall ? 0 : 1, 1 - currentStormTimer / (initialStormTimer / 2));
            }
        }
        else if (!runningOutOfTime && stormHasStarted)
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
            }
            else
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
            }
            else
            {
                currentGamerOverRestartWaitTimer -= Time.deltaTime;
                if (currentGamerOverRestartWaitTimer < 0)
                {
                    gameOverRestartIsFadingIn = true;
                }
            }
        }
        if (gameOverIsFadingIn && Input.GetButtonDown("Jump"))
        {
            // TODO : Replace the scene loading with the correct behaviour to restart the night
            deathByFall = false;
            player.transform.localScale = new Vector3(1, 1, 1);
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            if (DayNightManager.GetDay())
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
                
            }
            else
            {
                player.transform.position = GetComponent<PictureBehaviour>().GetPlayerNightInitialPosition();
                stormHasStarted = false;
                runningOutOfTime = false;
                currentStormTimer = initialStormTimer;
                gameOverIsFadingIn = false;
                gameOverIsDisplayed = false;
                gameOverRestartIsFadingIn = false;
                gameOverRestartIsDisplayed = false;
                stormImage.color = new Color(1f, 1f, 1f, 0f);
                Color gameOverTextColor = gameOverTextMesh.color;
                gameOverTextMesh.color = new Color(gameOverTextColor.r, gameOverTextColor.g, gameOverTextColor.b, 0);
                gameOverRestartTextMesh.color = new Color(gameOverTextColor.r, gameOverTextColor.g, gameOverTextColor.b, 0);
                DayNightManager.ChangeCycle();
                StartStorm();
            }
        }
    }

    public float GetRemainingTimeRatio()
    {
        return currentStormTimer / initialStormTimer;
    }
    private void TriggerGameOver()
    {
        currentGameOverApparitionTimer = INITIALGAMEOVERAPPARITIONTIMER;
        currentGameOverWaitTimer = INITIALGAMEOVERWAITTIMER;
        currentGamerOverRestartApparitionTimer = INITIALGAMEOVERRESTARTAPPARITIONTIMER;
        currentGamerOverRestartWaitTimer = INITIALGAMEOVERRESTARTWAITTIMER;
    }

    public void StartStorm()
    {
        GameObject.Find("PlayerAndCamera/UI/CompassBase").SetActive(false);
        stormHasStarted = true;
        runningOutOfTime = false;
        gameOverIsFadingIn = false;
        gameOverIsDisplayed = false;
        gameOverRestartIsFadingIn = false;
        gameOverRestartIsDisplayed = false;
        stormImage.color = new Color(1f, 1f, 1f, 0f);
    }

    public bool HasRunnedOutOfTime()
    {
        return runningOutOfTime;
    }

    public bool IsFalling()
    {
        return deathByFall;
    }

    public void TriggerFallGameOver()
    {
        if (!deathByFall)
        {
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            deathByFall = true;
            fallingTimer = INITIALFALLINGTIMER;
            currentStormTimer = initialStormTimer / 2;
            GameObject.Find("PlayerAndCamera/UI/Night").SetActive(false);
            GameObject.Find("PlayerAndCamera/UI/Day").SetActive(false);
        }
    }
}
