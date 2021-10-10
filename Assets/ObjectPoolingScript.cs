using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingScript : MonoBehaviour
{
    public GameObject enemy;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawner());
    }
    
    IEnumerator spawner()
	{
        spawnearEnemy();
        yield return new WaitForSeconds(10f);
        StartCoroutine(spawner());
	}

    void spawnearEnemy()
	{
        GameObject newEnemy = Instantiate(enemy);
        Destroy(newEnemy, 30);
    }
}
