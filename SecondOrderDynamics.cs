using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;


public class SecondOrderDynamics
{
    private Vector xp;// prvious input
    private Vector y,yd;// state variables
    private float k1, k2, k3;//dynamics constants

    public SecondOrderDynamics(float f, float z, float r, Vector x0)
    {
        //compute constants
        k1 = z / (Mathf.PI * f);
        k2 = 1 / ((2 * Mathf.PI * f) * (2 * Mathf.PI * f));
        k3 = r * z / (2 * Mathf.PI * f);
        // initialize variables
        xp = x0;
        y = x0;
        yd = 0;
    }

    public Vector Update(float T, Vector x, Vector xd=null)
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

}
