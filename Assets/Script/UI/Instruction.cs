using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instruction : MonoBehaviour
{
    // Start is called before the first frame update
    public Button btn;
    public GameObject Instructions;
    public GameObject Hat;
    public GameObject HatCursor;
    public GameObject Timer;
    public GameObject TimerCursor;
    public GameObject resource;
    public GameObject resourceCursor;
    public GameObject Town;
    public GameObject TownCursor;
    private void Awake()
    {
        btn.onClick.AddListener(openClick);
    }
    void openClick()
    {
        if(Instructions.active ==false) Instructions.SetActive(true);
        else Instructions.SetActive(false);
        Vector2 screenPos = Camera.main.WorldToScreenPoint(Hat.transform.position);
        RectTransform rt = HatCursor.GetComponent<RectTransform>();
        Vector3 uiPoint = PositionConvert.ScreenPointToUIPoint(rt, screenPos);
        Debug.Log(uiPoint.x + " " + uiPoint.y);
        HatCursor.transform.position = new Vector3(uiPoint.x-376, uiPoint.y-39, uiPoint.z);
        Vector2 screenPosTown = Camera.main.WorldToScreenPoint(Town.transform.position);
        RectTransform rtTown = TownCursor.GetComponent<RectTransform>();
        Vector3 uiPointTown = PositionConvert.ScreenPointToUIPoint(rtTown, screenPosTown);
        Debug.Log(uiPointTown.x + " " + uiPointTown.y);
        TownCursor.transform.position = new Vector3(uiPointTown.x -500, uiPointTown.y - 39, uiPointTown.z);
        resourceCursor.transform.position = new Vector3(resource.transform.position.x-500, resource.transform.position.y, resource.transform.position.z);
        TimerCursor.transform.position = new Vector3(Timer.transform.position.x - 500, Timer.transform.position.y-39, Timer.transform.position.z);


    }
}
