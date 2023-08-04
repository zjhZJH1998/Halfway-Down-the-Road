using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InstructionBattle : MonoBehaviour
{
    // Start is called before the first frame update
    public Button btn;
    public GameObject Instructions;
    public GameObject Hat;
    public GameObject HatCursor;
    public GameObject card;
    public GameObject cardCursor;
    public GameObject resource;
    public GameObject resourceCursor;
    public GameObject HP;
    public GameObject HPCursor;



    private void Awake()
    {
        btn.onClick.AddListener(openClick);
    }
    void openClick()
    {
        if (Instructions.active == false) Instructions.SetActive(true);
        else Instructions.SetActive(false);
        Vector2 screenPos = Camera.main.WorldToScreenPoint(Hat.transform.position);
        RectTransform rt = HatCursor.GetComponent<RectTransform>();
        Vector3 uiPoint = PositionConvert.ScreenPointToUIPoint(rt, screenPos);
        Debug.Log(uiPoint.x + " " + uiPoint.y);
        HatCursor.transform.position = new Vector3(uiPoint.x - 376, uiPoint.y - 39, uiPoint.z);
        cardCursor.transform.position = new Vector3(card.transform.position.x-200, card.transform.position.y+110, card.transform.position.z);
        HPCursor.transform.position = new Vector3(HP.transform.position.x, HP.transform.position.y + 60, HP.transform.position.z);
        resourceCursor.transform.position = new Vector3(resource.transform.position.x -180, resource.transform.position.y + 70, resource.transform.position.z);

    }
}
