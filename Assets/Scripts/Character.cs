using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Combat/Character")]
public class Character : ScriptableObject
{
    public int level = 1;
    public StatsSheet stats;
    public AbilityPool abilities;
}
