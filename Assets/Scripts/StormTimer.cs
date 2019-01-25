using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormTimer : MonoBehaviour
{
    [SerializeField]
    private float initialTimer = 10;

    private SpriteRenderer stormSprite;
    private float currentTimer;
    private bool runningOutOfTime;
    
    void Start()
    {
        runningOutOfTime = false;
        currentTimer = initialTimer;
        stormSprite = gameObject.GetComponent<SpriteRenderer>();
        if (stormSprite == null)
        {
            Debug.LogError("Storm sprite not attached to Storm object !");
        }
        stormSprite.color = new Color(1f, 1f, 1f, 0f);
    }
    
    void Update()
    {
        if (currentTimer > 0)
        {
            currentTimer -= Time.deltaTime;
            if (currentTimer < initialTimer / 2)
            {
                stormSprite.color = new Color(1f, 1f, 1f, 1 - currentTimer / (initialTimer / 2));
            }
        } else if (!runningOutOfTime)
        {
            runningOutOfTime = true;
            Debug.Log("Running out of time.");
        }
    }
}
