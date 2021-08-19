using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChangeBgColor : MonoBehaviour
{
    public Camera camera;
    public Color color;


    void ChangeBackgroundColor()
    {
        camera.backgroundColor = color;
    }



}
