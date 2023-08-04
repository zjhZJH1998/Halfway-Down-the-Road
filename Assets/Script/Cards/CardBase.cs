using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardType
{
    Archor,
    Swordsman,
    MetalPolymer,
    WaterElement,
    SP,
    FG,
    Skull
}

public class CardBase : MonoBehaviour
{
    // Card basic information
    public CardType cardType;
    public float summonTime;
    [SerializeField]
    private int resourceGreenCost;
    [SerializeField]
    private int resourceRedCost;
    [SerializeField]
    private int resourceBlueCost;
    [SerializeField]
    private ResourceManager resourceManager;
    
    private Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
        resourceManager = FindObjectOfType(typeof(ResourceManager)) as ResourceManager;
        btn.onClick.AddListener(Click2Summon);
    }

    private void Click2Summon()
    {
        if(resourceManager.checkResource(ResourceType.ResourceGreen, -resourceGreenCost)&& resourceManager.checkResource(ResourceType.ResourceRed, -resourceRedCost)&& resourceManager.checkResource(ResourceType.ResourceBlue, -resourceBlueCost))
        {
            EventCenter.Broadcast(EventDefine.Summon, cardType, summonTime);
        }
       
    }

}
