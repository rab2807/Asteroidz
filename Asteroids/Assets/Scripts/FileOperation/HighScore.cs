using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEngine;

public static class HighScore
{
    private static List<Tuple<string, int>> scoreChart = new List<Tuple<string, int>>();
    private static bool dataLoaded;
    private const string FileName = "HighScore.txt";

    public static bool DataLoaded => dataLoaded;
    public static List<Tuple<string, int>> ScoreChart => scoreChart;

    public static void ReadData()
    {
        dataLoaded = true;
        StreamReader input = null;
        try
        {
            input = File.OpenText(Path.Combine(Application.streamingAssetsPath, FileName));

            string line = input.ReadLine();
            while (line != null)
            {
                string[] s = line.Split(',');
                scoreChart.Add(new Tuple<string, int>(s[0], int.Parse(s[1])));
                line = input.ReadLine();
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.StackTrace);
        }
        finally
        {
            if (input != null) input.Close();
        }
    }

    private static void WriteData()
    {
        StreamWriter output = null;
        try
        {
            output = File.CreateText(Path.Combine(Application.streamingAssetsPath, FileName));

            foreach (var entry in scoreChart)
            {
                if (entry == null) break;
                output.WriteLine(entry.Item1 + "," + entry.Item2);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.StackTrace);
        }
        finally
        {
            if (output != null) output.Close();
        }
    }

    public static void UpdateData(string name, int score)
    {
        if (scoreChart.Count == 10)
        {
            if (scoreChart[9].Item2 > score) return;
            scoreChart[9] = new Tuple<string, int>(name, score);
        }

        scoreChart.Add(new Tuple<string, int>(name, score));
        scoreChart.Sort((i1, i2) => i2.Item2.CompareTo(i1.Item2));
        WriteData();
    }
}