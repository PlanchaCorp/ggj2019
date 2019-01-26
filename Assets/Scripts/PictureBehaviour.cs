using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PictureBehaviour : MonoBehaviour
{
    private GameObject player;
    private float INITIALPICTUREFADINGINTIME = 3.0f;
    private float INITIALPICTUREFADINGOUTTIME = 3.0f;
    private Image pictureSprite;
    private float currentPictureFadingTime;
    private float currentPictureFadingOutTime;
    private bool pictureHasStartedDisplaying;
    private bool picturehasDisplayed;
    private bool pictureIsSkipped;
    private bool pictureHasStartedFadingOut;
    private bool pictureHasFadedOut;
    private Vector2 playerNightInitialPosition;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pictureHasStartedDisplaying = false;
        pictureSprite = GameObject.FindGameObjectWithTag("Picture").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pictureHasStartedDisplaying)
        {
            if (!picturehasDisplayed)
            {
                currentPictureFadingTime -= Time.deltaTime;
                pictureSprite.color = new Color(1, 1, 1, 1 - currentPictureFadingTime / INITIALPICTUREFADINGINTIME);
                if (currentPictureFadingTime < 0)
                {
                    picturehasDisplayed = true;
                }
            } else
            {
                if (Input.GetButtonDown("Jump") && !pictureIsSkipped)
                {
                    pictureIsSkipped = true;
                    DayNightManager.SetDay(false);
                    currentPictureFadingOutTime = INITIALPICTUREFADINGOUTTIME;
                }
            }
            if (pictureIsSkipped && !pictureHasFadedOut)
            {
                currentPictureFadingOutTime -= Time.deltaTime;
                pictureSprite.color = new Color(1, 1, 1, currentPictureFadingOutTime / INITIALPICTUREFADINGOUTTIME);
                if (currentPictureFadingOutTime < 0)
                {
                    pictureHasFadedOut = true;
                    player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                }
            }
        }
    }

    public void StartAnimation()
    {
        pictureHasStartedDisplaying = true;
        picturehasDisplayed = false;
        pictureIsSkipped = false;
        pictureHasStartedFadingOut = false;
        pictureHasFadedOut = false;
        currentPictureFadingTime = INITIALPICTUREFADINGINTIME;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        playerNightInitialPosition = player.transform.position;
    }

    public Vector2 GetPlayerNightInitialPosition()
    {
        return playerNightInitialPosition;
    }
}
