using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class SecondOrderDynamics : MonoBehaviour
{
    private Vector3 xp;// prvious input
    private Vector3 y, yd;// state variables
    private float k1, k2, k3;//dynamics constants

    public void SecondOrderDynamic(float f, float z, float r, Vector3 x0)
    {
        //compute constants
        k1 = z / (Mathf.PI * f);
        k2 = 1 / ((2 * Mathf.PI * f) * (2 * Mathf.PI * f));
        k3 = r * z / (2 * Mathf.PI * f);
        // initialize variables
        xp = x0;
        y = x0;
        yd = Vector3.zero;
    }

    public void SecondOrderFuntion()
    {

        Vector3 oldPosition= transform.position;


        /*
        if (xd == null)// estimate velocity
        {
            xd = (x - xp) / T;
            xd = (x - xp) / T;
            xp = x;
        }
        y = y + T * yd;
        yd = yd + T * (x + k3 * xd - y - k1 * yd) / k2;
        return y;
        */

    }

    private void Update()
    {
        
    }

    /*
    Update(float T, Vector x, Vector xd = null)
    {
        if (xd == null)// estimate velocity
        {
            xd = (x - xp) / T;
            xd = (x - xp) / T;
            xp = x;
        }
        y = y + T * yd;
        yd = yd + T * (x + k3 * xd - y - k1 * yd) / k2;
        return y;
    }
    */
}
