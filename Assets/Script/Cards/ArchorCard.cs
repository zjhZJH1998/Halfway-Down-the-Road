//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UnityEngine.EventSystems;

//public class ArchorCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
//{
//    private Image mask;

//    public float CDTime;

//    public float currentTimeForCD;

//    public bool canPlace;

//    public bool wantPlace;

//    public GameObject Archor;

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
//                GameObject prefab = CardManager.Instance.GetHeroForType(CardManager.HeroType.Archor);
//                Archor = GameObject.Instantiate<GameObject>(prefab, Vector3.zero, Quaternion.identity, CardManager.Instance.transform);
//            }
//            else
//            //{
//                if (Archor != null)
//                {
//                    Destroy(Archor.gameObject);
//                    Archor = null;
//                }

//            }
//        }
//    }

//    void Start()
//    {
//        mask = transform.Find("Mask").GetComponent<Image>();
//        CanPlace = false;
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
//        if (WantPlace && Archor != null)
//        {
//            //hero follow mouse

//            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            Archor.transform.position = new Vector3(mousePoint.x, mousePoint.y, 0f);
//            if (Input.GetMouseButtonDown(0))
//            {
//                Archor = null;
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
