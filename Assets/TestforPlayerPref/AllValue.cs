using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllValue : MonoBehaviour
{
    private Text tm;

    private void Awake()
    {
        tm = transform.Find("value").GetComponent<Text>();
    }

    private void Start()
    {
        tm.text = PlayerPrefs.GetInt("value").ToString();
    }
}
