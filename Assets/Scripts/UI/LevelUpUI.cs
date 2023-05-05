using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class LevelUpUI : MonoBehaviour
{
    private InputControls _input;
    private Graph _graph;

    [SerializeField]
    private Button _proceedButton;

    [SerializeField]
    private TextMeshProUGUI _text;

    public void Setup(InputControls input, Graph graph)
    {
        _input = input;
        _graph = graph;
        ((RectTransform)transform).DOAnchorPos(new Vector2(0, -2000), 1);
        _graph.OnLevelUp += HandleLevelUp;
        _proceedButton.onClick.AddListener(HandleProceedButtonClicked);
    }
    private void HandleLevelUp(int level, int maxLevel)
    {
        _input.Game.Disable();
        if (level >= maxLevel)
        {
            _text.text = "No more levels! Congratulations on finishing the game!";
            _proceedButton.interactable = false;
        }
        ((RectTransform)transform).DOAnchorPos(new Vector2(0, 0), 1);

    }

    private void HandleProceedButtonClicked()
    {
        _input.Game.Enable();
        ((RectTransform)transform).DOAnchorPos(new Vector2(0, -2000), 1);
        _graph.RenderCurrentGraph();
    }
}
