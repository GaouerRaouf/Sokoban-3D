using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

   
    
    [SerializeField] private GameObject wall;
    [SerializeField] private GameObject floor;
    [SerializeField] private GameObject box;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject ArrivalPoint;
    [SerializeField] private GameObject ArrivalTrigger;
    [SerializeField] private GameObject youWin;
    [SerializeField] private GameObject menu;
    private int level = 0;

    public static GameManager Instance;

    private void Start()
    {
        Instance = this;
    }

    void Update()
    {
        CheckWin();
    }

    void CreateLevel(int[,] level)
    {
        int i = 0,j=0;
        for (i = 0; i <= level.GetUpperBound(0); i++)
        {
            for (j = 0; j <= level.GetUpperBound(1); j++)
            {
                GetBlock(level[i, j], i ,j);
            }
        }
    }
    void DestroyLevel(int[,] level)
    {
        GameObject[] Boxes = GameObject.FindGameObjectsWithTag("Box");
        GameObject[] Walls = GameObject.FindGameObjectsWithTag("Wall");
        GameObject[] Blocks = GameObject.FindGameObjectsWithTag("Block");
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        foreach(GameObject Block in Blocks)
        {
            Destroy(Block);
        }
        foreach (GameObject Wall in Walls)
        {
            Destroy(Wall);
        }
        foreach (GameObject Box in Boxes)
        {
            Destroy(Box);
        }

    }
    void GetBlock(int block, int x, int z)
    {
        
        switch (block)
        {
            case 4:
                var wallChild = Instantiate(wall, new Vector3(x, 1f, z), wall.transform.rotation);
                wallChild.transform.parent = gameObject.transform;
                var floorChild = Instantiate(floor, new Vector3(x, 0f, z), floor.transform.rotation);
                floorChild.transform.parent = gameObject.transform;
                break;
            case 2:
                var boxChild = Instantiate(box, new Vector3(x, 1f, z), box.transform.rotation);
                boxChild.transform.parent = gameObject.transform;
                floorChild = Instantiate(floor, new Vector3(x, 0f, z), floor.transform.rotation);
                floorChild.transform.parent = gameObject.transform;
                break;
            case 0:
                floorChild = Instantiate(floor, new Vector3(x, 0f, z), floor.transform.rotation);
                floorChild.transform.parent = gameObject.transform;
                break;
            case 1:
                var ArrivalTriggerChild =  Instantiate(ArrivalTrigger, new Vector3(x, 1f, z), floor.transform.rotation);
                ArrivalTriggerChild.transform.parent = gameObject.transform;
                var ArrivalPointChild = Instantiate(ArrivalPoint, new Vector3(x, 0f, z), floor.transform.rotation);
                ArrivalPointChild.transform.parent = gameObject.transform;
                break;
            case 3:
                Instantiate(player, new Vector3(x, 1f, z), wall.transform.rotation);
                floorChild = Instantiate(floor, new Vector3(x, 0f, z), floor.transform.rotation);
                floorChild.transform.parent = gameObject.transform;
                break;
        }
    }
    bool CheckWin()
    {
        WinningTrigger[] WinningScript = FindObjectsOfType<WinningTrigger>();

        int len = WinningScript.Length, i = 0, checkedTrigger = 0;
        if (len == 0) return false;
        for (i = 0; i < len; i++)
        {
            if (WinningScript[i].triggerChecked == true) checkedTrigger++;
        }
        if (checkedTrigger == len)
        {
            if (level == Levels.levels.Count-1)
            {
                DestroyLevel(Levels.levels[level]);
                menu.gameObject.SetActive(true);
            }
            else
            {
                DestroyLevel(Levels.levels[level]);
                youWin.gameObject.SetActive(true);
            }
            return true;
        }
        return false;
    }
    public void RestartLevel()
    {
        DestroyLevel(Levels.levels[level]);
        youWin.SetActive(false);
        CreateLevel(Levels.levels[level]);
    }
    public void NextLevel()
    {
        DestroyLevel(Levels.levels[level]);
        youWin.SetActive(false);
        level++;
        CreateLevel(Levels.levels[level]);
    }
    public void StartGame()
    {
        menu.gameObject.SetActive(false);
        DestroyLevel(Levels.levels[level]);
        level = 0;
        CreateLevel(Levels.levels[level]);
    }

    

}
