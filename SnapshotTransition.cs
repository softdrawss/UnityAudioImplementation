using UnityEngine;
using UnityEngine.Audio;

public class SnapshotTransition : MonoBehaviour
{

    public AudioMixerSnapshot pausedSnapshot;
    public AudioMixerSnapshot unpausedSnapshot;

    public float timeToReach = 1f;
    
    private bool paused = false;

    public void ToggleSnapshot()
    {
        paused = !paused;
        if (paused)
        {
            pausedSnapshot.TransitionTo(timeToReach);
        }
        else
        {
            unpausedSnapshot.TransitionTo(timeToReach);
        }
    }
}
