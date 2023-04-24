using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define
{
    //방향을 나타내는 Enum
    public enum Direction
    {
        Up,
        Right,
        Down,
        Left,
        Count
    }

    public static Direction GetDirectionByVector(Vector2 directionVector)
    {
        return Direction.Up;
    }

    //방향에 맞는 이동 벡터2 값을 담은 딕셔너리
    public static Dictionary<Direction, Vector2> directionVectors = new Dictionary<Direction, Vector2> 
    {
        { Direction.Up, Vector2.up },
        { Direction.Right, Vector2.right },
        { Direction.Down, Vector2.down },
        { Direction.Left, Vector2.left }
    };

    //방 종류
    public enum EventType
    {
        Start,
        Shop,
        Rest,
        Event,
        Enemy,
        Boss,
        Count
    }

    //랜덤하게 min과 max 사이의 count개의 수를 고르는 함수 생성.
    public static List<int> GenerateRandomNumbers(int min, int max, int count)
    {
        if (count == 0) return null;

        List<int> randomNumbers = new List<int>(count);

        for (int i = 0; i < count;)
        {
            int number = Random.Range(min, max);

            if (!randomNumbers.Contains(number))
            {
                randomNumbers.Add(number);
                i++;
            }
        }
        return randomNumbers;
    }
}
