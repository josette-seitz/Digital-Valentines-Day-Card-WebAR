using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Section3 : Section
{
    [SerializeField]
    private SpriteRenderer _zionImage;
    [SerializeField]
    private List<GameObject> _letsGoPelsText;
    [SerializeField]
    private GameObject _pelicanLogo;
    [SerializeField]
    private AudioClip _birdAudio;

    private SectionController section;
    private float imageAlpha;
    private float startAlpha;
    private Color imageColor;
    private int index = 0;
    private float speed = 3f;
    private bool flag = false;
    private Vector3 startPelicanLogo;

    void Awake()
    {
        section = SectionController.Instance;
        imageAlpha = _zionImage.color.a;
        startAlpha = imageAlpha;
        imageColor = _zionImage.color;
        startPelicanLogo = _pelicanLogo.transform.position;
    }

    public override void OnEnable()
    {
        StartCoroutine(FadeInImage(imageAlpha));
        StartCoroutine(ShowText());
    }

    public override void OnDisable()
    {
        // Set back to original 
        _zionImage.color = new Color(imageColor.r, imageColor.g, imageColor.g, startAlpha);

        foreach(var pelsText in _letsGoPelsText)
        {
            pelsText.SetActive(false);
        }

        index = 0;
        _pelicanLogo.SetActive(false);
        _pelicanLogo.transform.position = startPelicanLogo;
        flag = false;
    }

    IEnumerator FadeInImage(float alpha)
    {
        while(alpha < 1)
        {
            // Increment the alpha value from 0 to 1 over 2 seconds
            alpha += Time.deltaTime / 2f;

            // Set the new alpha value
            _zionImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, alpha);

            // Wait for the next frame
            yield return null;
        }
    }

    IEnumerator ShowText()
    {
        _letsGoPelsText[index].SetActive(true);
        index++;
        yield return new WaitForSeconds(1f);
        _letsGoPelsText[index].SetActive(true);
        index++;
        yield return new WaitForSeconds(1f);
        _letsGoPelsText[index].SetActive(true);
        yield return new WaitForSeconds(1.5f);
        _pelicanLogo.SetActive(true);
        flag = true;
        section.StartAudio.clip = _birdAudio;
        section.StartAudio.Play();
    }

    void Update()
    {
        if (flag)
        {
            _pelicanLogo.transform.position = 
                Vector3.MoveTowards(_pelicanLogo.transform.position, Camera.main.transform.position, speed * Time.deltaTime);
            float distance = Vector3.Distance(_pelicanLogo.transform.position, Camera.main.transform.position);
            if(distance <= 0.25f)
            {
                EventDispatcher.Instance.Dispatch(this.gameObject);
            }
        }
    }
}
