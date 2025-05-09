using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Core
{
    public class UniqueId : ScriptableObject
    {
    [ReadOnly] [HorizontalGroup("ID"), OnInspectorInit("CheckGenerate")]
    public string uniqueId = "";
    [ReadOnly] public ushort uniqueShortId = 0;

    public string GetUniqueId => uniqueId;
    public ushort GetUniqueShortID => uniqueShortId;
    
#if UNITY_EDITOR
    public void CheckGenerate() { //OnInspectorInit
		string[] guids = AssetDatabase.FindAssets("t:"+ nameof(UniqueId),new[] {"Assets/ScriptableData"});

      if(NeedNewId(guids))
        GenerateID();

      HashSet<ushort> usedShotID = GetUsedShortID(guids);
			if(uniqueShortId==0 || NeedShortId(guids)){
				GenerateShortID(usedShotID);
			}
    }

    public bool NeedNewId(string[] guids) {
      UniqueId checkId;
      for(int i =0;i<guids.Length;i++){
        string path = AssetDatabase.GUIDToAssetPath(guids[i]);
        checkId = AssetDatabase.LoadAssetAtPath<UniqueId>(path);
        if (checkId.uniqueId == uniqueId && checkId != this) {
          return true;
        }
      }
      return false;
    }

    
    [HorizontalGroup("ID")]
    [Button(ButtonStyle.Box), LabelText("Generate"), LabelWidth(15)]
    public void GenerateID() {
      uniqueId = Guid.NewGuid().ToString();
      EditorUtility.SetDirty(this);
    }

		public void GenerateShortID(HashSet<ushort> usedIDs){
      ushort newID = 1;
      while (usedIDs.Contains(newID))
      {
        newID++;
      }

      uniqueShortId = newID;
      EditorUtility.SetDirty(this);
    }

		private bool NeedShortId(string [] guids){
      UniqueId checkId;
      for(int i =0;i<guids.Length;i++){
        string path = AssetDatabase.GUIDToAssetPath(guids[i]);
        checkId = AssetDatabase.LoadAssetAtPath<UniqueId>(path);
        if(checkId.uniqueShortId == uniqueShortId && checkId != this) return true;
      }
      return false;
		}

    public HashSet<ushort> GetUsedShortID(string [] guids) {
      HashSet<ushort> usedIDs = new HashSet<ushort>();
      UniqueId checkId;
      for(int i =0;i<guids.Length;i++){
        string path = AssetDatabase.GUIDToAssetPath(guids[i]);
        checkId = AssetDatabase.LoadAssetAtPath<UniqueId>(path);
        usedIDs.Add(checkId.uniqueShortId);
      }
      return usedIDs;
    }

#endif
    public string GetId() {
      return uniqueId;
    }
    }
}
