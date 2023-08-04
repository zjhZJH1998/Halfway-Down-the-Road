using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoutDownUI : MonoBehaviour
{
    private Text weekLeft;

    private void Awake()
    {
        weekLeft = GetComponentInChildren<Text>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        weekLeft.text = "Week: " + levelManager._instance.WeekLeft;
    }
}
