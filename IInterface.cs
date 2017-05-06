using System.Collections.Generic;

public interface IInterface
{
    void FillCell(Position position, string name, string color);
    void Move(Position oldPosition, Position newPosition);
    void PawnToQueen(Position position, string name);
    void HintOn(List<Position> hint);
    void HintOff();
    void DisplayingMessage(string message);
}
