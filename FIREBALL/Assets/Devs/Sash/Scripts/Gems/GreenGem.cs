using UnityEngine;

public class GreenGem : MonoBehaviour, IGem
{
    [SerializeField] private int shotCount = 6;
    [SerializeField] private float manaCost = 20f;

    public System.Type GetBehaviorType()
    {
        return typeof(GreenBehavior);
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