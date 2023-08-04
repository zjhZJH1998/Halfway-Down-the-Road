using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtelierPanel : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            this.gameObject.SetActive(false);
        }
    }
}
