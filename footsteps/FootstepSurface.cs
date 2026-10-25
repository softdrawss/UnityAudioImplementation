using UnityEngine;

public enum SurfaceType
{
    Default,
    Grass,
    Wood,
    Stone,
    Metal
}

public class FootstepSurface : MonoBehaviour
{
    public SurfaceType surfaceType = SurfaceType.Default;
}