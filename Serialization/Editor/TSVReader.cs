using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace BloodMoon.Serialization.Editor
{
    public static class TSVReader
    {
        public static List<string[]> TSVData { get; private set; }

        public static bool TryLoadTSV(string path, int length)
        {
            if(TSVData==null)
                TSVData = new List<string[]>();
            else
                TSVData.Clear();
            
            int longLength = 0;
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    // Read and display lines from the file until the end of the file is reached.
                    
                    while ((line = sr.ReadLine()) != null)
                    {
                        if(line.Length<1)continue;
                        
                        string[] splitLine = line.Split('\t', length);

                        TSVData.Add(splitLine);
                        longLength = Math.Max(longLength, splitLine.Length);
                    }
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Debug.LogError("The file could not be read: " + e.Message);
                return false;
            }

            return true;
        }
    }
}



