using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    private Slider slider;
    private GameObject Wizard;
    // Start is called before the first frame update

    private void Awake()
    {

        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        Wizard = GameObject.FindGameObjectWithTag("Target");
        if(Wizard != null)
        {
            slider.maxValue = Wizard.GetComponent<Wizard>().HP;
        }
    }

    private void Update()
    {
        slider.value = Wizard.GetComponent<Wizard>().HP;
    }

}
