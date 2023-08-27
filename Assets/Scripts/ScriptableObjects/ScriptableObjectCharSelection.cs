using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Char Selection/Char Selection Scriptable Obj")]
public class ScriptableObjectCharSelection: ScriptableObject
{
    // ıı
    public List<CharType> selectedChars= new List<CharType>();
}
