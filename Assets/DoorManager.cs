using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorManager : MonoBehaviour
{
   
    public void Open()
    {
        Debug.Log("Open");
        transform.Find("ClosedGate").gameObject.SetActive(false);
        transform.Find("OpennedGate").gameObject.SetActive(true);
    }
}
