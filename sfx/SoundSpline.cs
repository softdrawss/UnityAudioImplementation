using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Splines;

public class SoundSpline : MonoBehaviour
{
    public SplineContainer Spline;
    public Transform Player;

    void FixedUpdate()
    {
        Vector3 localPlayerPos = Spline.transform.InverseTransformPoint(Player.position);

        SplineUtility.GetNearestPoint(Spline.Spline, localPlayerPos, out float3 nearestPointLocal, out float normalizedT);

        Vector3 nearestWorldPos = Spline.transform.TransformPoint(nearestPointLocal);

        transform.position = nearestWorldPos;
        Vector3 tangent = Spline.transform.TransformDirection(Spline.Spline.EvaluateTangent(normalizedT));
        if (tangent != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(tangent);
    }
}