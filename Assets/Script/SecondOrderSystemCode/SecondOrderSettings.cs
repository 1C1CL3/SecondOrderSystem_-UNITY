using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondOrderSettings : MonoBehaviour
{

    public SamplingMode inputSampleMode;

    #region Seralized Fields

    [Tooltip("f: The natural frequency of the system, in Hz. If the system isn't dampened, this is the frequency " +
             "it will oscillate at.")]
    public ValueRange frequency;
    [Tooltip("ец: The damping coefficient. Affects how quickly the system settles on the target value. 0 = 'undamped', " +
             "between 0 and 1, 'underdamped'. 1 = 'critical damping' (e.g.: Unity's SmoothDamp())")]
    public ValueRange damping;
    [Tooltip("r: The initial responsiveness of the system. With a r of 0, the system will take some time to move towards the target. " +
             "A value of 1 will cause it to react immediately. A value greater than 1 will cause overshoot, " +
             "while a negative value will cause an undershoot before moving towards the target. " +
             "The r of a typical mechanical system is 2.")]
    public ValueRange responsiveness;

    [Tooltip("Controls if the system should reset itself to it's last reset state and initial value, should the system fail.")]
    public bool unstableAutoReset;

    [Tooltip("A multiplier for the time step passed to the system. Values lower than 1 will cause slower movement, higher faster.")]
    public ValueRange deltaTimeScale;

    #endregion

    private float Sample(ref ValueRange rangeToSample, in SecondOrderState state)
    {
        // switch out of fixed mode.
        if (inputSampleMode != SamplingMode.Fixed && rangeToSample.forceFixedValue)
        {
            rangeToSample.forceFixedValue = false;
        }

        return inputSampleMode switch
        {
            SamplingMode.None => 0,
            SamplingMode.Fixed => rangeToSample.ForceFixed(),
            SamplingMode.Minimum => rangeToSample.Minimum(),
            SamplingMode.Midpoint => rangeToSample.Midpoint(),
            SamplingMode.Maximum => rangeToSample.Maximum(),
            SamplingMode.Random when !state._isDeserializing => rangeToSample.Random(),
            SamplingMode.Random => rangeToSample.Midpoint(), // using random during deserialization makes unity sad
            //SamplingMode.LerpByStepTime => rangeToSample.Sample(state.DeltaTime),
            //SamplingMode.LerpByScaledTime => rangeToSample.Sample(state.ElapsedTime / deltaTimeScale),
            _ => throw new ArgumentOutOfRangeException(),
        };
    }

    internal float SampleF(in SecondOrderState state)
        => Sample(ref frequency, in state);

    internal float SampleZ(in SecondOrderState state)
        => Sample(ref damping, in state);

    internal float SampleR(in SecondOrderState state)
        => Sample(ref responsiveness, in state);

    internal float SampleDeltaTimeScale(in SecondOrderState state)
        => Sample(ref deltaTimeScale, in state);
}
