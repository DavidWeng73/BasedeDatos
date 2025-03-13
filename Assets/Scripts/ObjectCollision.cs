using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Colisión detectada con: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Colisión con pared. Destruyendo objeto...");
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Colisión con coche. Terminando juego...");
            CarController car = collision.gameObject.GetComponent<CarController>();
            if (car != null)
            {
                Debug.Log("Llamando a OnGameOver()...");
                car.OnGameOver();
            }
            else
            {
                Debug.LogError(" No se encontró el script CarController en el coche.");
            }
        }
    }
}
