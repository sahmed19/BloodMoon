using BloodMoon.Serialization;
using UnityEngine;


[CreateAssetMenu(fileName = "CurveLibrary", menuName = "BloodMoon/CurveLibrary")]
public class CurveLibrary : ALibrary<CurveLibrary>
{
    [SerializeField] private AnimationCurve inOutQuadratic;
    [SerializeField] private AnimationCurve inQuadratic;
    [SerializeField] private AnimationCurve outQuadratic;

    public static AnimationCurve InOutQuadratic => GetInstance().inOutQuadratic;
    public static AnimationCurve InQuadratic => GetInstance().inQuadratic;
    public static AnimationCurve OutQuadratic => GetInstance().outQuadratic;
}