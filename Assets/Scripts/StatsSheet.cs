using UnityEngine;

[CreateAssetMenu(fileName = "StatsSheet", menuName = "Combat/StatsSheet")]
public class StatsSheet : ScriptableObject
{
    public int hitPoints = 3;
    public int strength = 1;
    public int stamina = 1;
    public int inteligence = 1;
    public int agility = 1;
}
