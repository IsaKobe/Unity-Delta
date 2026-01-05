
using System;
using System.Security;

[Serializable]
public class SaveData
{
    public LevelScore[] data;
    public int[] itemLevels;
    public int money;


    public SaveData()
    {
           
    }


    public SaveData(int levelCount, int itemLength)
    {
        data = new LevelScore[levelCount];
        itemLevels = new int[itemLength];
    }

    public void SetData(int levelIndex, LevelScore score)
    {
        if(data[levelIndex].score < score.score)
        {
            data[levelIndex] = score;
        }
    }

    public LevelScore GetScore(int levelIndex)
    {
        return data[levelIndex];
    }

    public void EnforceLevelLength(int levelCount, int itemLength)
    {
        data = RecreateArray(levelCount, data);
        itemLevels = RecreateArray(itemLength, itemLevels);
    }

    T[] RecreateArray<T>(int count, T[] array)
    {
        if (array.Length != count)
        {
            var a = new T[count];
            for (int i = 0; i < array.Length && i < count; i++)
            {
                a[i] = array[i];
            }
            return a;
        }
        return array;
    }
}

[Serializable]
public struct LevelScore
{
    public int score;

    public LevelScore(int _score)
    {
        score = _score;
    }
}
