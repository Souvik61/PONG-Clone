using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public struct ShareData {
        public uint difficulty;
        public uint highestScore;
        public ShareData(uint diff,uint highSc)
        {
            difficulty = diff;
            highestScore = highSc;
        }
    }

    public static GameData instance;

    ShareData currentData;

    void Awake()
    {
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public ShareData GetSharedData()
    {
        return currentData;
    }

    public void SetSharedData(ShareData data)
    {
        this.currentData = data;
    }
}
