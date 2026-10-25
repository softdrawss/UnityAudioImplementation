using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class PlayerFootsteps : MonoBehaviour
{
    [Header("Audio Random Containers")]
    public AudioResource defaultContainer;
    public AudioResource grassContainer;
    public AudioResource woodContainer;
    public AudioResource stoneContainer;
    public AudioResource metalContainer;

    [Header("Settings")]
    public float distanceBetweenSteps = 1.8f;
    public float groundDistance = 1.2f;

    private AudioSource audioSource;
    private Vector3 lastPosition;
    private float distanceWalked;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        lastPosition = transform.position;
    }

    void FixedUpdate()
    {
        Vector3 movement = transform.position - lastPosition;
        movement.y = 0f;
        lastPosition = transform.position;

        RaycastHit floor;
        bool onGround = Physics.Raycast(transform.position, Vector3.down, out floor, groundDistance);
        if (onGround == false)
        {
            return;
        }

        distanceWalked += movement.magnitude;
        if (distanceWalked >= distanceBetweenSteps)
        {
            distanceWalked = 0f;
            PlayStep(floor.collider);
        }
    }

    void PlayStep(Collider floor)
    {
        SurfaceType surfaceType = SurfaceType.Default;

        FootstepSurface surface = floor.GetComponentInParent<FootstepSurface>();
        if (surface != null)
        {
            surfaceType = surface.surfaceType;
        }

        AudioResource container = defaultContainer;

        switch (surfaceType)
        {
            case SurfaceType.Grass:
                container = grassContainer;
                break;
            case SurfaceType.Wood:
                container = woodContainer;
                break;
            case SurfaceType.Stone:
                container = stoneContainer;
                break;
            case SurfaceType.Metal:
                container = metalContainer;
                break;
        }

        if (container == null)
        {
            container = defaultContainer;
        }

        if (container == null)
        {
            return;
        }

        audioSource.resource = container;
        audioSource.Play();
    }
}