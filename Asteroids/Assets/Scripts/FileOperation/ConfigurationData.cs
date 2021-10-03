using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;

public class ConfigurationData
{
    #region Fields

    private static ConfigurationData data;
    private const string FileName = "ConfigurationData.txt";

    private float shipForceMagnitude;
    private float shipRotationSpeed;
    private float bulletSpeed;
    private float rockMinForceMagnitude;
    private float rockMaxForceMagnitude;
    private float rockMinTorque;
    private float rockMaxTorque;
    private float minRockSpawnTime;
    private float maxRockSpawnTime;
    private float particleFadeSpeed;

    private int bulletPoolCapacity;
    private int rockPoolCapacity;
    private int maxBigRockOnScreen;

    #endregion

    #region Properties

    public float ShipForceMagnitude => shipForceMagnitude;

    public float ShipRotationSpeed => shipRotationSpeed;

    public float BulletSpeed => bulletSpeed;

    public float RockMinForceMagnitude => rockMinForceMagnitude;

    public float RockMaxForceMagnitude => rockMaxForceMagnitude;

    public float RockMinTorque => rockMinTorque;

    public float RockMaxTorque => rockMaxTorque;

    public int BulletPoolCapacity => bulletPoolCapacity;

    public int RockPoolCapacity => rockPoolCapacity;

    public int MAXBigRockOnScreen => maxBigRockOnScreen;

    public float MinRockSpawnTime => minRockSpawnTime;

    public float MaxRockSpawnTime => maxRockSpawnTime;

    public float ParticleFadeSpeed => particleFadeSpeed;

    #endregion

    #region Methods

    private ConfigurationData()
    {
        StreamReader input = null;
        try
        {
            input = File.OpenText(Path.Combine(Application.streamingAssetsPath, FileName));

            var values = new List<string[]>();
            string line = input.ReadLine();
            while (line != null)
            {
                string[] s = line.Split(' ');
                values.Add(s);
                line = input.ReadLine();
            }

            SetData(values);
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

    private void SetData(List<string[]> entry)
    {
        shipForceMagnitude = float.Parse(entry[0][1]);
        shipRotationSpeed = float.Parse(entry[1][1]);
        bulletSpeed = float.Parse(entry[2][1]);
        rockMinForceMagnitude = float.Parse(entry[3][1]);
        rockMaxForceMagnitude = float.Parse(entry[4][1]);
        rockMinTorque = float.Parse(entry[5][1]);
        rockMaxTorque = float.Parse(entry[6][1]);
        bulletPoolCapacity = int.Parse(entry[7][1]);
        rockPoolCapacity = int.Parse(entry[8][1]);
        maxBigRockOnScreen = int.Parse(entry[9][1]);
        minRockSpawnTime = float.Parse(entry[10][1]);
        maxRockSpawnTime = float.Parse(entry[11][1]);
        particleFadeSpeed = float.Parse(entry[12][1]);
    }

    public static ConfigurationData GetData()
    {
        // return only one instance (singleton)
        return data ?? (data = new ConfigurationData());
    }

    #endregion
}