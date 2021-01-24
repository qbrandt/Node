using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Color
{
    BLANK = 0,
    GREEN = 1,
    YELLOW = 2,
    RED = 3,
    BLUE = 4
}

public enum UserEntity
{
    NONE = 0,
    PLAYER_1 = 1,
    PLAYER_2 = 2
}

public struct Tile
{
    public Color Color { get; set; }
    public int Dots { get; set; }
}

public struct BoardPosition
{
    public UserEntity User { get; set; }
    public bool FirstNetwork { get; set; }
}

public struct Coordinate
{
    public int X;
    public int Y;

    Coordinate(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public struct Move
{
    Coordinate Postition;
    UserEntity User;
}


public class Board : MonoBehaviour
{
    private Tile[5,5] tiles { get; set; }
    private BoardPosition[11,11] moves { get; set; }

    private int user1NetworkLength1;
    private int user1NetworkLength2;
    private int user2NetworkLength1;
    private int user2NetworkLength2;

    public void SetBoard(Tile[,] board)
    {
        tiles = board;
        ResetMoves();
    }

    private bool IsTile(Coordinate position)
    {
        if (position.X < 11 && position.Y < 11 &&
            position.X >= 0 && position.Y >= 0 &&
            position.X % 2 == 1 && position.Y % 2 == 1 &&
            ((5 - position.X) / 2) + (5 - position.Y) / 2)) <= 2)
        {
            return true;
        }
        return false;
    }

    public bool MoveIsValid(Move move)
    {
        
    }

    public bool MakeMove(Move move)
    {

    }

    public UserEntity LongestNetwork()
    {
        int user1 = user1NetworkLength1 > user1NetworkLength2 ? user1NetworkLength1 : user1NetworkLength2;
        int user2 = user2NetworkLength1 > user2NetworkLength2 ? user2NetworkLength1 : user2NetworkLength2;
        if (user1 > user2)
            return UserEntity.PLAYER_1;
        else if (user2 > user1)
            return UserEntity.PLAYER_2;
        else
            return UserEntity.NONE;
    }

    public int NumberOfNodes(UserEntity user)
    {
        int count = 0;

        for (int i = 0; i < 11; i += 2)
        {
            for (int j = 0; j < 11; j += 2)
            {
                if (moves[i,j].User == user)
                {
                    count++;
                }
            }
        }

        return count;
    }

    public int NumberOfCapturedTile(UserEntity user)
    {
        int count = 0;

        for (int i = 1; i < 11; i += 2)
        {
            for (int j = 1; j < 11; j += 2)
            {
                if (moves[i, j].User == user)
                {
                    count++;
                }
            }
        }

        return count;
    }

    private void ResetMoves()
    {
        foreach var move in moves
        {
            move.User = UserEntity.NONE;
            move.FirstNetwork = false;
        }
    }

    private Coordinate TileToBoardPosition(Coordinate coordinate)
    {
        return new Coordinate(coordinate.X * 2 + 1, coordinate.Y * 2 + 1)
    }

    private Coordinate BoardPositionToTile(Coordinate coordinate)
    {
        return new Coordinate((coordinate.X - 1) / 2, (coordinate.Y - 1) / 2)
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
