using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReduceTest : MonoBehaviour
{
    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(Reduce);
    }

    private void Reduce()
    {
        int value = PlayerPrefs.GetInt("value");
        value--;
        PlayerPrefs.SetInt("value", value);
    }
}
