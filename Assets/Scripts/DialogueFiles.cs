using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DialogueFiles", order = 1)]
public class DialogueFiles : ScriptableObject
{
    public string[] sentences;
    public string[] heads;
    public string[] buttonTexts;


}
