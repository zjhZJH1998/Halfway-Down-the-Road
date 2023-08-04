using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToShowScene : MonoBehaviour
{
    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(LoadADDscene);
    }

    private void LoadADDscene()
    {
        SceneManager.LoadScene("ShowUp");
    }
}
