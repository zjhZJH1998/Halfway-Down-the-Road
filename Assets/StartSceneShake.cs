using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneShake : MonoBehaviour
{

    private bool isFlip;
    [SerializeField]
    private float offset;
    [SerializeField]
    private float ShakeTime;
    private float timer;
    private Vector3 origin;

    private void Start()
    {
        timer = ShakeTime;
        origin = transform.position;
    }
    private void Update()
    {

        timer -= Time.deltaTime;
        if(isFlip)
        {
            Vector3 nextPosition = Vector3.Lerp(origin, origin + new Vector3(0,offset,0), (1 - timer / ShakeTime));
            transform.position = new Vector3(transform.position.x + Time.deltaTime, nextPosition.y, transform.position.z);
        }
        else
        {
            Vector3 nextPosition = Vector3.Lerp(origin + new Vector3(0, offset, 0), origin, (1 - timer / ShakeTime));
            transform.position = new Vector3(transform.position.x + Time.deltaTime, nextPosition.y, transform.position.z );
        }

        if(timer <= 0)
        {
            timer = ShakeTime;
            isFlip = !isFlip;
        }
    }
}
