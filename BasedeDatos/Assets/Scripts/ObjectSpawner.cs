using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab;
    public Transform Point1;
    public Transform Point2;
    public Transform Point3;
    public float spawnInterval = 2f;
    public float speed = 5f;

    void Start()
    {
        StartCoroutine(SpawnObjects());
    }
    IEnumerator SpawnObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            int randomPoint = Random.Range(0, 3);
            Transform spawnPoint = Point1;

            if (randomPoint == 0)
            {
                spawnPoint = Point1;
            }
            else if (randomPoint == 1)
            {
                spawnPoint = Point2;
            }
            else if (randomPoint == 2)
            {
                spawnPoint = Point3;
            }

            GameObject newObject = Instantiate(objectPrefab, spawnPoint.position, Quaternion.identity);

            StartCoroutine(MoveObjectDown(newObject));
        }
    }
    IEnumerator MoveObjectDown(GameObject obj)
    {
        while (obj.transform.position.y > -5f) 
        {
            obj.transform.Translate(Vector2.down * speed * Time.deltaTime);
            yield return null;
        }
        Destroy(obj);
    }
}

public class ObjectCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject); 
        }
    }
}
