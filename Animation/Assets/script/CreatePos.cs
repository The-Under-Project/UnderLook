using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreatePos : MonoBehaviour
{
    public Transform posA;
    public Transform posB;
    public GameObject point;
    public int numbers;
    public int coef;
    public bool pattern;

    public GameObject[] pos;

    void Start()
    {
        pos = new GameObject[numbers + 3];
        Vector2 center = new Vector3((posA.position.x + posB.position.x) / 2, (posA.position.y + posB.position.y) / 2, (posA.position.z + posB.position.z) / 2);
        float diameter = Vector3.Distance(posA.position, posB.position);
        if (diameter < 0)
            diameter *= -1;
        float rayon = diameter / 2;

        float distanceZ = (posB.position.z - posA.position.z) / numbers;
        if (distanceZ < 0 && pattern)
            distanceZ *= -1;

        int index = 0;

        special(index, 0);
        index++;
        special(index, 0.25f);
        index++;

        for (int i = 1; i < numbers / 2; i++)
        {
            float iteration = i * distanceZ - rayon;  //rayon - ((i * rayon) / (i + 1));

            float Xn = Mathf.Sqrt(rayon * rayon - iteration * iteration );
            if (float.IsNaN(Xn))
                Xn = 0;
            GameObject g =
            Instantiate(point,
                new Vector3(posA.position.x + Xn*coef, posA.position.y, posA.position.z + distanceZ * i),
                Quaternion.identity) as GameObject;
            pos[index] = g;
            index++;
        }

        for (int i = 0; i < (numbers / 2); i++)
        {
            float iteration = i * distanceZ;  //rayon - ((i * rayon) / (i + 1));


            float Xn = Mathf.Sqrt(rayon * rayon - iteration * iteration);
            if (float.IsNaN(Xn))
                Xn = 0;
            GameObject g =
            Instantiate(point,
                new Vector3(posA.position.x + Xn*coef, posA.position.y, posA.position.z + distanceZ * i + rayon),
                Quaternion.identity) as GameObject;
            pos[index] = g;
            index++;
        }
        special(index, numbers - 0.25f);
        index++;
        special(index, numbers);

        Transform[] position = new Transform[pos.Length];
        for (int i = 0; i < pos.Length; i++)
        {
            position[i] = pos[i].transform;

        }

        


        //GetComponentInParent<CreateLine>().Line(position);
        GetComponentInParent<CreateDots>().Create(position);
    }
    void special(int index, float i)
    {
        Vector2 center = new Vector3((posA.position.x + posB.position.x) / 2, (posA.position.y + posB.position.y) / 2, (posA.position.z + posB.position.z) / 2);
        float diameter = Vector3.Distance(posA.position, posB.position);
        float rayon = diameter / 2;

        float distanceZ = (posB.position.z - posA.position.z) / numbers;
        if (distanceZ < 0 )
            distanceZ *= -1;
        float iteration = i * distanceZ - rayon;  //rayon - ((i * rayon) / (i + 1));

        float Xn = Mathf.Sqrt(rayon * rayon - iteration * iteration);
        if (float.IsNaN(Xn))
            Xn = 0;
        GameObject g =
        Instantiate(point,
            new Vector3(posA.position.x + Xn * coef, posA.position.y, posA.position.z + distanceZ * i),
            Quaternion.identity) as GameObject;
        pos[index] = g;
    }
}
