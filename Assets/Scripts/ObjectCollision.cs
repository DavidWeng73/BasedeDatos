using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisi�n detectada con: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Colisi�n con pared. Destruyendo objeto...");
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colisi�n con coche. Terminando juego...");
            CarController car = collision.gameObject.GetComponent<CarController>();
            if (car != null)
            {
                Debug.Log("Llamando a OnGameOver()...");
                car.OnGameOver();
            }
            else
            {
                Debug.LogError(" No se encontr� el script CarController en el coche.");
            }
        }
    }
}
