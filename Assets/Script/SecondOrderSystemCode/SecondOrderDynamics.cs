using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class SecondOrderDynamics : MonoBehaviour
{
    private SecondOrderState _state;

    [SerializeField]
    internal SecondOrderSettings settings;

    /// <summary>
    /// A default second order dynamics system that has some sane default values to produce eased movement.
    /// </summary>
    public static SecondOrderDynamics Default => new();

    private Vector3 xp;// prvious input
    private Vector3 y, yd;// state variables
    public float k1, k2, k3;//dynamics constants


    /// <summary>
    /// Resets the system to the original initial value.
    /// </summary>
    public void Reset() => ResetTemporaryIv(_state.InitialValue);

    /// <summary>
    /// Resets the system to the given initial value, and optionally
    /// the initial velocity.
    /// </summary>
    /// <param name="newIv"></param>
    /// <param name="newVelocity"></param>
    public void Reset(float newIv, float newVelocity = default)
    {
        _state.InitialValue = newIv;
        ResetTemporaryIv(_state.InitialValue, newVelocity);
    }


    internal void ResetTemporaryIv(float iv, float newYd = default)
    {
        _state.PreviousTargetValue = iv;
        _state.CurrentValue = iv;
        _state.LastStableValue = iv;
        _state.CurrentVelocity = newYd;
        _state.DeltaTime = 0;
        _state.ElapsedTime = 0;
        ResampleUserInputs();
    }

    private void ResampleUserInputs()
    {
        _state.F = settings.SampleF(in _state);
        _state.Z = settings.SampleZ(in _state);
        _state.R = settings.SampleR(in _state);
    }

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
