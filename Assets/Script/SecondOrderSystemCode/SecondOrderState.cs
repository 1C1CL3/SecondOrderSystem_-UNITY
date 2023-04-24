internal struct SecondOrderState
{
    //user input
    public float F;
    public float Z;
    public float R;
    public float InitialValue;
    public float TargetValue;
    public float PreviousTargetValue;

    // system input
    public float DeltaTime;
    public float ElapsedTime;

    // system output
    public float CurrentValue; // at t0: intial value
    public float CurrentVelocity; // first derivative of iv
    public float LastStableValue;

    // function constants calculated from f, z, r
    public float K1;
    public float K2;
    public float K3;

    //calculated constants for pole zero matching
    public float W;
    public float D;

    //the maximum time step for any one solve iteration
    public float MaximumTimeStep;

    // meta about the current state of system as a whole
    internal bool _isDeserializing;

}
