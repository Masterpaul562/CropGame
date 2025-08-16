using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    public static Game_Manager Instance;

    public int wheatSeedCount;
    public Vector3[] plantLocation;
    public string[] plantType;
    public GrowState[] growthState;
    public bool hasStarted;
    public int arrayNum = 0;
    public int money;
    public bool boughtWateringcan;
    public bool boughtShovel;
    public bool boughtWeedspray;
    public int amountWeedspray;
    public int cornSeedCount;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        plantLocation = new Vector3[36];
        plantType = new string[36];
        growthState = new GrowState[36];
    }
}
