using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class finishInstrBtn : MonoBehaviour
{
    // Start is called before the first frame update
  
    public void finishInstruction()
    {   
        SceneManager.LoadScene("GameStart");
    }
}
