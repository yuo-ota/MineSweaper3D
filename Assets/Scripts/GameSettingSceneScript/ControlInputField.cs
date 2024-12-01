using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.Text.RegularExpressions;

public class ControlInputField : MonoBehaviour
{
    [SerializeField] private GameObject _gameSettingControllerObject;
    [Header("data")]
    [SerializeField] private int[] _mapSize;
    [SerializeField] private int _mapSeed;
    [SerializeField] private int[,,] _stageStatus;
    [SerializeField] private int _useHintNum;
    [SerializeField] private int _timer;
    [SerializeField] private int _gameStatus;

    private void Start()
    {
        GetComponent<TMP_InputField>().onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(string text)
    {
        if (text.Length == 0)
        {
            _gameSettingControllerObject.GetComponent<GameSettingController>().DisplayCodeStatus(0);
            return;
        }

        _gameSettingControllerObject.GetComponent<GameSettingController>().DisplayCodeStatus(2);
        _gameSettingControllerObject.GetComponent<GameSettingController>().IsUseCode = false;
        ResetAnyField();
        
        // option kind data
        if (text == "") return;
        int optionKind = CheckOptionKind(text.Substring(0, 1));
        text = text.Substring(1);
        if (optionKind == -1) return;

        // bourd data
        if (text.Length < 7) return;
        int[] bourdData = CheckBourdData(text);
        text = text.Substring(7);
        if (bourdData == null) return;
        MapSize = new int[] { bourdData[0], bourdData[1], bourdData[2]};
        MapSeed = bourdData[3];

        // progress data
        if (optionKind / 4 == 1)
        {
            optionKind %= 4;
            var tupleData = CheckProgressData(text, bourdData);

            // mapStatusの確認
            int[,,] progressData = tupleData.mapStatus;
            if (progressData == null) return;
            StageStatus = progressData;
            // usedHintNumの確認
            int usedHintNum = tupleData.usedHintNum;
            UseHintNum = usedHintNum;

            // 文字列の切り出し
            text = text[(text.IndexOf('g') + 1)..];
        }
        else
        {
            int[,,] progressData = new int[bourdData[0], bourdData[1], bourdData[2]];
            StageStatus = progressData;
            UseHintNum = 0;
        }

        // timer data
        if (optionKind / 2 == 1)
        {
            optionKind %= 2;
            int timerData = CheckTimerData(text);
            if (timerData == -1) return;
            Timer = timerData;
        }
        else
        {
            Timer = 0;
        }

        // status data
        if (optionKind == 1) {
            int statusData = CheckStatusData(text);
            if (statusData == -1) return;
            GameStatus = statusData;
        }
        _gameSettingControllerObject.GetComponent<GameSettingController>().DisplayCodeStatus(1);
        _gameSettingControllerObject.GetComponent<GameSettingController>().IsUseCode = true;
    }

    public void ResetAnyField() 
    {
        UseHintNum = 0;
        Timer = 0;
        GameStatus = 0;
    }

    public int CheckOptionKind(string s)
    {
        if (int.TryParse(s, out int result))
        {
            if (result < 0 || result > 7)
            {
                result = -1;
            }
        }
        else
        {
            result = -1;
        }
        return result;
    }

    public int[] CheckBourdData(string s)
    {
        int[] result = new int[4];

        char[] mapSizeData = s.Substring(0, 4).ToCharArray();
        char[] mapSeedData = Convert.ToInt32(s.Substring(4, 3), 16).ToString("X").ToCharArray();

        int parity = 0;
        for (int i = 0; i < 3; i++) {
            result[i] = ConvertNumber(mapSizeData[i]);
            parity += result[i];
        }
        if (parity != ConvertNumber(mapSizeData[3])) return null;

        Array.Reverse(mapSeedData);
        result[3] = Convert.ToInt32(new string(mapSeedData), 16);
        return result;
    }

    public (int[,,] mapStatus, int usedHintNum) CheckProgressData(string s, int[] bourdData)
    {
        char[] inputText = s.ToCharArray();
        int[,,] result = new int[bourdData[0], bourdData[1], bourdData[2]];
        Queue<int> statusNumbers = new Queue<int>();

        // 入力が正しくない場合
        if (!s.Contains('Z') || !s.Contains('g')) return (null, 0);

        // ヒント数のデータの取得
        String usedHintString = s[(s.IndexOf('Z') + 1)..(s.IndexOf('g'))];
        int usedHintNum = 0;

        // usedHintNumのint変換
        if (int.TryParse(usedHintString, out int number)) usedHintNum = number;
        else return (null, 0);

        // RLEのデコード
        String mapStatusString = RLEDecorder(s.Substring(0, s.IndexOf('Z')));

        // 1セット完成している場合の処理
        for (int i = 0; i < mapStatusString.Length / 4; i++) {
            int[] status = new int[6];
            int parityCount = 0;
            // 文字列の切り出しとアップデート
            String nowStringPart = mapStatusString[(4*i)..(4*(i + 1))];

            for (int j = 0; j < 3; j++)
            {
                int target = ConvertNumber(nowStringPart[j]);
                statusNumbers.Enqueue(target % 5);
                statusNumbers.Enqueue(target / 5);

                parityCount += target / 5 + target % 5;
            }

            // パリティと合致しない場合エラーとしてreturn
            if (parityCount != ConvertNumber(nowStringPart[3]))
            {
                return (null, 0);
            }
        }
        // セットからあふれた場合の処理
        String remainStringPart = mapStatusString[(4 * (mapStatusString.Length / 4))..];
        for (int i = 0; i < remainStringPart.Length; i++)
        {
            int target = ConvertNumber(remainStringPart[i]);
            statusNumbers.Enqueue(target % 5);
            statusNumbers.Enqueue(target / 5);
        }

        // Queueから取り出してresultに代入
        for (int i = 0; i < bourdData[0]; i++)
        {
            for (int j = 0; j < bourdData[1]; j++)
            {
                for (int k = 0; k < bourdData[2]; k++)
                {
                    result[i, j, k] = statusNumbers.Dequeue();
                }
            }
        }
        return (result, usedHintNum);
    }

    public int CheckTimerData(String s)
    {
        char[] result;

        var matches = Regex.Matches(s, @"(\d+)");
        foreach (Match match in matches)
        {
            result = match.Groups[1].Value.ToCharArray(); // 数字部分
            Array.Reverse(result);
            return int.Parse(new String(result));
        }
        return -1;
    }

    public int CheckStatusData(String s)
    {
        char status = s[^1];

        return status switch
        {
            'a' => 0,
            'b' => 1,
            'c' => 2,
            _ => -1,
        };
    }

    public int ConvertNumber(char c)
    {
        return c - 'A';
    }

    public String RLEDecorder(String s)
    {
        String result = "";

        var matches = Regex.Matches(s, @"(\d*)([A-Za-z])");
        foreach (Match match in matches)
        {
            int count = string.IsNullOrEmpty(match.Groups[1].Value) ? 1 : int.Parse(match.Groups[1].Value); // 数字部分
            char character = match.Groups[2].Value[0];   // アルファベット部分
            result += new string(character, count);    // 指定回数アルファベットを繰り返す
        }

        return result;
    }

    public int[] MapSize
    {
        get { return _mapSize; }
        set { _mapSize = value; }
    }

    public int MapSeed
    {
        get { return _mapSeed; }
        set { _mapSeed = value; }
    }

    public int[,,] StageStatus
    {
        get { return _stageStatus; }
        set { _stageStatus = value; }
    }

    public int UseHintNum
    {
        get { return _useHintNum; }
        set { _useHintNum = value; }
    }

    public int Timer
    {
        get { return _timer; }
        set { _timer = value; }
    }

    public int GameStatus
    {
        get { return _gameStatus; }
        set { _gameStatus = value; }
    }

}