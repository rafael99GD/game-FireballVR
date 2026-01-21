using System.Collections.Generic;
using UnityEngine;

public class LivesCounter : MonoBehaviour
{
    [SerializeField] private Material matGlow;
    [SerializeField] private Material matOff;

    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private List<GameObject> LifeObjects = new List<GameObject>();

    private void Start()
    {
        changeLivesDisplayed(LifeObjects.Count);

        //Cuando ocurra este evento en el PlayerManager se ejecuta la función changeLivesDisplayed
        playerManager.OnLivesChanged.AddListener(changeLivesDisplayed);
    }

    public void changeLivesDisplayed(int currentLives)
    {
        for (int i = 0; i < LifeObjects.Count; i++)
        {
            if (i < currentLives)
            {
                //LifeObjects[i].SetActive(true);
                LifeObjects[i].GetComponent<Renderer>().material = matGlow;
            }
            else
            {
                //LifeObjects[i].SetActive(false);
                LifeObjects[i].GetComponent<Renderer>().material = matOff;
            }
        }
    }
}
