using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace BloodMoon.Serialization
{
    public static class TSVReader
    {
        public static List<string[]> TSVData { get; private set; }

        public static bool TryLoadTSVResources(string resourcesPath, int length = -1, char split = '\t')
        {
            if (TSVData == null)
                TSVData = new List<string[]>();
            else
                TSVData.Clear();

            TextAsset textAsset = Resources.Load<TextAsset>(resourcesPath);
            if (textAsset == null)
            {
                throw new Exception($"Could not find file at path [{resourcesPath}]");
            }


            int longLength = 0;


            string textAssetString = textAsset.text;
            string[] splitLines = textAssetString.Split("\n"[0]);


            for (int l = 0; l < splitLines.Length; l++)
            {
                string line = splitLines[l].Trim();
                if (line.Length < 1) continue;

                string[] splitLine = length == -1 ? line.Split(split) : line.Split(split, length);
                TSVData.Add(splitLine);
                longLength = Math.Max(longLength, splitLine.Length);
            }

            try
            {

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



