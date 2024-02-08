using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace BloodMoon.Editor
{
    public class MultiRenameTool : OdinEditorWindow
    {
        [MenuItem("Tools/Blood Moon/Multiple Asset Rename")]
        static void OpenWindow()
        {
            GetWindow<MultiRenameTool>().Show();
        }

        void Update()
        {
            Redraw();
        }

        bool mCurrentlyRenaming;

        [HorizontalGroup("Prefix", 15f), ToggleLeft, LabelText("Prefix")] public bool PrefixEnabled = true;
        [HorizontalGroup("Prefix", 150f), EnableIf("PrefixEnabled"), HideLabel] public string Prefix = "MAT_";
        [HorizontalGroup("Suffix", 15f), ToggleLeft, LabelText("Suffix")] public bool SuffixEnabled = false;
        [HorizontalGroup("Suffix", 150f), EnableIf("SuffixEnabled"), HideLabel] public string Suffix = "_D";
        [HorizontalGroup("ChangeFormat", 150f), ToggleLeft, LabelText("Change Format")] public bool ChangeFormatEnabled = false;
        [HorizontalGroup("ChangeFormat", 130.0f), EnableIf("ChangeFormatEnabled"), HideLabel] public Format NextFormat;

        public string DelimCharacters = "_";
        
        [Tooltip("Separate multiple removals with commas")]
        [HorizontalGroup("RemoveText", 150f), ToggleLeft, LabelText("Remove Text")] public bool RemoveTextEnabled = false;
        [HorizontalGroup("RemoveText", 130.0f), EnableIf("RemoveTextEnabled"), HideLabel] public string TextToRemove;
        
        [TableList(AlwaysExpanded = true, ShowPaging = false, HideToolbar = true, ShowIndexLabels = false, IsReadOnly = true)]
        public List<RenamePreview> RenamePreviews;

        [Button(ButtonSizes.Gigantic)]
        void RenameAll()
        {
            mCurrentlyRenaming = true;
            if (Selection.assetGUIDs.Length != Selection.objects.Length)
            {
                EditorUtility.DisplayDialog("Deselect Hierarchy Assets",
                    "You have assets selected that are not Project files. Please deselect them to continue.", "Ok");
            }
            else if(EditorUtility.DisplayDialog("Rename Assets?",
                   "Are you sure you want to rename these assets? This cannot be undone.", 
                   "Rename", 
                   "Cancel"))
            {

                string[] paths = new string[Selection.objects.Length];
                string[] names = new string[Selection.objects.Length];

                for (var i = 0; i < Selection.objects.Length; i++)
                {
                    var obj = Selection.objects[i];
                    Debug.Log(obj.name);
                    names[i] = obj.name;
                    paths[i] = AssetDatabase.GetAssetPath(obj);
                }

                for (int i = 0; i < paths.Length; i++)
                {
                    string newName = ModifyName(names[i]);
                    string path = paths[i];
                    AssetDatabase.RenameAsset(path, newName);
                }
                
                AssetDatabase.Refresh();
            }

            mCurrentlyRenaming = false;
        }
        
        void Redraw()
        {
            if (mCurrentlyRenaming) return;
            if (RenamePreviews == null)
            {
                RenamePreviews = new List<RenamePreview>();
            }
            
            RenamePreviews.SetLength(Selection.objects.Length);

            if (Selection.assetGUIDs.Length > 0)
            {
                for (var i = 0; i < Selection.objects.Length; i++)
                {
                    var selection = Selection.objects[i];
                    RenamePreviews[i] = new RenamePreview()
                    {
                        Current = selection.name,
                        AfterChange = ModifyName(selection.name)
                    };
                }
            }
        }

        string ModifyName(string input)
        {

            if (ChangeFormatEnabled)
            {
                bool isDelimited = false;
                
                char[] delimCharArray = DelimCharacters.ToCharArray();
                foreach(var c in delimCharArray)
                {
                    if (input.Contains(c))
                    {
                        isDelimited = true;
                        break;
                    }
                }
                string[] separated = isDelimited ? input.Split(delimCharArray) : input.SplitPascalCase().Split(' ');
                string newInput = "";
                
                for (int i = 0; i < separated.Length; i++)
                {
                    if(separated[i].Length<1) continue;
                    switch (NextFormat)
                    {
                        case Format.AllLower:
                            newInput += separated[i].ToLowerInvariant() + "_";
                            break;
                        case Format.AllUpper:
                            newInput += separated[i].ToUpperInvariant() + "_";
                            break;
                        case Format.CamelCase:
                            if (i == 0)
                                newInput += separated[i].ToLowerInvariant();
                            else
                                newInput += separated[i][0].ToString().ToUpperInvariant() + separated[i].Substring(1);
                            break;
                        case Format.PascalCase:
                            newInput += separated[i][0].ToString().ToUpperInvariant() + separated[i].Substring(1);
                            break;
                    }     
                }
                input = newInput;
            }

            if (RemoveTextEnabled && TextToRemove != null && TextToRemove.Length>0)
            {
                string[] textsToRemove = TextToRemove.Split(',');

                foreach (string text in textsToRemove)
                {
                    if(text.Length < 1) continue;
                    while (input.Contains(text, StringComparison.InvariantCultureIgnoreCase))
                    {
                        int index = input.LastIndexOf(text, StringComparison.InvariantCultureIgnoreCase);
                        input = input.Remove(index, text.Length);
                    }
                }
            }
            
            string output = (PrefixEnabled ? Prefix : "") + input + (SuffixEnabled ? Suffix : "");
            return output;
        }
        
        [Serializable]
        public struct RenamePreview
        {
            [ReadOnly] public string Current;
            [ReadOnly] public string AfterChange;
        }

        public enum Format
        {
            CamelCase,
            PascalCase,
            AllLower,
            AllUpper
        }
        
    }
}