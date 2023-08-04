using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float a = Mathf.Atan2(1, 1)*Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0, 0, a);
    }

    // Update is called once per frame
    
}
