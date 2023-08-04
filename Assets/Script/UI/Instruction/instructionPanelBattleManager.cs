using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instructionPanelBattleManager : MonoBehaviour
{
    public GameObject Hat;
    public GameObject HatCursor;
    public GameObject card;
    public GameObject cardCursor;
    public GameObject resource;
    public GameObject resourceCursor;
    public GameObject HP;
    public GameObject HPCursor;
    public GameObject firstPanel;
    void Start()
    {
        if (PlayerPrefs.GetInt("Instruction") == 1)
        {
            initializeCursors();
            Time.timeScale = 0f;
            firstPanel.SetActive(true);
        }
    }

    private void initializeCursors()
    {
        Vector2 screenPos = Camera.main.WorldToScreenPoint(Hat.transform.position);
        RectTransform rt = HatCursor.GetComponent<RectTransform>();
        Vector3 uiPoint = PositionConvert.ScreenPointToUIPoint(rt, screenPos);
        Debug.Log(uiPoint.x + " " + uiPoint.y);
        HatCursor.transform.position = new Vector3(uiPoint.x - 376, uiPoint.y - 39, uiPoint.z);
        cardCursor.transform.position = new Vector3(card.transform.position.x - 200, card.transform.position.y + 110, card.transform.position.z);
        HPCursor.transform.position = new Vector3(HP.transform.position.x, HP.transform.position.y + 60, HP.transform.position.z);
        resourceCursor.transform.position = new Vector3(resource.transform.position.x - 180, resource.transform.position.y + 70, resource.transform.position.z);

    }
}
