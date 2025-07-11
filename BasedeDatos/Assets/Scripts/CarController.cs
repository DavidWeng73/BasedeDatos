using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public GameObject Car;
    private int IndexPoint = 1;  
    public Transform Point1;
    public Transform Point2;
    public Transform Point3;

    public float speed = 5f;  

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.D))
        {
            CambiarPuntoDerecha();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            CambiarPuntoIzquierda();
        }

        MoverCoche();
    }


    void CambiarPuntoDerecha()
    {
        if (IndexPoint < 2) 
        {
            IndexPoint++;
        }
    }

    void CambiarPuntoIzquierda()
    {
        if (IndexPoint > 0) 
        {
            IndexPoint -- ;
        }
    }

    void MoverCoche()
    {
        Transform targetPoint = null;

        if (IndexPoint == 0)
        {
            targetPoint = Point1;
        }
        else if (IndexPoint == 1)
        {
            targetPoint = Point2;
        }
        else if (IndexPoint == 2)
        {
            targetPoint = Point3;
        }

        if (targetPoint != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);
        }
    }
}
