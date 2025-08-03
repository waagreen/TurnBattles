using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityPool", menuName = "Combat/Abilities/AbilityPool")]
public class AbilityPool : ScriptableObject
{
    public List<Ability> abilities;
}
