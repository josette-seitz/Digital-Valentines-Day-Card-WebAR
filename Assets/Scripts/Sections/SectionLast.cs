using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionLast : Section
{
    [SerializeField]
    private Material _lastMat;
    [SerializeField]
    private AudioClip _lastClip;

    private SectionController section;

    public AudioClip LastClip
    {
        get
        {
            return _lastClip;
        }
        set
        {
            _lastClip = value;
        }
    }

    private void Awake()
    {
        section = SectionController.Instance;
    }

    public override void OnEnable()
    {
        section.StartMaterial = _lastMat;
        section.StartAudio.clip = _lastClip;
        section.StartAudio.Play();
    }

    public override void OnDisable()
    {
        this.gameObject.SetActive(false);
    }
}
