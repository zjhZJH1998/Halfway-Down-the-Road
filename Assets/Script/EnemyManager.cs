using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyManager : MonoBehaviour
{

    public List<GameObject> enemies;

    public List<GameObject> enemyPrefabs;
    private Transform finalTarget;

    public float height;
    public float length;
    public float spawningTime;
    public float waitTime;
    public float timer;

    public int morale;
    [SerializeField]
    private int levelID;
    [SerializeField]
    private int threatValue;

    public GameObject winPanel;
    public AudioSource winAudio;
    private bool isWin = false;

    private void Awake()
    {
        finalTarget = GameObject.FindWithTag("Target").transform;
        
        // level information
        if (GameManager._instance != null)
        {
            levelID = GameManager._instance.levelID;
            threatValue = levelManager._instance.GetThreatValue(levelID);
        }
        else
        {
            //go to sample fight
            levelID = 99;
            threatValue = 40;
            
        }


        EventCenter.AddListener<int>(EventDefine.moraleChange, moraleChange);
        EventCenter.AddListener<Vector3>(EventDefine.GenerateSmallSmile, generateSmallSmile);
    }

    private void Start()
    {
        morale = 100;
        if (levelID == 99)
        {
            morale = 20;
        }
    }

    private void Update()
    {
        if(waitTime > 0)
            waitTime -= Time.deltaTime;

        timer -= Time.deltaTime;
        if (timer < 0 && waitTime <= 0)
        {
            //EventCenter.Broadcast(EventDefine.LoadMap);
            Spawning();
            timer = spawningTime;

        }

        for(int i = 0;i< enemies.Count;i++)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }
            else if(enemies[i].GetComponent<EnemyBase>().HP <= 0)
            {
                morale -= (5 - (threatValue / 10));
                GameObject tmp = enemies[i].gameObject;
                enemies.RemoveAt(i);
                Destroy(tmp);

            }
            else if(enemies[i].transform.position.x < -20 || enemies[i].transform.position.x > 20
                || enemies[i].transform.position.y > 11 || enemies[i].transform.position.y < -11)
            {
                GameObject tmp = enemies[i].gameObject;
                enemies.RemoveAt(i);
                Destroy(tmp);
            }
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null && enemies[i].GetComponent<EnemyBase>().Morale >= morale && !enemies[i].GetComponent<EnemyBase>().isFleeing)
            {
                enemies[i].GetComponent<EnemyBase>().set2Flee();

            }
        }


        if(enemies.Count <= 0 && morale < 50 - threatValue )
        {
            if (levelID == 5)
            {
                EventCenter.Broadcast(EventDefine.Win);
            }
            else if(levelID == 99)
            {
                SceneManager.LoadScene("GameStart");
            }
            else
            {
                //EventCenter.Broadcast(EventDefine.LoadMap);
                winBattle();
            }
           
            
        }

    }
    void winBattle()
    {
        
        Time.timeScale = 0f;
        if (!isWin)
        {
            winAudio.Play();
            isWin = true;
        }
        winPanel.SetActive(true);

    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener<int>(EventDefine.moraleChange, moraleChange);
        EventCenter.RemoveListener<Vector3>(EventDefine.GenerateSmallSmile, generateSmallSmile);
    }

    private void Spawning()
    {

        float r1, r2;

        r1 = Random.Range(-length, length);
        r2 = Random.Range(-height, height);

        if (r1  > 0)
        {
            r1 = length;
        }
        else
        {
           r1 = -length;
        }

        //if (r1 * r2 > 0)
        //{
        //    if (finalTarget.position.x > 0)
        //    {
        //        r1 = -Mathf.Abs(r1);
        //        if (r2 > 0) r2 = height;
        //        else r2 = -height;
        //    }
        //    else
        //    {
        //        r1 = Mathf.Abs(r1);
        //        if (r2 > 0) r2 = height;
        //        else r2 = -height;
        //    }

        //}
        //else
        //{
        //    r1 = 0;

        //    if (finalTarget.position.y > 0)
        //    {
        //        r2 = -Mathf.Abs(r2);
        //        if (r1 > 0) r1 = length;
        //        else r1 = -length;
        //    }
        //    else
        //    {
        //        r2 = Mathf.Abs(r2);
        //        if (r1 > 0) r1 = length;
        //        else r1 = -length;
        //    }
        //}

        int threatLevel = (threatValue / 10);
        bool eliteSpawn = false; 
        if(threatLevel > 1 && Random.Range(0,10) > 8 - threatLevel)
        {
            eliteSpawn = true;
        }



        if (morale >= 50 - threatValue)
        {
            Debug.Log(50 - threatValue);
            if(levelID == 0)
            {
                GameObject go;
                if (eliteSpawn)
                {
                    go = Instantiate(enemyPrefabs[3], new Vector3(r1, r2, 0.0f), Quaternion.identity);
                }
                else
                    go = Instantiate(enemyPrefabs[0], new Vector3(r1, r2, 0.0f), Quaternion.identity);
                go.transform.parent = this.transform;
                go.GetComponent<EnemyBase>().Init(finalTarget, (threatValue / 10));
                enemies.Add(go);
            }
            else if(levelID == 1)
            {
                GameObject go;
                if (eliteSpawn)
                {
                    go = Instantiate(enemyPrefabs[5], new Vector3(r1, r2, 0.0f), Quaternion.identity);
                }
                else
                    go = Instantiate(enemyPrefabs[0], new Vector3(r1, r2, 0.0f), Quaternion.identity);
                go.transform.parent = this.transform;
                go.GetComponent<EnemyBase>().Init(finalTarget, (threatValue / 10));
                enemies.Add(go);
            }
            else if(levelID == 2)
            {
                GameObject go;
                if (eliteSpawn)
                {
                    go = Instantiate(enemyPrefabs[6], new Vector3(r1, r2, 0.0f), Quaternion.identity);
                }
                else
                    go = Instantiate(enemyPrefabs[0], new Vector3(r1, r2, 0.0f), Quaternion.identity);
                go.transform.parent = this.transform;
                go.GetComponent<EnemyBase>().Init(finalTarget, (threatValue / 10));
                enemies.Add(go);
            }
            else if(levelID == 5)
            {
                GameObject go;
                threatValue = 40;
                if (Random.Range(0,10) > 5 )
                {
                    int r = Random.Range(0,3);
                    if(r == 0) go = Instantiate(enemyPrefabs[6], new Vector3(r1, r2, 0.0f), Quaternion.identity);
                    else if(r == 1) go = Instantiate(enemyPrefabs[3], new Vector3(r1, r2, 0.0f), Quaternion.identity);
                    else  go = Instantiate(enemyPrefabs[5], new Vector3(r1, r2, 0.0f), Quaternion.identity);
                } 
                else
                {
                    go = Instantiate(enemyPrefabs[0], new Vector3(r1, r2, 0.0f), Quaternion.identity);

                    go.transform.parent = this.transform;
                    go.GetComponent<EnemyBase>().Init(finalTarget, (threatValue / 10));
                    enemies.Add(go);
                }

                go.transform.parent = this.transform;
                go.GetComponent<EnemyBase>().Init(finalTarget, (threatValue / 10));
                enemies.Add(go);
            }
            else if (levelID == 99)
            {
                GameObject go;
                
                go = Instantiate(enemyPrefabs[0], new Vector3(r1, r2, 0.0f), Quaternion.identity);
                go.transform.parent = this.transform;
                go.GetComponent<EnemyBase>().Init(finalTarget, (threatValue / 10));
                enemies.Add(go);
            }
        }

    }

    private void moraleChange(int num)
    {
        morale += num; 
    }
    private void generateSmallSmile(Vector3 bigSmilePos)
    {
        int flag = Random.Range(0, 2);
        Vector3 generatePos = bigSmilePos;
        if(flag == 0)
        {
            generatePos += new Vector3(0,2,0);
        }
        else
        {
            generatePos += new Vector3(0, -2, 0);
        }
        GameObject go = Instantiate(enemyPrefabs[4], generatePos, Quaternion.identity);
        go.transform.parent = this.transform;
        go.GetComponent<EnemyBase>().Init(finalTarget, (threatValue / 10));
        enemies.Add(go);
    }
}
