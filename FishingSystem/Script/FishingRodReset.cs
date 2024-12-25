using System.Collections.Generic;
using UnityEngine;

public class FishingRodReset : MonoBehaviour
{
    public List<HingeJoint> ropeJoints;

    public void ResetRope()
    {
        foreach (HingeJoint joint in ropeJoints)
        {
            JointMotor motor = joint.motor;
            motor.targetVelocity = -50f; // Ýpi geri çekmek için negatif bir hýz veriyoruz
            motor.force = 100f;
            joint.motor = motor;
            joint.useMotor = true; // Motoru aktif hale getir
        }
    }
}
