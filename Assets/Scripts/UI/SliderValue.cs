using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class SliderValue : MonoBehaviour {
    [Header("References")]
    [SerializeField] private Slider slider;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI label;

    [Header("Settings")]
    [SerializeField] private string format = "0"; 
    [Tooltip("Мінімальне значення, яке бачить користувач (наприклад, 0%)")]
    [SerializeField] private float minDisplayValue = 0f;
    [Tooltip("Максимальне значення, яке бачить користувач (наприклад, 100%)")]
    [SerializeField] private float maxDisplayValue = 100f;

    public UnityEvent<float> OnValueChanged;

    private void Start() {
        slider.minValue = 0f;
        slider.maxValue = 1f;

        slider.onValueChanged.AddListener(HandleSliderValueChanged);
        inputField.onEndEdit.AddListener(HandleInputFieldValueChanged);

        UpdateInputText(slider.value);
    }

    private void HandleSliderValueChanged(float normalizedValue) {
        UpdateInputText(normalizedValue);
        OnValueChanged?.Invoke(normalizedValue);
    }

    private void HandleInputFieldValueChanged(string textValue) {
        if (float.TryParse(textValue, out float parsedDisplayValue)) {
            parsedDisplayValue = Mathf.Clamp(parsedDisplayValue, minDisplayValue, maxDisplayValue);

            float normalizedValue = Mathf.InverseLerp(minDisplayValue, maxDisplayValue, parsedDisplayValue);

            slider.SetValueWithoutNotify(normalizedValue);

            UpdateInputText(normalizedValue);

            OnValueChanged?.Invoke(normalizedValue);
        } else {
            UpdateInputText(slider.value);
        }
    }

    private void UpdateInputText(float normalizedValue) {
        float displayValue = Mathf.Lerp(minDisplayValue, maxDisplayValue, normalizedValue);

        inputField.SetTextWithoutNotify(displayValue.ToString(format));
    }

    public void SetValue(float normalizedValue, bool triggerEvent = false) {
        normalizedValue = Mathf.Clamp01(normalizedValue);

        slider.SetValueWithoutNotify(normalizedValue);
        UpdateInputText(normalizedValue);

        if (triggerEvent)
            OnValueChanged?.Invoke(normalizedValue);
    }

    public float GetValue() {
        return slider.value;
    }

    public void SetLabel(string text) {
        if (label != null) label.text = text;
    }

    private void OnDestroy() {
        slider.onValueChanged.RemoveListener(HandleSliderValueChanged);
        inputField.onEndEdit.RemoveListener(HandleInputFieldValueChanged);
    }
}
