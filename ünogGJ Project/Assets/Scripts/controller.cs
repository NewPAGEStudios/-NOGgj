using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controller : MonoBehaviour
{
    map currentStage;
    enum map
    {
        map1,
        map2
    }
    [SerializeField]
    private GameObject pausePanel; 

    public Transform map1Point;
    public Transform map2Point;
    public int needKilltoLevelUP = 5;
    public GameObject[] portalsPoint;
    public GameObject[] enemies;
    public int maxEnemyCount;
    private float spawnTimer;
    // Update is called once per frame
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        currentStage = map.map1;
        needKilltoLevelUP = 5;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("bomba");
            pausePanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
        if (needKilltoLevelUP <= 0)
        {
            if(currentStage == map.map1)
            {
                currentStage = map.map2;
                GameObject.FindWithTag("Player").transform.position = map2Point.position;
            }
            else if (currentStage == map.map2) 
            {
                Debug.Log("EndGame");
            }
        }
        int count = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if(count >= maxEnemyCount)
        {
            return;
        }
        else
        {
            if (spawnTimer <= 0)
            {
                if(currentStage == map.map1)
                {
                    Instantiate(enemies[Random.Range(0, enemies.Length)], portalsPoint[Random.Range(0, 1)].transform.position, Quaternion.Euler(0f, 0f, 0));
                }
                else if(currentStage == map.map2)
                {
                    Instantiate(enemies[Random.Range(0, enemies.Length)], portalsPoint[Random.Range(1, 5)].transform.position, Quaternion.Euler(0f, 0f, 0));

                }
                count += 1;
                spawnTimer = 3f;
            }
            else
            {
                spawnTimer -= Time.deltaTime;
            }
        }
    }
    public void devamEt()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void menü()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
