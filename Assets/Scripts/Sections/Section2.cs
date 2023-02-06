using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Section2 : Section
{
    [SerializeField]
    private TextMeshPro _versusText;

    private float textAlpha;
    private float startAlpha;

    void Awake()
    {
        textAlpha = _versusText.alpha;
        startAlpha = textAlpha;
    }

    public override void OnEnable()
    {
        StartCoroutine(FadeInText(textAlpha));
    }

    public override void OnDisable()
    {
        // Alpha back to transparent -> close to 0
        _versusText.alpha = startAlpha;
    }

    IEnumerator FadeInText(float alpha)
    {
        while (alpha < 1)
        {
            // Increment the alpha value from 0.5 to 1 over 2.5 seconds
            alpha += Time.deltaTime / 2.5f;

            // Set the new alpha value
            _versusText.alpha = alpha;

            // Wait for the next frame
            yield return null;
        }

        yield return new WaitForSeconds(1.75f);
        EventDispatcher.Instance.Dispatch(this.gameObject);
    }
}
