using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section1 : Section
{
    [SerializeField]
    private List<GameObject> _lightTheBeamText;
    [SerializeField]
    private GameObject _beamParticles;

    private int index = 0;

    public override void OnEnable()
    {
        StartCoroutine(ShowText());
    }

    public override void OnDisable()
    {
        foreach(var beamText in _lightTheBeamText)
        {
            beamText.SetActive(false);
        }

        _beamParticles.SetActive(false);
        index = 0;
    }

    IEnumerator ShowText()
    {
        yield return new WaitForSeconds(1.6f);
        _lightTheBeamText[index].SetActive(true);
        index++;
        yield return new WaitForSeconds(1.5f);
        _lightTheBeamText[index].SetActive(true);
        index++;
        yield return new WaitForSeconds(1.5f);
        _lightTheBeamText[index].SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _beamParticles.SetActive(true);
        yield return new WaitForSeconds(5.5f);
        EventDispatcher.Instance.Dispatch(this.gameObject);
    }
}
