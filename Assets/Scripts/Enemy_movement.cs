using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_movement : MonoBehaviour
{
    public float speed = 5f;
    public float up = 0f; //up y down sonlos limites
    public float down = 10f;
    private Vector3 palante = Vector3.forward;//vector para moverse
    private Vector3 rota = new Vector3(0, 180, 0);//vector para rotar
    void Update()
    {
        transform.Translate(palante * speed * Time.deltaTime);//siempre va para alante
        if (transform.position.z >= up)//si llega a x coordenada o mas
        {
            transform.Rotate(rota);//Rotara 180
        }
        if (transform.position.z <= down)//si llega a y coordenada o mas
        {
            transform.Rotate(rota);//Rotara 180
        }
    }
}
