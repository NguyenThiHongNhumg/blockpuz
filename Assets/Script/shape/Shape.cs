using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    public List<GameObject> squareShapeImage;

    [HideInInspector]
    public ShapeData CurrentShapeData;

    private List<GameObject> _currentShape = new List<GameObject>();

    private void Start()
    {
        
    }

    public void RequestNewShape(ShapeData shapeData)
    {
        CreateShape(shapeData);
    }

    public void CreateShape(ShapeData shapeData)
    {
        var backgroundRandom = UnityEngine.Random.Range(0, squareShapeImage.Count);
        var squareShapeImageRandom = squareShapeImage[backgroundRandom];
        CurrentShapeData = shapeData;
        var totalSquareNumber = GetNumberOfSquare(shapeData);
        while(_currentShape.Count <= totalSquareNumber)
        {
            _currentShape.Add(Instantiate(squareShapeImageRandom, transform) as GameObject);            
        }
        foreach(var square in _currentShape)
        {
            square.gameObject.transform.position = Vector3.zero;
            square.gameObject.SetActive(false);
        }
        var squareRect =  squareShapeImageRandom.GetComponent<RectTransform>();
        var moveDistance = new Vector2(squareRect.rect.width * squareRect.localScale.x, 
            squareRect.rect.height * squareRect.localScale.y);

        var currentIndexInList = 0;
        for(var row = 0; row < shapeData.rows; row++)
        {
            for(var column = 0; column < shapeData.columns; column++)
            {
                if (shapeData.board[row].column[column])
                {
                    _currentShape[currentIndexInList].SetActive(true);
                    _currentShape[currentIndexInList].GetComponent<RectTransform>().localPosition =
                        new Vector2(GetXPositionForShapeSquare(shapeData, column, moveDistance), GetYPostionForShapeSquare(shapeData, row, moveDistance));
                    currentIndexInList++;
                }
            }
        }

    }

    private float GetYPostionForShapeSquare(ShapeData shapeData, int row, Vector2 moveDistance)
    {
        float shiftOnY = 0f;

        if (shapeData.rows > 1)
        {
            if (shapeData.rows % 2 != 0)
            {
                var middleSquareIndex = (shapeData.rows - 1) / 2;
                var multiplier = (shapeData.rows - 1) / 2;
                if (row < middleSquareIndex)
                {
                    shiftOnY = moveDistance.x * (-1);
                    shiftOnY *= multiplier;
                }
                else if (row > middleSquareIndex)
                {
                    shiftOnY = moveDistance.x * 1;
                    shiftOnY *= multiplier;
                }

            }
            else
            {
                var middleSquareIndex2 = (shapeData.rows == 2) ? 1 : (shapeData.rows / 2);
                var middleSquareIndex1 = (shapeData.rows == 2) ? 0 : (shapeData.rows - 1);
                var multiplier = shapeData.rows / 2;

                if (row == middleSquareIndex2 || row == middleSquareIndex1)
                {
                    if (row == middleSquareIndex2)
                    {
                        shiftOnY = moveDistance.x / 2;
                    }
                    if (row == middleSquareIndex1)
                    {
                        shiftOnY = (moveDistance.x / 2) * -1;
                    }
                }
                if (row < middleSquareIndex1 && row < middleSquareIndex2)
                {
                    shiftOnY = moveDistance.x * -1;
                    shiftOnY *= multiplier;
                }
                else if (row > middleSquareIndex1 && row > middleSquareIndex2)
                {
                    shiftOnY = moveDistance.x * 1;
                    shiftOnY *= multiplier;
                }
            }
        }

        return shiftOnY;
    }

    private float GetXPositionForShapeSquare(ShapeData shapeData, int column, Vector2 moveDistance)
    {
        float shiftOnX = 0f;

        if(shapeData.columns > 1)
        {
            if(shapeData.columns % 2 != 0)
            {
                var middleSquareIndex = (shapeData.columns - 1) / 2;
                var multiplier = (shapeData.columns - 1) / 2;
                if(column < middleSquareIndex)
                {
                    shiftOnX = moveDistance.x * (-1);
                    shiftOnX *= multiplier;
                }
                else if(column > middleSquareIndex)
                {
                    shiftOnX = moveDistance.x * 1;
                    shiftOnX *= multiplier;
                }

            }
            else
            {
                var middleSquareIndex2 = (shapeData.columns == 2) ? 1: (shapeData.columns /2);
                var middleSquareIndex1 = (shapeData.columns == 2) ? 0: (shapeData.columns - 1);
                var multiplier = shapeData.columns /2;

                if(column == middleSquareIndex2 || column == middleSquareIndex1)
                {
                    if(column == middleSquareIndex2)
                    {
                        shiftOnX = moveDistance.x / 2;
                    }
                    if(column == middleSquareIndex1)
                    {
                        shiftOnX = (moveDistance.x / 2) * -1;
                    }
                }
                if(column < middleSquareIndex1 && column < middleSquareIndex2)
                {
                    shiftOnX = moveDistance.x * -1;
                    shiftOnX *= multiplier;
                }
                else if(column > middleSquareIndex1 && column > middleSquareIndex2)
                {
                    shiftOnX = moveDistance.x * 1;
                    shiftOnX *= multiplier;
                }
            }
        }

        return shiftOnX;
    }
    

    /// <summary>
    /// Count cell active in board
    /// </summary>
    /// <param name="shapeData"></param>
    /// <returns></returns>
    private int GetNumberOfSquare(ShapeData shapeData)
    {
        int number = 0;
        foreach(var rowData in shapeData.board)
        {
            foreach(var cellActive in rowData.column)
            {
                if (cellActive)
                {
                    number++;
                }
            } 
        }
        return number;
    }
}
