using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class fadeinfadeout : MonoBehaviour
{
    // Start is called before the first frame update
    public Image img1;
    public Image img2;
    public Image img3;
    private float delayTime = 2;

    private void fadeout()
    {
        img1.CrossFadeAlpha(0, 1.5f, false);
        img2.CrossFadeAlpha(0, 1.5f, false);
        img3.CrossFadeAlpha(0, 1.5f, false);
        Invoke("startgame", delayTime);
        

    }
    private void startgame()
    {
        SceneManager.LoadScene("GameStart");
    }
    private void Start()
    {
        img1.CrossFadeAlpha(0f, 0f, true);
        img2.CrossFadeAlpha(0f, 0f, true);
        img3.CrossFadeAlpha(0f, 0f, true);
        img1.CrossFadeAlpha(1, 1.5f, false);
        img2.CrossFadeAlpha(1, 1.5f, false);
        img3.CrossFadeAlpha(1, 1.5f, false);
        Invoke("fadeout", delayTime);

    }
}
