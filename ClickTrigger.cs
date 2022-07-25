using System;
using System.Collections.Generic;
using UnityEngine;

public class ClickTrigger : MonoBehaviour
{
	TicTacToeAI _ai;

	[SerializeField]
	private int _myCoordX = 0;
	[SerializeField]
	private int _myCoordY = 0;

	[SerializeField]
	private bool canClick;

	private void Awake()
	{
		_ai = FindObjectOfType<TicTacToeAI>();
	}

	private void Start(){

		_ai.onGameStarted.AddListener(AddReference);
		_ai.onGameStarted.AddListener(() => SetInputEndabled(true));
		_ai.onPlayerWin.AddListener((win) => SetInputEndabled(false));
	}

	private void SetInputEndabled(bool val){
		canClick = val;
	}

	private void AddReference()
	{
		_ai.RegisterTransform(_myCoordX, _myCoordY, this);
		canClick = true;
	}

	private void OnMouseDown()
	{
		if(canClick){
			_ai.PlayerSelects(_myCoordX, _myCoordY);
			AiSelects();
		}
	}

	private void AiSelects()
	{
		int aiCoordX = _myCoordX;
		int aiCoordY = _myCoordY;
		bool isSelected = false;

		for (int y = 0; y <= 2; y++)
		{
			if (!isSelected)
			{
				for (int x = 0; x <= 2; x++)
				{
					if (!_ai.IsSelected(x, y))
					{
						if (x == _myCoordX || y == _myCoordY)
						{
							aiCoordX = x;
							aiCoordY = y;
							isSelected = true;
							break;
						}
					}
				}
			}
		}

		if (isSelected)
		{
			_ai.AiSelects(aiCoordX, aiCoordY);
		}
	}
}
