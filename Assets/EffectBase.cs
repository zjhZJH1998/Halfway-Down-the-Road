using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectBase : MonoBehaviour
{
    private void OnFinishedPlay()
    {
        Destroy(gameObject);
    }    
}
