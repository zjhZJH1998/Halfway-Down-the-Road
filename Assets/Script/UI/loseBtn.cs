using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loseBtn : MonoBehaviour
{
    // Start is called before the first frame update
    public void lose()
    {
        SceneManager.LoadScene("GameStart");
    }
}
