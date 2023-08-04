using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTest : MonoBehaviour
{
    // Start is called before the first frame update
    private Inventory inventory;
    [SerializeField]
    private UI_Inventory uiInventory;
    private void Awake()
    {
        inventory = new Inventory();
        
    }
    void Start()
    {
        uiInventory.SetInventory(inventory);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
