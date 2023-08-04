//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.EventSystems;

//public class Hero1Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
//{
//    private Image mask;

//    public float CDTime;

//    public float currentTimeForCD;

//    public bool canPlace;

//    public bool wantPlace;

//    public GameObject Hero1;
//    public ResourceManager resourceManager;

//    public bool CanPlace
//    {
//        get => canPlace;
//        set
//        {

//            canPlace = value;

//            //can't place
//            if (!canPlace)
//            {
//                mask.fillAmount = 1;
//                CDEnter();
//            }
//            else
//            {
//                mask.fillAmount = 0;
//            }
//        }
//    }

//    public bool WantPlace
//    {
//        get => wantPlace; set
//        {
//            wantPlace = value;
//            if (wantPlace)
//            {
//                resourceManager.playerResource1Num -= 1;
//                resourceManager.playerResource2Num -= 2;
//                GameObject prefab = CardManager.Instance.GetHeroForType(CardManager.HeroType.Hero1);
//                Hero1 = GameObject.Instantiate<GameObject>(prefab, Vector3.zero, Quaternion.identity, CardManager.Instance.transform);
//            }
//            else
//            {
//                if (Hero1 != null)
//                {
//                    Destroy(Hero1.gameObject);
//                    Hero1 = null;
//                }

//            }
//        }
//    }

//    void Start()
//    {
//        mask = transform.Find("Mask").GetComponent<Image>();
//        CanPlace = false;
//        resourceManager = ResourceManager.Instance;
//    }
//    private void CDEnter()
//    {

//        StartCoroutine(CalCD());

//    }
//    IEnumerator CalCD()
//    {
//        float calCD = (1 / CDTime) * 0.1f;
//        currentTimeForCD = CDTime;
//        while (currentTimeForCD >= 0)
//        {
//            yield return new WaitForSeconds(0.1f);
//            mask.fillAmount -= calCD;
//            currentTimeForCD -= 0.1f;
//        }
//        canPlace = true;
//    }


//    public void OnPointerEnter(PointerEventData eventData)
//    {
//        if (!CanPlace) return;
//        transform.localScale = new Vector2(1.05f, 1.05f);
//    }

//    public void OnPointerExit(PointerEventData eventData)
//    {
//        if (!CanPlace) return;
//        transform.localScale = new Vector2(1f, 1f);
//    }

//    // Start is called before the first frame update


//    // Update is called once per frame
//    void Update()
//    {
//        if (WantPlace && Hero1 != null)
//        {
//            //hero follow mouse

//            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            Hero1.transform.position = new Vector3(mousePoint.x, mousePoint.y, 0f);
//            if (Input.GetMouseButtonDown(0))
//            {
//                Hero1 = null;
//                WantPlace = false;
//                CanPlace = false;
//            }

//        }
//    }

//    public void OnPointerClick(PointerEventData eventData)
//    {
//        if (!CanPlace) return;
//        if (!WantPlace)
//        {
//            WantPlace = true;
//        }
//    }
//}
