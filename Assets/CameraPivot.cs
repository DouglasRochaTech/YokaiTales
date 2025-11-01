using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    float VelocidadeRotacaoCamera = 80;
    public Transform JogadorT;

    void Update()
    {
        if (JogadorT)
        {
            transform.position = Vector3.Lerp(transform.position, JogadorT.position, Time.deltaTime * 3);
        }

        transform.Rotate(0, Input.GetAxis("HorizontalRight") * VelocidadeRotacaoCamera * Time.deltaTime, 0);
    }
}
