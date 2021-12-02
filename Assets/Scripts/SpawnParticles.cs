using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnParticles : MonoBehaviour
{
    public GameObject[] Particles;
    public float spawnTime;
    public Transform[,] grid;
    public float timer = 1.0f;
    public GameObject[] notification;
    public Text[] upHUD;
    public Text[] downHUD;
    public GameObject player;
    public GameObject pauseMenu;
    public GameObject restartBtn;
    public bool lose1;
    public bool lose2;
    public bool lose3;
    public bool gamePaused;
    public bool gameEnd;
    public int DBUnder;
    public double score;
    public double destroyDCount;
    private int[] colCount;
    private int rgbCount;
    private int total;
    public float startTime = 0f;
    public string lastTag;


    void Awake()
    {
        score = 0;
        DBUnder = 0;
        destroyDCount = 0;
        rgbCount = 0;
        colCount = new int[3];
        lose1 = false;
        lose2 = false;
        lose3 = false;
        grid = new Transform[5, 5];
        lastTag = null;
    }

    private void Start()
    {
        CreateParticle();
        StartCoroutine(NewParticle());
    }

    public void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (gamePaused && !gameEnd)
            {
                Time.timeScale = 1;
                gamePaused = false;
                pauseMenu.SetActive(false);
                restartBtn.SetActive(false);
            }
            else if (!gamePaused && !gameEnd)
            {
                Time.timeScale = 0;
                gamePaused = true;
                pauseMenu.SetActive(true);
                restartBtn.SetActive(true);
            }
        }

        timer -= Time.deltaTime;
        if (timer <= 0 && !gamePaused)
        {
            upHUD[3].text = Convert.ToInt32(Time.time - startTime).ToString(); //Show time
            timer = 1.0f;
        }

    }

    private void FixedUpdate()
    {
        if (lose1)
        {
            notification[1].SetActive(true);
            restartBtn.SetActive(true);
            EndGame();
        }

        if (DBUnder > 5)
        {
            lose2 = true;
            notification[2].SetActive(true);
            restartBtn.SetActive(true);
            EndGame();
        }

        if (lose3)
        {
            notification[3].SetActive(true);
            player.GetComponent<SpriteRenderer>().color = new Color(195, 195f, 195f);
            restartBtn.SetActive(true);
            EndGame();
        }

    }

    IEnumerator NewParticle()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            CreateParticle();
        }
    }

    void CreateParticle() 
    {
        float initialX = 2.5f;
        if (lastTag == null)
        {
            GameObject particle = Instantiate(Particles[UnityEngine.Random.Range(0, Particles.Length)]);
            lastTag = particle.tag;
            particle.transform.SetParent(this.transform);
            particle.transform.position = new Vector3(initialX, 12.6f, 0f);
        }
        else 
        {
            GameObject temp = Particles[UnityEngine.Random.Range(0, Particles.Length)];
            while (temp.tag == lastTag) 
            {
                temp = Particles[UnityEngine.Random.Range(0, Particles.Length)];
            }
            lastTag = temp.tag;
            GameObject particle = Instantiate(temp);
            particle.transform.SetParent(this.transform);
            particle.transform.position = new Vector3(initialX, 12.6f, 0f);
        }

    }

    public void CheckForLines()
    {
        for (int i = 4; i >= 0; i--)
        {
            if (HasLine(i))
            {
                rgbCount++;
                score += 15;
                SetHUD();
            }
        }
    }
    bool HasLine(int i) // Check RGB
    {
        for (int j = 0; j < 3; j++)
        {
            if (grid[j, i] != null)
            {
                if (grid[j, i].tag == "R")
                {
                    int nextGridJ = j + 1;
                    if (nextGridJ < 5) 
                    {
                        if (grid[nextGridJ, i] != null && grid[nextGridJ, i].tag == "G")
                        {
                            nextGridJ++;
                            if (nextGridJ < 5)
                            {
                                if (grid[nextGridJ, i] != null && grid[nextGridJ, i].tag == "B")
                                {
                                    Destroy(grid[j, i].gameObject);
                                    grid[j, i] = null;
                                    for (int k = i + 1; k < 5; k++) 
                                    {
                                        if (grid[j, k] != null)
                                        {
                                            grid[j, k].gameObject.GetComponent<ParticleBehavior>().probe.isStable = false;
                                            grid[j, k].gameObject.GetComponent<ParticleBehavior>().added = false;
                                            grid[j, k] = null;
                                        }
                                    }

                                    Destroy(grid[j + 1, i].gameObject);
                                    grid[j + 1, i] = null;
                                    for (int k = i + 1; k < 5; k++)
                                    {
                                        if (grid[j + 1, k] != null)
                                        {
                                            int temp = j + 1;
                                            grid[j + 1, k].gameObject.GetComponent<ParticleBehavior>().probe.isStable = false;
                                            grid[j + 1, k].gameObject.GetComponent<ParticleBehavior>().added = false;
                                            grid[j + 1, k] = null;
                                        }
                                    }

                                    Destroy(grid[j + 2, i].gameObject);
                                    grid[j + 2, i] = null;
                                    for (int k = i + 1; k < 5; k++)
                                    {
                                        if (grid[j + 2, k] != null)
                                        {
                                            int temp = j + 2;
                                            grid[j + 2, k].gameObject.GetComponent<ParticleBehavior>().probe.isStable = false;
                                            grid[j + 2, k].gameObject.GetComponent<ParticleBehavior>().added = false;
                                            grid[j + 2, k] = null;
                                        }
                                    }
                                    return true;
                                }
                            }
                        }
                    }

                }
            }
        }
        return false;
    }

    public void CheckForColumns()
    {
        for (int i = 4; i >= 0; i--)
        {
            if (HasColumn(i))
            {
                score += 10;
                SetHUD();
            }
        }
    }

    bool HasColumn(int i) // Check three in a column
    {
        for (int j = 0; j < 3; j++)
        {
            if (grid[i, j] != null)
            {
                string tempTag = grid[i, j].tag;
                int nextGridJ = j + 1;
                if (nextGridJ < 5 && tempTag != "D")
                {
                    if (grid[i, nextGridJ] != null && grid[i, nextGridJ].tag == tempTag)
                    {
                        nextGridJ++;
                        if (nextGridJ < 5)
                        {
                            if (grid[i, nextGridJ] != null && grid[i, nextGridJ].tag == tempTag)
                            {
                                Destroy(grid[i, j].gameObject);
                                grid[i, j] = null;
                                Destroy(grid[i, j + 1].gameObject);
                                grid[i, j+1] = null;
                                Destroy(grid[i, j + 2].gameObject);
                                grid[i, j+2] = null;
                                for (int k = j + 3; k < 5; k++)
                                {
                                    if (grid[i, k] != null)
                                    {
                                        Debug.Log("I'm null" + grid[i, k].gameObject);
                                        grid[i, k].gameObject.GetComponent<ParticleBehavior>().added = false;
                                        grid[i, k] = null;
                                    }
                                }
                                switch (tempTag) 
                                {
                                    case "R":
                                        colCount[0]++;
                                        break;
                                    case "G":
                                        colCount[1]++;
                                        break;
                                    case "B":
                                        colCount[2]++;
                                        break;
                                }
                                return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }
    void EndGame()
    {
        gameEnd = true;
        Time.timeScale = 0;
    }
    public void Resume() 
    {
        if (!gameEnd) 
        {
            gamePaused = false;
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            restartBtn.SetActive(false);
        }
    }

    public void SetHUD() 
    {
        upHUD[0].text = destroyDCount.ToString();
        upHUD[2].text = score.ToString();
        
        downHUD[0].text = colCount[0].ToString();
        downHUD[1].text = colCount[1].ToString();
        downHUD[2].text = colCount[2].ToString();
        downHUD[3].text = rgbCount.ToString();

        total = colCount[0] + colCount[1] + colCount[2] + rgbCount;
        upHUD[1].text = total.ToString();

        if (score >= 100)
        {
            notification[0].SetActive(true);
            restartBtn.SetActive(true);
            EndGame();
        }

    }


}
