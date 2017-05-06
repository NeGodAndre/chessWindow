using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct Position
{
    public Position(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public int x;
    public int y;
}

interface IFigureInterface
{
    string GetName();
    string GetColor();
    Position GetPosition();
    List<Position> PossibleMoves(IFigureInterface[,] matrix, int cell);
    void Move(Position position);
    bool IsFirstMove();
    //
    IFigureInterface DeepCopy();
}
/*
white стартуют с 0
black стартуют с 8
*/