using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LigthController : MonoBehaviour
{

    private Color neonColor;
    private Animator neonAnimator;
    // Start is called before the first frame update

    void Start()
    {
        neonColor = Color.magenta;
        neonColor = NeonColor.Colors[Mathf.FloorToInt(Random.Range(0, 3))];
        Debug.Log(neonColor.ToString());
        this.GetComponent<SpriteRenderer>().color = neonColor;   
        SetDayLigthColor();
        neonAnimator = gameObject.GetComponentInChildren<Animator>();
    }

    public void SetNightLightColor()
    {
        neonAnimator = gameObject.GetComponentInChildren<Animator>();
        neonAnimator.SetBool("Night", true);
      
    }
    public void SetDayLigthColor()
    {
        neonAnimator = gameObject.GetComponentInChildren<Animator>();
        neonAnimator.SetBool("Night", false);
    }


}
