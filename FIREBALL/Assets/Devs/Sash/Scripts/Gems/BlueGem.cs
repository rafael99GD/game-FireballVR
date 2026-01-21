using UnityEngine;

public class BlueGem : MonoBehaviour, IGem
{
    [SerializeField] private int shotCount = 8;
    [SerializeField] private float manaCost = 15f; 

    public System.Type GetBehaviorType()
    {
        return typeof(BlueFreezeBehavior);
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