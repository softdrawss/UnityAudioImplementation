using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private string exposedParameter = "MasterVolume";

    private Slider slider;
    private AudioMixer audioMixer;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        slider.minValue = 0f;
        slider.maxValue = 1f;
    }

    private void Start()
    {
        if (AudioManager.Instance != null) audioMixer = AudioManager.Instance.Mixer;
        if (audioMixer == null)
            Debug.LogWarning("VolumeSlider: no Audio Mixer found. Add an AudioManager to the scene and assign the Audio Mixer on it.");

        float savedVolume = PlayerPrefs.GetFloat(exposedParameter + "_Volume", 1f);
        slider.SetValueWithoutNotify(savedVolume);
        ApplyVolume(savedVolume);

        slider.onValueChanged.AddListener(OnSliderChanged);
    }

    private void OnDestroy()
    {
        slider.onValueChanged.RemoveListener(OnSliderChanged);
    }

    private void OnSliderChanged(float value)
    {
        PlayerPrefs.SetFloat(exposedParameter + "_Volume", value);
        ApplyVolume(value);
    }

    private void ApplyVolume(float value)
    {
        if (audioMixer == null) return;

        bool isMuted = PlayerPrefs.GetInt(exposedParameter + "_Muted", 0) == 1;

        float decibels = (isMuted || value <= 0.0001f) ? -80f : Mathf.Log10(value) * 20f;
        audioMixer.SetFloat(exposedParameter, decibels);
    }
}