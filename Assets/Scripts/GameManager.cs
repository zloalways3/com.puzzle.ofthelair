using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public List<int> tiles = new List<int>();
    public int nulltileid;

    public List<int> tilesWin = new List<int>();

    public GameObject prefab;
    public GameObject prefab1;
    public GameObject Panel;

    public float time;
    public int moves;
    public bool playnow;

    public List<GameObject> ach;
    public List<int> achs2;
    public List<int> achs4;
    public List<int> achs6;

    public List<int> achsCounts;

    public int Level;
    public int levelmax;

    public List<GameObject> panels;

    public List<Sprite> achspr;
    public Image achimg;

    public TextMeshProUGUI Timetxt;
    public TextMeshProUGUI Movestxt;
    public TextMeshProUGUI Time1txt;
    public TextMeshProUGUI Moves1txt;

    public Text txtach2;
    public Text txtach4;
    public Text txtach6;

    public Slider sl1;
    public Slider sl2;

    public AudioSource music;
    public AudioSource sounds;

    public bool whenOpen;

    private void Start()
    {
        Invoke("GameOpen", UnityEngine.Random.Range(2, 5));
        Load();

        GenerateTiles();
        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i] != -1)
            {
                GameObject gm = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity, Panel.transform);
                gm.GetComponent<Tile>().gm = this;
                gm.GetComponent<Tile>().id = i;
                gm.GetComponent<Tile>().number = tiles[i];
            }
            else
            {
                GameObject gm = Instantiate(prefab1, new Vector3(0, 0, 0), Quaternion.identity, Panel.transform);
            }
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt("level", Level);
        PlayerPrefs.SetInt("levelmax", levelmax);
        PlayerPrefs.SetFloat("music", music.volume);
        PlayerPrefs.SetFloat("sounds", sounds.volume);
        for (int i = 0; i < achs2.Count; i++)
        {
            PlayerPrefs.SetInt("ach2" + i, achs2[i]);
        }
        for (int i = 0; i < achs4.Count; i++)
        {
            PlayerPrefs.SetInt("ach4" + i, achs4[i]);
        }
        for (int i = 0; i < achs6.Count; i++)
        {
            PlayerPrefs.SetInt("ach6" + i, achs6[i]);
        }
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("level"))
        {
            Level = PlayerPrefs.GetInt("level");
            levelmax = PlayerPrefs.GetInt("levelmax");
            music.volume = PlayerPrefs.GetFloat("music");
            sounds.volume = PlayerPrefs.GetFloat("sounds");

            achs2.Clear();
            for(int i = 0;i < 16;i++) 
            {
                achs2.Add(PlayerPrefs.GetInt("ach2" + i));
            }
            achs4.Clear();
            for (int i = 0; i < 16; i++)
            {
                achs4.Add(PlayerPrefs.GetInt("ach4" + i));
            }
            achs6.Clear();
            for (int i = 0; i < 16; i++)
            {
                achs6.Add(PlayerPrefs.GetInt("ach6" + i));
            }
        }
    }

    public void GameOpen()
    {
        OpenPanel(1);
    }

    public void SaveOptions()
    {
        music.volume = sl2.value;
        sounds.volume = sl1.value;
    }

    public void OpenOptions(bool g)
    {
        whenOpen = g;
        playnow = false;
        OpenPanel(5);
        sl1.value = sounds.volume;
        sl2.value = music.volume;
    }

    public void ExitOptions()
    {
        if (whenOpen) 
        {
            OpenPanel(0);
            playnow = true;
        }
        else 
        {
            OpenPanel(1);
        }
    }

    private void Update()
    {
        if(playnow) 
        {
            time += Time.deltaTime;
            List<bool> bools = new List<bool>();
            for(int i = 0; i < tiles.Count; i++) 
            {
                if (tiles[i] == tilesWin[i])
                {
                    bools.Add(true);
                }
                else
                {
                    bools.Add (false);
                }
            }
            if (bools[0]  && bools[2] && bools[3] && bools[4] && bools[5] && bools[6] && bools[7] && bools[8] && bools[9] && bools[10] && bools[11] && bools[12] && bools[13] && bools[14] && bools[15])
            {
                Win();
            }
        }

        Timetxt.text = $"TIME:\n{Mathf.FloorToInt(time / 60):D2}:{Mathf.FloorToInt(time % 60):D2}";
        Movestxt.text = "MOVES:\n" + moves;
        Time1txt.text = $"TIME:\n{Mathf.FloorToInt(time / 60):D2}:{Mathf.FloorToInt(time % 60):D2}";
        Moves1txt.text = "MOVES:\n" + moves;

        txtach2.text = "" + achsCounts[0];
        txtach4.text = "" + achsCounts[1];
        txtach6.text = "" + achsCounts[2];

        if (time <= 120)
        {
            ach[0].SetActive(true);
            ach[1].SetActive(true);
            ach[2].SetActive(true);
            achimg.sprite = achspr[0];
            achimg.gameObject.SetActive(true);
        }
        else if (time <= 240)
        {
            ach[0].SetActive(false);
            ach[1].SetActive(true);
            ach[2].SetActive(true);
            achimg.sprite = achspr[1];
            achimg.gameObject.SetActive(true);
        }
        else if (time <= 360)
        {
            ach[0].SetActive(false);
            ach[1].SetActive(false);
            ach[2].SetActive(true);
            achimg.sprite = achspr[2];
            achimg.gameObject.SetActive(true);
        }
        else
        {
            ach[0].SetActive(false);
            ach[1].SetActive(false);
            ach[2].SetActive(false);
            achimg.gameObject.SetActive(false);
        }

        Save();
    }

    public void OpenAchs()
    {
        CountAch();
        OpenPanel(4);
    }

    public void CountAch()
    {
        achsCounts.Clear();
        int a = 0;
        foreach (int i in achs2) 
        {
            if(i == 1)
            {
                a++;
            }
        }
        achsCounts.Add(a);

        a = 0;
        foreach (int i in achs4)
        {
            if (i == 1)
            {
                a++;
            }
        }
        achsCounts.Add(a);
        a = 0;
        foreach (int i in achs6)
        {
            if (i == 1)
            {
                a++;
            }
        }
        achsCounts.Add(a);
    }

    public void NextLevel()
    {
        if(Level != 23)
        {
            Level++;
        }
        StartGame();
    }

    public void Win()
    {
        playnow = false;
        if(time <= 120)
        {
            achs2[Level] = 1;
            achs4[Level] = 1;
            achs6[Level] = 1;
        }
        else if (time <= 240)
        {
            
            achs4[Level] = 1;
            achs6[Level] = 1;
        }
        else if (time <= 360)
        {
            
            achs6[Level] = 1;
        }
        OpenPanel(3);
        if(Level == levelmax && levelmax != 23)
        {
            levelmax++;
        }
    }

    public void ChangeLevel(int lvl)
    {
        if(lvl <= levelmax)
        {
            Level = lvl;
        }
    }

    public void OpenPanel(int id)
    {
        foreach(GameObject go in panels) 
        {
            go.SetActive(false);        
        }
        panels[id].SetActive(true);
    }

    public void StartGame()
    {
        time = 0;
        moves = 0;
        GenerateTiles();
        while(tiles == tilesWin)
        {
            if (tiles == tilesWin)
            {
                GenerateTiles();
            }
        }
        TilesUpdate();
        OpenPanel(0);
        playnow = true;
    }

    public void GenerateTiles()
    {
        tiles.Clear();
        for(int c = 0; c < 16; c++)
        {
            int a = UnityEngine.Random.Range(0, 16);
            while (true)
            {
                bool b = true;
                foreach (int i in tiles)
                {
                    if (i == a)
                    {
                        b = false;
                    }

                }
                if (b == false)
                {
                    a = UnityEngine.Random.Range(0, 16);
                }
                else
                {
                    if(a == 0)
                    {
                        nulltileid = tiles.Count;
                    }
                    tiles.Add(a);
                    break;
                }
            }
        }
    }

    public void TilesUpdate()
    {
        for(int i = 0; Panel.transform.childCount > i; i++) 
        {
            Color c = Panel.transform.GetChild(i).gameObject.GetComponent<Image>().color;
            Panel.transform.GetChild(i).GetChild(0).gameObject.SetActive(true);
            Panel.transform.GetChild(i).gameObject.GetComponent<Tile>().id = i;
            Panel.transform.GetChild(i).gameObject.GetComponent<Tile>().number = tiles[i];
            Panel.transform.GetChild(i).gameObject.GetComponent<Image>().color = new Color(c.r, c.g, c.b, 1);
            if (tiles[i] == 0)
            {
                
                Panel.transform.GetChild(i).gameObject.GetComponent<Image>().color = new Color(c.r, c.g, c.b, 0);
                Panel.transform.GetChild(i).GetChild(0).gameObject.SetActive(false);
            }
        }

        //for (int i = 0; i < tiles.Count; i++) 
        //{
        //   if (tiles[i] != 0)
        //    {
        //        GameObject gm = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity, Panel.transform);
        //        gm.GetComponent<Tile>().gm = this;
        //        gm.GetComponent<Tile>().id = i;
        //        gm.GetComponent<Tile>().number = tiles[i];
        //    }
        //    else
        //    {
        //        GameObject gm = Instantiate(prefab1, new Vector3(0, 0, 0), Quaternion.identity, Panel.transform);
        //    }
        //}
    }


    public void TileMove(int id)
    {
        if (CheckTiled(id))
        {
            tiles[nulltileid] = tiles[id];
            tiles[id] = 0;
            nulltileid = id;
            moves++;
            sounds.Play();
            TilesUpdate();
        }

    }

    private bool CheckTiled(int id)
    {
        if(id - 1 == nulltileid && (nulltileid + 1) % 4 != 0)
        {
            return true;
        }
        else if(id + 1 == nulltileid && nulltileid % 4 != 0 && nulltileid != 0)
        {
            return true;
        }
        else if (id - 4 == nulltileid && nulltileid < 12)
        {
            return true;
        }
        else if (id + 4 == nulltileid && nulltileid > 3)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
