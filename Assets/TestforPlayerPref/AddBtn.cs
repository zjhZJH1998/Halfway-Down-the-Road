using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddBtn : MonoBehaviour
{
    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(Add);
    }

    private void Add()
    {
        int value = PlayerPrefs.GetInt("value");
        value++;
        PlayerPrefs.SetInt("value", value);
    }
}
