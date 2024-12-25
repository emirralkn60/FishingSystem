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
            motor.targetVelocity = -50f; // �pi geri �ekmek i�in negatif bir h�z veriyoruz
            motor.force = 100f;
            joint.motor = motor;
            joint.useMotor = true; // Motoru aktif hale getir
        }
    }
}
