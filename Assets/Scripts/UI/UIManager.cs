using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    InputControls _input;
    [SerializeField] LevelUpUI _levelUpUI;

    public void Setup(InputControls input, Graph graph)
    {
        _input = input;
        _levelUpUI.Setup(input, graph);
    }
}
