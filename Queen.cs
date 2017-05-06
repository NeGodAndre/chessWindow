﻿using System.Collections.Generic;

class Queen : Figure
    {
    public Queen(string name, string color, Position position) : base(name, color, position)
    {
        this.firstMove = true;
    }

    public Queen(string name, string color, Position position, bool firstMove) : base(name, color, position)
    {
        this.firstMove = false;
    }

    public override List<Position> PossibleMoves(IFigureInterface[,] matrix, int cell)
    {
        List<Position> list = new List<Position>();

        for (int i = position.y + 1; i < cell; i++)
            if (matrix[position.x, i] == null)
            {
                list.Add(new Position(position.x, i));
            }
            else
            {
                if (matrix[position.x, i].GetColor() != color)
                {
                    list.Add(new Position(position.x, i));
                    break;
                }
                else
                    break;
            }

        for (int i = position.y - 1; i >= 0; i--)
            if (matrix[position.x, i] == null)
                list.Add(new Position(position.x, i));
            else
            {
                if (matrix[position.x, i].GetColor() != color)
                {
                    list.Add(new Position(position.x, i));
                    break;
                }
                else
                    break;
            }

        for (int i = position.x + 1; i < cell; i++)
            if (matrix[i, position.y] == null)
                list.Add(new Position(i, position.y));
            else
            {
                if (matrix[i, position.y].GetColor() != color)
                {
                    list.Add(new Position(i, position.y));
                    break;
                }
                else
                    break;
            }

        for (int i = position.x - 1; i >= 0; i--)
            if (matrix[i, position.y] == null)
                list.Add(new Position(i, position.y));
            else
            {
                if (matrix[i, position.y].GetColor() != color)
                {
                    list.Add(new Position(i, position.y));
                    break;
                }
                else
                    break;
            }

        for (int i = 1; position.x + i < cell && position.y + i < cell; i++)
            if (matrix[position.x + i, position.y + i] == null)
                list.Add(new Position(position.x + i, position.y + i));
            else
            {
                if (matrix[position.x + i, position.y + i].GetColor() != color)
                {
                    list.Add(new Position(position.x + i, position.y + i));
                    break;
                }
                else
                    break;
            }

        for (int i = 1; position.x + i < cell && position.y - i >= 0; i++)
            if (matrix[position.x + i, position.y - i] == null)
                list.Add(new Position(position.x + i, position.y - i));
            else
            {
                if (matrix[position.x + i, position.y - i].GetColor() != color)
                {
                    list.Add(new Position(position.x + i, position.y - i));
                    break;
                }
                else
                    break;
            }

        for (int i = 1; position.x - i >= 0 && position.y - i >= 0; i++)
            if (matrix[position.x - i, position.y - i] == null)
                list.Add(new Position(position.x - i, position.y - i));
            else
            {
                if (matrix[position.x - i, position.y - i].GetColor() != color)
                {
                    list.Add(new Position(position.x - i, position.y - i));
                    break;
                }
                else
                    break;
            }

        for (int i = 1; position.x - i >= 0 && position.y + i < cell; i++)
            if (matrix[position.x - i, position.y + i] == null)
                list.Add(new Position(position.x - i, position.y + i));
            else
            {
                if (matrix[position.x - i, position.y + i].GetColor() != color)
                {
                    list.Add(new Position(position.x - i, position.y + i));
                    break;
                }
                else
                    break;
            }

        return list;
    }

    public override IFigureInterface DeepCopy()
    {
        return new Queen(this.name, this.color, this.position, this.firstMove);
    }
}
