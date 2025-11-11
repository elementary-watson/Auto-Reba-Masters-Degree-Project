using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class REBA_TableABC
{
    public int[,,] tableA = new int[,,]
    {
        {
            { 1, 2, 3, 4 },
            { 2, 3, 4, 5 },
            { 2, 4, 5, 6 },
            { 3, 5, 6, 7 },
            { 4, 6, 7, 8 }
        },
        {
            { 1, 2, 3, 4 },
            { 3, 4, 5, 6 },
            { 4, 5, 6, 7 },
            { 5, 6, 7, 8 },
            { 6, 7, 8, 9 }
        },
        {
            { 3, 3, 5, 6 },
            { 4, 5, 6, 7 },
            { 5, 6, 7, 8 },
            { 6, 7, 8, 9 },
            { 7, 8, 9, 9 }
        }
    };

    public int[,,] tableB = new int[,,]
    {
        {
            { 1, 2, 2 },
            { 1, 2, 3 },
            { 3, 4, 5 },
            { 4, 5, 5 },
            { 6, 7, 8 },
            { 7, 8, 8 }
        },
        {
            { 1, 2, 3 },
            { 2, 3, 4 },
            { 4, 5, 5 },
            { 5, 6, 7 },
            { 7, 8, 8 },
            { 8, 9, 9 }
        }
    };

    public int[,] tableC = new int[,]
    {
        { 1, 1, 1, 2, 3, 3, 4, 5, 6, 7, 7, 7 },
        { 1, 2, 2, 3, 4, 4, 5, 6, 6, 7, 7, 8 },
        { 2, 3, 3, 3, 4, 5, 6, 7, 7, 8, 8, 8 },
        { 3, 4, 4, 4, 5, 6, 7, 8, 8, 9, 9, 9 },
        { 4, 4, 4, 5, 6, 7, 8, 8, 9, 9, 9, 9 },
        { 6, 6, 6, 7, 8, 8, 9, 9, 10, 10, 10, 10 },
        { 7, 7, 7, 8, 9, 9, 9, 10, 10, 11, 11, 11 },
        { 8, 8, 8, 9, 10, 10, 10, 10, 10, 11, 11, 11 },
        { 9, 9, 9, 10, 10, 10, 11, 11, 11, 12, 12, 12 },
        { 10, 10, 10, 11, 11, 11, 11, 12, 12, 12, 12, 12 },
        { 11, 11, 11, 11, 12, 12, 12, 12, 12, 12, 12, 12 },
        { 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12, 12 }
    };

    // Table A

    public int Step1_getNeckScore(int flexTolerance, int neckFlexAngle)
    {
        // For flexion
        if (neckFlexAngle < 0)
        {
            if (neckFlexAngle > -flexTolerance)
            {
                return 1; // Ignore mild flexion within the tolerance
            }
            else if (neckFlexAngle <= -flexTolerance && neckFlexAngle > -20)
            {
                return 1;
            }
            else if (neckFlexAngle <= -20)
            {
                return 2;
            }
        }
        // For extension
        else if (neckFlexAngle > 0)
        {
            if (neckFlexAngle < flexTolerance)
            {
                return 1; // Ignore mild extension within the tolerance
            }
            else
            {
                return 2;
            }
        }
        return 1; // Default case for no flexion or extension
    }

    public int Step1a_twisted_getNeckScore(int twistTolerance, int neckTwistAngle)
    {
        // If the absolute value of the twist angle is within the tolerance
        if (Mathf.Abs(neckTwistAngle) <= twistTolerance)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    public int Step1a_sidebend_getNeckScore(int adductionTolerance, int neckAddAngle)
    {
        // If the absolute value of the adduction angle is within the tolerance
        if (Mathf.Abs(neckAddAngle) <= adductionTolerance)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    public int Step2_getTrunkScore(int flexTolerance, int trunkFlexAngle)
    {
        // For flexion
        if (trunkFlexAngle < 0)
        {
            if (trunkFlexAngle > -flexTolerance)
            {
                return 1; // Ignore mild flexion within the tolerance
            }
            else if (trunkFlexAngle <= -flexTolerance && trunkFlexAngle > -20)
            {
                return 2;
            }
            else if (trunkFlexAngle <= -20 && trunkFlexAngle > -60)
            {
                return 3;
            }
            else if (trunkFlexAngle <= -60)
            {
                return 4;
            }
        }
        // For extension
        else if (trunkFlexAngle > 0)
        {
            if (trunkFlexAngle < flexTolerance)
            {
                return 1; // Ignore mild extension within the tolerance
            }
            else if (trunkFlexAngle >= flexTolerance && trunkFlexAngle <= 20)
            {
                return 2;
            }
            else if (trunkFlexAngle > 20)
            {
                return 3;
            }
        }
        return 1; // Default case for no flexion or extension
    }

    public int Step2a_twisted_getTrunkScore(int twistTolerance, int trunkTwistAngle)
    {
        // If the absolute value of the twist angle is within the tolerance
        if (Mathf.Abs(trunkTwistAngle) <= twistTolerance)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    public int Step2a_sidebend_getTrunkScore(int adductionTolerance, int trunkAddAngle)
    {
        // If the absolute value of the adduction angle is within the tolerance
        if (Mathf.Abs(trunkAddAngle) <= adductionTolerance)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    public int Step3_getLegScore(int flexTolerance, int legFlexAngle)
    {
        // For flexion
        if (legFlexAngle < 0)
        {
            if (legFlexAngle > -flexTolerance)
            {
                return 0; // Ignore mild flexion within the tolerance
            }
            else if (legFlexAngle <= -flexTolerance && legFlexAngle > -30)
            {
                return 1; // No score for flexion between tolerance and -30
            }
            else if (legFlexAngle <= -30 && legFlexAngle > -60)
            {
                return 1;
            }
            else if (legFlexAngle <= -60)
            {
                return 2;
            }
        }
        return 1;
    }

    public int Step3a_isLegBilateralOrUnilateral(bool isLeftFootTouching, bool isRightFootTouching)
    {
        if (isLeftFootTouching && isRightFootTouching == true)
            return 1;
        else
            return 2;
    }

    // Table B
    public int Step7_getUpperArmScore(int flexTolerance, int upperArmFlexAngle)
    {
        // For flexion
        if (upperArmFlexAngle < 0)
        {
            if (upperArmFlexAngle > -flexTolerance)
            {
                return 1; // Ignore mild flexion within the tolerance
            }
            else if (upperArmFlexAngle <= -flexTolerance && upperArmFlexAngle > -20)
            {
                return 1;
            }
            else if (upperArmFlexAngle <= -20 && upperArmFlexAngle > -45)
            {
                return 2;
            }
            else if (upperArmFlexAngle <= -45 && upperArmFlexAngle > -90)
            {
                return 3;
            }
            else if (upperArmFlexAngle <= -90)
            {
                return 4;
            }
        }
        // For extension
        else if (upperArmFlexAngle > 0)
        {
            if (upperArmFlexAngle < flexTolerance)
            {
                return 1; // Ignore mild extension within the tolerance
            }
            else if (upperArmFlexAngle >= flexTolerance && upperArmFlexAngle <= 20)
            {
                return 1;
            }
            else if (upperArmFlexAngle > 20)
            {
                return 3; // +2 from the base score of 1 for extension beyond 20 degrees.
            }
        }
        return 1; // Default case for no flexion or extension
    }
    public int Step7b_sidebend_getUpperArmScore(int upperArmAngleAdduction, int adductionTolerance)
    {
        if (Mathf.Abs(upperArmAngleAdduction) <= adductionTolerance)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }
    public int Step7b_isShoulderRaised(bool l_isShoulderRaised, bool r_isShoulderRaised)
    {
        // Assuming the method returns a score based on whether the shoulder is raised or not.
        return (l_isShoulderRaised || r_isShoulderRaised) ? 1 : 0;
    }

    public int Step7b_isLeaning()
    {
        // Placeholder for future implementation.
        return 0; // Default value for now.
    }

    public int Step8_getLowerArmScore(int flexTolerance, int lowerArmFlexAngle)
    {
        // For flexion
        if (lowerArmFlexAngle < 0)
        {
            if (lowerArmFlexAngle > -flexTolerance)
            {
                return 1; // Within the tolerance
            }
            else if (lowerArmFlexAngle <= -flexTolerance && lowerArmFlexAngle > -60)
            {
                return 2;
            }
            else if (lowerArmFlexAngle <= -60 && lowerArmFlexAngle > -100)
            {
                return 1;
            }
            else if (lowerArmFlexAngle <= -100)
            {
                return 2;
            }
        }
        return 1; // Default case for no flexion
    }

    public int Step9_getWristScore(int flexTolerance, int wristFlexAngle)
    {
        // For flexion
        if (wristFlexAngle < 0)
        {
            if (wristFlexAngle > -flexTolerance)
            {
                return 1; // Within the tolerance
            }
            else if (wristFlexAngle <= -flexTolerance && wristFlexAngle > -15)
            {
                return 1;
            }
            else if (wristFlexAngle <= -15)
            {
                return 2;
            }
        }
        // For extension
        else if (wristFlexAngle > 0)
        {
            if (wristFlexAngle < flexTolerance)
            {
                return 1; // Within the tolerance
            }
            else if (wristFlexAngle >= flexTolerance && wristFlexAngle <= 15)
            {
                return 1;
            }
            else if (wristFlexAngle > 15)
            {
                return 2;
            }
        }
        return 1; // Default case for no flexion or extension
    }

    public int Step9a_twisted_getWristScore(int twistTolerance, int wristTwistAngle)
    {
        // If the absolute value of the twist angle is within the tolerance
        if (Mathf.Abs(wristTwistAngle) <= twistTolerance)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    public int Step9a_sidebend_getWristScore(int adductionTolerance, int wristAddAngle)
    {
        // If the absolute value of the adduction angle is within the tolerance
        if (Mathf.Abs(wristAddAngle) <= adductionTolerance)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }

    public int Step13a_isBodyPartStatic()
    {
        return 0;
    }
    public int Step13a_isRepeatingSmallRangeAction()
    {
        return 0;
    }
    public int Step13a_isRapidlyChanging(bool isRapidlyChanging)
    {
        return isRapidlyChanging ? 1 : 0;
    }
}
