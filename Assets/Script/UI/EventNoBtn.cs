using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventNoBtn : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject eventPanel;
    private int eventType;
    public AudioSource footstep;
    private void Awake()
    {
        EventCenter.AddListener<int>(EventDefine.EventPanel, setEventType);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener<int>(EventDefine.EventPanel, setEventType);
    }
    private void setEventType(int eventType)
    {
        this.eventType = eventType;
    }

    public void eventNo()
    {
        eventPanel.SetActive(false);
        Time.timeScale = 1;
        footstep.Play();

    }
}
