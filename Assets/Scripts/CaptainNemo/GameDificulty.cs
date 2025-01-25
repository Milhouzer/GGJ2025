using UnityEngine;

[CreateAssetMenu(fileName = "GameDificulty", menuName = "Game/GameDificulty")]
public class GameDificulty : ScriptableObject
{
    public float timeToChangeDificulty = default;
    public float timeToTriggerEvent = default;
    public float dificultyVariationParameter = default;
    public float weightBadBubble = default;
    public float bubbleSpawnXSeoncds = default;
    public float weightedIncrementBadBubble = default;
}
