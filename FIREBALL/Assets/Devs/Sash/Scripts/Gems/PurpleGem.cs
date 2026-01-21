using UnityEngine;

public class PurpleGem : MonoBehaviour, IGem
{
    [SerializeField] private int shotCount = 3;
    [SerializeField] private float manaCost = 33f;

    public System.Type GetBehaviorType()
    {
        return typeof(PurpleGravityBehavior);
    }

    public int GetShotCount()
    {
        return shotCount;
    }

    public float GetManaCost()
    {
        return manaCost;
    }
}