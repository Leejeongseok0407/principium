using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raser : Patterns
{
    [SerializeField]
    private float _spwanInterval = 0.5f;
    [SerializeField]
    GameObject[] _meteos;

    private void Start()
    {
        for (int i = 0; i < _meteos.Length; i++)
        {
            _meteos[i].gameObject.SetActive(false);
        }
    }

    public override IEnumerator Run()
    {
        for (int i = 0; i < _meteos.Length; i++)
        {
            _meteos[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(_spwanInterval);
        }
        if (_meteos[_meteos.Length - 1].gameObject.activeSelf == true)
            yield return null;

    }

    public override void Play()
    {
        _patternAni.Play("Mad");
    }
}