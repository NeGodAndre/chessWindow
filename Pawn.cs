using System.Collections.Generic;

class Pawn: Figure
{
    public Pawn(string name, string color, Position position) : base(name, color, position)
    {
        this.firstMove = true;
    }
    public Pawn(string name, string color, Position position, bool firstMove) : base(name, color, position)
    {
        this.firstMove = firstMove;
    }

    public override List<Position> PossibleMoves(IFigureInterface[,] matrix, int cell)
    {
        List<Position> list = new List<Position>();
        if (color == "white")
        {
            if (position.x < cell - 1)
            {
                if (matrix[position.x + 1, position.y] == null)
                    list.Add(new Position(position.x + 1, position.y));
                if (position.y < cell - 1)
                    if (matrix[position.x + 1, position.y + 1] != null)
                        if (matrix[position.x + 1, position.y + 1].GetColor() != color)
                            list.Add(new Position(position.x + 1, position.y + 1));
                if (position.y > 0)
                    if (matrix[position.x + 1, position.y - 1] != null)
                        if (matrix[position.x + 1, position.y - 1].GetColor() != color)
                            list.Add(new Position(position.x + 1, position.y - 1));
            }
            if (IsFirstMove())
                if (matrix[position.x + 2, position.y] == null && matrix[position.x+1, position.y] == null)
                    list.Add(new Position(position.x + 2, position.y));
        }
        else
        {
            if (position.x >= 1)
            {
                if (matrix[position.x - 1, position.y] == null)
                    list.Add(new Position(position.x - 1, position.y));
                if (position.y < cell - 1)
                    if (matrix[position.x - 1, position.y + 1] != null)
                        if (matrix[position.x - 1, position.y + 1].GetColor() != color)
                            list.Add(new Position(position.x - 1, position.y + 1));
                if (position.y > 0)
                    if (matrix[position.x - 1, position.y - 1] != null)
                        if (matrix[position.x - 1, position.y - 1].GetColor() != color)
                            list.Add(new Position(position.x - 1, position.y - 1));
            }
            if (IsFirstMove())
                if (matrix[position.x - 2, position.y] == null && matrix[position.x - 1, position.y] == null)
                        list.Add(new Position(position.x - 2, position.y));
        }

        return list;
    }

    public override IFigureInterface DeepCopy()
    {
        return new Pawn(this.name, this.color, this.position, this.firstMove);
    }
}
