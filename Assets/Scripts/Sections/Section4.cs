using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Section4 : Section
{
    [SerializeField]
    private List<TextMeshPro> _notesText;
    [SerializeField]
    private GameObject _buttonCanvas;
    [SerializeField]
    private Material _endBaseMat;

    private SectionController section;
    private int index = 0;

    void Awake()
    {
        section = SectionController.Instance;
    }
    public override void OnEnable()
    {
        if(!section.BeginningFinished)
            StartCoroutine(FadeInText(_notesText[index], 1f));

        section.StartMaterial = _endBaseMat;
    }

    public override void OnDisable()
    {
        if(!section.BeginningFinished)
        {
            foreach (var note in _notesText)
            {
                note.alpha = 0;
            }

            index = 0;
        }
    }

    IEnumerator FadeInText(TextMeshPro notes, float time)
    {
        float alpha = 0;

        yield return new WaitForSeconds(time);

        while(alpha < 1)
        {
            // Increment the alpha value from 0 to 1 over 2 seconds
            alpha += Time.deltaTime / 2;

            // Set the new alpha value
            notes.alpha = alpha;

            // Wait for the next frame
            yield return null;
        }

        index++;
        if (index >= _notesText.Count)
        {
            index = 0;
            _buttonCanvas.SetActive(true);
            section.BeginningFinished = true;
        }
        else
        {
            StartCoroutine(FadeInText(_notesText[index], 0));
        }
    }
}
