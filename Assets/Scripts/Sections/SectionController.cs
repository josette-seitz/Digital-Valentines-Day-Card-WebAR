using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Use Singleton approach to avoid GetComponent<MainController>()  in scripts
public class SectionController : SingletonBehavior<SectionController>
{
    [SerializeField]
    private List<GameObject> _sections;

    private GameObject currSection;
    private int index = 0;

    private MeshRenderer startRenderer;
    private AudioSource startSource;
    private Material startMat;
    private AudioClip startClip;
    private bool beginFinish = false;

    public MeshRenderer StartRenderer
    {
        get
        {
            return startRenderer;
        }
    }

    public Material StartMaterial
    {
        get
        {
            return startRenderer.material;
        }
        set
        {
            startRenderer.material = value;
        }
    }

    public AudioSource StartAudio
    {
        get
        {
            return startSource;
        }
    }

    public bool BeginningFinished
    {
        get
        {
            return beginFinish;
        }
        set
        {
            beginFinish = value;
        }
    }

    protected override void Awake()
    {
        startRenderer = GetComponent<MeshRenderer>();
        startSource = GetComponent<AudioSource>();

        startMat = StartMaterial;
        startClip = startSource.clip;
    }

    void OnEnable()
    {
        EventDispatcher.Instance.RegisterListener(OnEnableDisableSection);

        if(!beginFinish)
        {
            StartMaterial = startMat;
            startSource.clip = startClip;
            startSource.Play();
        }

        currSection = _sections[index];
        currSection.SetActive(true);
    }

    void OnDisable()
    {
        EventDispatcher.Instance.UnregisterListener(OnEnableDisableSection);

        switch(beginFinish)
        {
            case true:
                index = _sections.Count - 1;
                break;
            case false:
                index = 0;
                break;
        }

        currSection.SetActive(false);
    }

    public void OnEnableDisableSection(GameObject gameObj)
    {
        if(currSection == gameObj)
        {
            // Turn off current object
            currSection.SetActive(false);
            // Turn on next object
            index++;
            currSection = _sections[index];
            currSection.SetActive(true);
        }
        else
        {
            Debug.LogError("Current Section not set correctly.");
        }
    }
}
