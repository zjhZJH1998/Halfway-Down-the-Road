using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class battle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene("SampleScene");
    }
    //{

    //    SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}
