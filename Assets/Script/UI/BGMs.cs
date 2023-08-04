using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMs : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource bgm1;
    public AudioSource bgm2;
    private void Start()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                bgm1.enabled = false;
                bgm2.enabled = true;
                break;
            case 1:
                bgm2.enabled = false;
                bgm1.enabled = true;
                break;

        }
    }
}
