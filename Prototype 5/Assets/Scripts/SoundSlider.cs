using UnityEngine;
using UnityEngine.UI;

public class SoundSlider : MonoBehaviour
{
    private GameObject soundSource;
    private Slider slider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        soundSource = GameObject.Find("Main Camera");
        slider = GetComponent<Slider>();
        slider.SetValueWithoutNotify(0.5f);
        slider.onValueChanged.AddListener(ChangeVolume);
    }

    // Update is called once per frame
    void ChangeVolume(float value)
    {
        soundSource.GetComponent<AudioSource>().volume = value;
    }
}
