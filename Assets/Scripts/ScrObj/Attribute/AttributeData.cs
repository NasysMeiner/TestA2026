using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Attribute/AttributeData")]
public class AttributeData : ScriptableObject
{
    public List<AttributeParameters> AttributeParameters = new();
}