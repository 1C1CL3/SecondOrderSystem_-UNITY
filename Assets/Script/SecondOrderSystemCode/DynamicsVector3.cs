using System.Drawing;
using UnityEngine;

public class DynamicsVector3 : MonoBehaviour
{

    public SecondOrderDynamics dynamicsX = SecondOrderDynamics.Default;
    public SecondOrderDynamics dynamicsY = SecondOrderDynamics.Default;
    public SecondOrderDynamics dynamicsZ = SecondOrderDynamics.Default;

    public bool modifyX = true;
    public bool modifyY = true;
    public bool modifyZ = true;

    public Vector3 Value => _value;

    protected Vector3 _value;

    public void Reset(Vector3 resetValue)
    {
        _value = resetValue;
        dynamicsX.Reset(_value.x);
        dynamicsY.Reset(_value.y);
        dynamicsZ.Reset(_value.z);
    }





}
