using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CreateDots : MonoBehaviour
{
    public int numbers;
    public GameObject dot;
    public GameObject[,] dots;

    public void Create(Transform[] points)
    {
        dots = new GameObject[points.Length - 1, numbers];

        for (int i = 0; i < points.Length - 1; i++)
        {
            float distanceX = (points[i+1].position.x - points[i].position.x)/numbers;
            float distanceY = (points[i+1].position.y - points[i].position.y)/numbers;
            float distanceZ = (points[i+1].position.z - points[i].position.z)/numbers;

            for (int j = 0; j < numbers; j++)
            {
                GameObject gb = 
                Instantiate(dot, 
                    new Vector3(points[i].position.x + distanceX * j, points[i].position.y + distanceY * j, points[i].position.z + distanceZ * j),
                    Quaternion.identity) as GameObject;
                dots[i, j] = gb; 
            }
        }
        GetComponent<Animate>().Anim(dots);
    }
}
