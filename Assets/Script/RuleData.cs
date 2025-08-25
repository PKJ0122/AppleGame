using UnityEngine;

[CreateAssetMenu(fileName = "RuleData",menuName = "ScriptableObject/RuleData")]
public class RuleData : ScriptableObject
{
    public Sprite[] ruleSprites;
    public string[] ruleDescription;
}