using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Image activeHealthBar;
    [SerializeField] private Image activeHealthBarMask;
    [SerializeField] private Image deadHealthBar;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Camera uiCam;

    [Header("Visual Config")]
    [SerializeField] private Color activeHealthColor;
    [SerializeField] private Color deadHealthColor;
    [SerializeField] private bool displayRelativeToOwnerPosition;
    [SerializeField] private GameObject owner;
    [SerializeField] private Vector2 positionalOffset;  //  this is only used if showing relative to owner position, otherwise the rect transform's position will be used

    [Header("Math Config")]
    [SerializeField] private float maxValue;
    [SerializeField] private float minValue;
    [SerializeField] private float currentValue;

    /// <summary>
    /// Used to preview the in-game look of the health bar
    /// </summary>
    private void OnValidate()
    {
        UpdateHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthBar();
        activeHealthBarMask.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x / 2, activeHealthBarMask.GetComponent<RectTransform>().sizeDelta.y);
    }

    /// <summary>
    /// All the updating code for the health bar is included here
    /// </summary>
    private void UpdateHealthBar()
    {

        // active health bar width = remap based on values and dead health bar width

        activeHealthBar.color = activeHealthColor;
        deadHealthBar.color = deadHealthColor;

        if (displayRelativeToOwnerPosition)
        {
            RectTransform canvasRect = canvas.GetComponent<RectTransform>();

            Vector2 viewPortPosition = uiCam.WorldToViewportPoint(owner.transform.position);
            Vector2 ownerScreenPosition = new Vector2((viewPortPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f), (viewPortPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f));

            gameObject.GetComponent<RectTransform>().anchoredPosition = ownerScreenPosition + positionalOffset;
        }
    }

    /// <summary>
    /// Update the current value (ex. remaining health)
    /// </summary>
    /// <param name="value">
    /// The new value
    /// </param>
    public void SetCurrentValue(float value)
    {
        currentValue = value;
    }

    /// <summary>
    /// Update the maximum possible value (ex. max health)
    /// </summary>
    /// <param name="value">
    /// The new value
    /// </param>
    public void SetMaxValue(float value)
    {
        maxValue = value;
    }

    /// <summary>
    /// Update the minimum possible value (ex. minimum health, often 0)
    /// </summary>
    /// <param name="value">
    /// The new value
    /// </param>
    public void SetMinValue(float value)
    {
        minValue = value;
    }
}
