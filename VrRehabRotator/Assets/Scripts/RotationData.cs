using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Threading;


public struct Angle
{
    public float X;
    public float Y;
    public float Z;

    public Angle(float x, float y, float z)
    {
        this.X = x;
        this.Y = y;
        this.Z = z;
    }

    public override string ToString()
    {
        return string.Format("{0},{1},{2}", X, Y, Z);
    }

}


public class RotationData
{
    /// <summary>
    /// Used to signal when rotations are ready
    /// </summary>
    public Action RotationsReady;

    public List<Angle> Rotations { get; private set; }

    /// <summary>
    /// Loads rotations from provided file name. If the file doesn't
    /// exists, it will first be initialized with random rotations
    /// </summary>
    /// <param name="fileName"></param>
    public void LoadRotations(string fileName)
    {
        var th = new Thread(GetRotations);
        th.IsBackground = true;
        th.Start(Path.Combine(Application.dataPath, fileName));
    }

    private void GetRotations(object obj)
    {
        string fileName = obj.ToString();

        if (!File.Exists(fileName))
        {
            // Write and return random rotations
            Rotations = WriteRandomRotations(fileName, 100);
        }
        else
        {
            // Read from file
            Rotations = ReadRotations(fileName);
        }

        // Signal complete
        if (RotationsReady != null)
            RotationsReady();
    }


    private List<Angle> WriteRandomRotations(string fileName, int count)
    {
        List<Angle> randomRotations = GenerateRandomRotations(count);

        using (StreamWriter sw = new StreamWriter(fileName))
        {
            foreach (Angle angle in randomRotations)
            {
                // Write as CSV x,y,z                
                sw.WriteLine(angle);
            }
        }

        return randomRotations;
    }

    private List<Angle> GenerateRandomRotations(int count)
    {
        List<Angle> rotations = new List<Angle>();


        System.Random rand = new System.Random();
        for (int i = 0; i < count; i++)
        {          
            rotations.Add(new Angle(rand.Next(0, 359), rand.Next(0, 359), rand.Next(0, 359)));
        }

        return rotations;
    }

    private List<Angle> ReadRotations(string fileName)
    {
        List<Angle> rotations = new List<Angle>();

        if (File.Exists(fileName))
        {
            using (StreamReader sr = File.OpenText(fileName))
            {
                while (!sr.EndOfStream)
                {
                    string[] angles = sr.ReadLine().Split(',');

                    if (angles.Length == 3)
                    {
                        float x = 0;
                        float y = 0;
                        float z = 0;

                        float.TryParse(angles[0], out x);
                        float.TryParse(angles[1], out y);
                        float.TryParse(angles[2], out z);

                        rotations.Add(new Angle(x, y, z));
                    }
                }
            }
        }

        return rotations;
    }
  
}
