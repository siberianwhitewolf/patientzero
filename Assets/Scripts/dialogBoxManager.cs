using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class dialogBoxManager : MonoBehaviour
{
    public TextMeshProUGUI itemDescription;
    public Image itemImage;

   public void setDescription(string description)
    {
        itemDescription.text = description;
    }

   public void setImage(Sprite image)
    {
        itemImage.sprite = image;
    }

    public void clearDescription()
    {
        itemDescription.text = "";
    }

    public void clearImage()
    {
        itemImage.sprite = null;
    }


}
