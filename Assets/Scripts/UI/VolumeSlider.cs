using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        GameManager.Instance.soundManager.ChangeMasterVolume(volumeSlider.value);
        volumeSlider.onValueChanged.AddListener(val =>  GameManager.Instance.soundManager.ChangeMasterVolume(val));
    }
}