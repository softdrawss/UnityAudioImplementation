using UnityEngine;
using Unity.Mathematics;
using UnityEngine.Splines;

public class AmbientZoneSpline : MonoBehaviour
{
    [Tooltip("Spline Path to follow")]
    public SplineContainer Spline;

    [Tooltip("Character to track")]
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

        if (!Spline.Spline.Closed)
            return;

        Vector3 sub = transform.position - Player.position;
        Vector3 splineRight = Vector3.Cross(Vector3.up, tangent);

        if (Vector3.Dot(sub, splineRight) < 0f)
        {
            transform.position = Player.position + new Vector3(0f, 1f, 0f);
            transform.rotation = Player.rotation;
        }
    }
}