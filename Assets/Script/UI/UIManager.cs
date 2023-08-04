using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static UIManager Instance;
    public Camera UICamera;
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera
            || canvas.renderMode == RenderMode.WorldSpace)
        {
            UICamera = canvas.worldCamera;
        }
        else if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            UICamera = null;
        }
    }

    public static UIManager GetInstance()
    {
        return Instance;
    }

}
