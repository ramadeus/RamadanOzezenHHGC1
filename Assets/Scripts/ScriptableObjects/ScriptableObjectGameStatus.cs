using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Status/Game Status Scriptable Obj")]
public class ScriptableObjectGameStatus: ScriptableObject
{
    // ıı
    public PlayerStat[] players; 
    [Serializable]
   public class PlayerStat
    {
        public int playerId;
        public string playerName;
        public int collectedWood;
    }
}
