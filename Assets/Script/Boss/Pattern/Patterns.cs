using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Patterns : MonoBehaviour, IPattern, IAnimation
{

    [SerializeField]
    [Range(0, 4)]
    protected float _patternTime = 0.5f;
    [SerializeField]
    [Range(1, 5)]
    protected float _rotationSpeed;
    protected GameObject _player;
    protected Animator _patternAni;
    public GameObject Player
    {
        set
        {
            _player = value;
        }
    }
    public Animator PatternAni
    {
        set
        {
            _patternAni = value;
        }
    }

    public abstract void Play();

    public abstract IEnumerator Run();
}