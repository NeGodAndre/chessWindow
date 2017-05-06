
using System.Collections.Generic;

class Bishop: Figure
{
    public Bishop(string name, string color, Position position) : base(name, color, position)
    {
        this.firstMove = false;
    }
    public Bishop(string name, string color, Position position, bool firstMove) : base(name, color, position)
    {
        this.firstMove = firstMove;
    }

    public override List<Position> PossibleMoves(IFigureInterface[,] matrix, int cell)
    {
        List<Position> list = new List<Position>();

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
        return new Bishop(this.name, this.color, this.position, this.firstMove);
    }
}
