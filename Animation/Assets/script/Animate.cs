using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    public float timeMax;
    public GameObject[] obj;
    public bool start;

    public int index;
    public float actualtime;
    public void Anim(GameObject[,] array)
    {
        obj = new GameObject[array.GetLength(0) * array.GetLength(1)];

        int smallIndex = 0;

        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                obj[smallIndex] = array[i, j];
                smallIndex++;
            }
        }
        start = true;
    }
    void Update()
    {
        if (start && index != obj.Length)
        {
            if (actualtime <= 0)
            {
                actualtime = timeMax;
                grow(obj[index]);
                index++;
            }
            else
            {
                actualtime -= Time.deltaTime;
            }
        }
    }


    private void grow(GameObject obj)
    {
        //obj.transform.localScale *= 2;
        obj.GetComponent<StartParticles>().particles();
    }
}
