using UnityEngine;

public class RedGem : MonoBehaviour, IGem
{
    [SerializeField] private int shotCount = 10;
    [SerializeField] private float manaCost = 10f; 

    public System.Type GetBehaviorType()
    {
        return typeof(RedBounceBehavior);
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