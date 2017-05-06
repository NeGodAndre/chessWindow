
using System.Collections.Generic;

class Knight : Figure
{
    public Knight(string name, string color, Position position) : base(name, color, position)
    {
        this.firstMove = false;
    }

    public Knight(string name, string color, Position position, bool firstMove) : base(name, color, position)
    {
        this.firstMove = firstMove;
    }

    public override List<Position> PossibleMoves(IFigureInterface[,] matrix, int cell)
    {
        List<Position> list = new List<Position>();

        for (int i = position.x - 2; i <= position.x + 2 && i < cell; i += 4)
            if (i >= 0)
                for (int j = position.y - 1; j <= position.y + 1 && j < cell; j += 2)
                    if (j >= 0)
                        if (matrix[i, j] == null)
                            list.Add(new Position(i, j));
                        else
                            if (matrix[i, j].GetColor() != color)
                            list.Add(new Position(i, j));

        for (int i = position.x - 1; i <= position.x + 1 && i < cell; i += 2)
            if (i >= 0)
                for (int j = position.y - 2; j <= position.y + 2 && j < cell; j += 4)
                    if (j >= 0)
                        if (matrix[i, j] == null)
                            list.Add(new Position(i, j));
                        else
                            if (matrix[i, j].GetColor() != color)
                            list.Add(new Position(i, j));

        return list;
    }

    public override IFigureInterface DeepCopy()
    {
        return new Knight(this.name, this.color, this.position, this.firstMove);
    }
}
