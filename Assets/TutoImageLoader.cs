using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutoImageLoader : MonoBehaviour
{

    public List<Sprite> images;
    private Image tutoImage;
    private int index; 

    // Start is called before the first frame update
    void Start()
    {
        tutoImage = transform.Find("TutoImage").GetComponent<Image>();
        index = 0;
        loadImage();
    }

    public void next()
    {
        if(index < images.Count -1)
        {
            index++;
        }
        else
        {
            this.gameObject.SetActive(false);
        }

        loadImage();
    }
    public void prev()
    {
        if (index > 0)
        {
            index--;
        }
        else
        {
            this.gameObject.SetActive(false);
        }

        loadImage();
    }

    private void loadImage()
    {
        tutoImage.sprite = images[index];
    }
    public void displayPanel()
    {
        this.gameObject.SetActive(true);
    }


}
