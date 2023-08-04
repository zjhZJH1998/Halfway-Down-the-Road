using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourceText : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private TextMeshProUGUI resouceGreenNum;
    [SerializeField]
    private TextMeshProUGUI resouceRedNum;
    [SerializeField]
    private TextMeshProUGUI resouceBlueNum;
    [SerializeField]
    private TextMeshProUGUI resouceBlackNum;
    [SerializeField]
    private TextMeshProUGUI resouceWhiteNum;
    private ResourceManager resourceManager;

    private void Awake()
    {
        resouceGreenNum = GetComponentsInChildren<TextMeshProUGUI>()[0];
        resouceRedNum = GetComponentsInChildren<TextMeshProUGUI>()[1];
        resouceBlueNum = GetComponentsInChildren<TextMeshProUGUI>()[2];
        //resouceBlackNum = GetComponentsInChildren<TextMeshProUGUI>()[3];
        //resouceWhiteNum = GetComponentsInChildren<TextMeshProUGUI>()[4];
        resourceManager = FindObjectOfType<ResourceManager>();
    }
    private void Start()
    {
        resouceGreenNum.text = resourceManager.playerResourceGreenNum.ToString();
        resouceRedNum.text = resourceManager.playerResourceRedNum.ToString();
        resouceBlueNum.text = resourceManager.playerResourceBlueNum.ToString();
        //resouceBlackNum.text = resourceManager.playerResourceBlackNum.ToString();
        //resouceWhiteNum.text = resourceManager.playerResourceWhiteNum.ToString();
    }
    void Update()
    {
        resouceGreenNum.text = resourceManager.playerResourceGreenNum.ToString();
        resouceRedNum.text = resourceManager.playerResourceRedNum.ToString();
        resouceBlueNum.text = resourceManager.playerResourceBlueNum.ToString();
        //resouceBlackNum.text = resourceManager.playerResourceBlackNum.ToString();
        //resouceWhiteNum.text = resourceManager.playerResourceWhiteNum.ToString();
    }
}
