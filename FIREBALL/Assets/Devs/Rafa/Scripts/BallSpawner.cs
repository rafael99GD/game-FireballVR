using System.Collections;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject iceBall;
    public Transform spawnZone;


    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SpawnIceBall();
        }
    }

    public void SpawnIceBall()
    {
        float minX = spawnZone.position.x - ((spawnZone.localScale.x * 10)/2);
        float maxX = spawnZone.position.x + ((spawnZone.localScale.x * 10) / 2);
        float minZ = spawnZone.position.z - ((spawnZone.localScale.z * 10) / 2);
        float maxZ = spawnZone.position.z + ((spawnZone.localScale.z * 10) / 2);

        float spawnPointX = Random.Range(minX, maxX);
        float spawnPointY = 8;
        float spawnPointZ = Random.Range(minZ, maxZ);

        Vector3 spawnPosition = new Vector3(spawnPointX, spawnPointY, spawnPointZ);

        Instantiate(iceBall, spawnPosition, Quaternion.identity);
    }

}
