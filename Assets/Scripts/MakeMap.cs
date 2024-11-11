using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeMap : MonoBehaviour
{
    [SerializeField] private int[] _mapSize;
    [SerializeField] private int _mapSeed;
    [SerializeField] private int[,,] _stage;
    [SerializeField] private int[,,] _stageStatus;
    [SerializeField] public static int generateTime = 0;
    public void GenerateMap(int mapSeed, int[] mapSize, int[,,] stageStatus)
    {
        mapSeed = (mapSeed + generateTime) % 4096;
        generateTime++;
        int bomb = 0;
        _mapSeed = mapSeed;
        MapSize = mapSize;
        StageStatus = stageStatus;
        Random.InitState(_mapSeed);

        //爆弾の設置
        for (int i = 0; i < MapSize[0]; i++)
        {
            for (int j = 0; j < MapSize[1]; j++)
            {
                for (int k = 0; k < MapSize[2]; k++)
                {
                    if ((int)Random.Range(0, 5) == 0)
                    {
                        _stage[i, j, k] = 27;
                        bomb++;
                    }
                    _stageStatus[i, j, k] = 0;
                }
            }
        }
        //グリッド毎の数値の設定
        for (int i = 0; i < MapSize[0]; i++)
        {
            for (int j = 0; j < MapSize[1]; j++)
            {
                for (int k = 0; k < MapSize[2]; k++)
                {
                    if (_stage[i, j, k] != 27)
                    {
                        CheckBomb(i, j, k);
                    }
                }
            }
        }
        FinishGenerate(bomb);
    }
    public void CheckBomb(int i, int j, int k)
    {
        int aroundBombNum = 0;
        for (int a = -1; a <= 1; a++)
        {
            if (i + a < 0 || i + a >= MapSize[0]) continue;
            for (int b = -1; b <= 1; b++)
            {
                if (j + b < 0 || j + b >= MapSize[1]) continue;
                for (int c = -1; c <= 1; c++)
                {
                    if (k + c < 0 || k + c >= MapSize[2]) continue;
                    if (_stage[i + a, j + b, k + c] == 27)
                    {
                        aroundBombNum++;
                    }
                }
            }
        }
        _stage[i, j, k] = aroundBombNum;
    }
    public void FinishGenerate(int bomb)
    {
        GameData.BombNum = bomb;
        GetComponent<GameSettingController>().Stage = Stage;
        GetComponent<GameSettingController>().StageStatus = StageStatus;
    }
    public int[] MapSize
    {
        get { return _mapSize; }
        set { _mapSize = value; }
    }
    public int[,,] Stage
    {
        get { return _stage; }
        set { _stage = value; }
    }
    public int[,,] StageStatus
    {
        get { return _stageStatus; }
        set { _stageStatus = value; }
    }
}
