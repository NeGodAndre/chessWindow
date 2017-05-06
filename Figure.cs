using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Figure : IFigureInterface
{
    protected string name;
    protected string color;
    protected Position position;
    protected bool firstMove;

    public Figure(string name, string color, Position position)
    {
        this.name = name;
        this.color = color;
        this.position.x = position.x;
        this.position.y = position.y;
    }

    public string GetName()
    {
        return name;
    }
    public string GetColor()
    {
        return color;
    }
    public Position GetPosition()
    {
        return position;
    }
    public bool IsFirstMove()
    {
        return firstMove;
    }
    public virtual void Move(Position position)
    {
        if (firstMove)
            firstMove = false;
        this.position.x = position.x;
        this.position.y = position.y;
    }
    public virtual List<Position> PossibleMoves(IFigureInterface[,] matrix, int cell)
    {
        return new List<Position>();
    }

    public virtual IFigureInterface DeepCopy()
    {
        return new Figure(this.name, this.color, this.position);
    }
}
