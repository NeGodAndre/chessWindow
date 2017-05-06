using System.Collections.Generic;

struct MoveCell
{
    public Position position;
    public Position newPosition;
    public bool isMove;
}

class Controller
{
    private AI ai;

    private Board board;
    private MoveCell moveCell;
    private string color;
    private bool playerMove;
    private bool isGame;
    private string win;

    IInterface interfaceBoard;

    public Controller(IInterface interfaceBoard)
    {
        this.interfaceBoard = interfaceBoard;
    }

    public void Start(string color)
    {
        board = new Board();
        this.color = color;
        if (color == "white")
            playerMove = true;
        else
            playerMove = false;
        isGame = true;
        win = "";

        for(int i = 0; i < 8; i++)
            for(int j = 0; j < 8; j++)
            {
                string name = "";
                string col = "";
                Position p = new Position(i, j);
                board.AreFigure(p, ref name, ref col);
                interfaceBoard.FillCell(p, name, col);
            }

        if (color == "white")
            ai = new AI("black", 2);
        else
            ai = new AI("white", 2);
    }

    public bool IsGame()
    {
        return isGame;
    }

    public string IsWin()
    {
        return win;
    }

    private void checkFinishAndMat()
    {
        string str = "";
        if (board.WhoWin(ref str))
            isGame = false;
        interfaceBoard.DisplayingMessage(str);
    }

    public bool CellAssignment(int x, int y)
    {
        if (/*playerMove && */isGame)
            if (0 <= x && x < 8 && 0 <= y && y < 8)
            {
                Position position = new Position(x, y);
                playerTurn(position);
               // checkFinishAndMat();
               // AITurn();
               // checkFinishAndMat();
                return true;
                //if()
            }
            else
                return false;
        return false;
    }

    private void changeCourse()
    {
        playerMove = !playerMove;
    }

    private void AITurn()
    {
        Position figure = new Position();
        Position newPosition = new Position();

        do
            ai.MakeMove(board, ref figure, ref newPosition);
        while (!board.Move(figure, newPosition));
        interfaceBoard.Move(figure, newPosition);

        changeCourse();
    }

    private void playerTurn(Position position)
    {
        if(moveCell.isMove)
        {
            interfaceBoard.HintOff();
            moveCell.isMove = false;
            if (!(moveCell.position.x == position.x && moveCell.position.y == position.y))
            {
                moveCell.newPosition = position;

                string name = "";
                string color = "";
                board.AreFigure(moveCell.position, ref name, ref color);

                if (board.Move(moveCell.position, moveCell.newPosition))
                {
                    interfaceBoard.Move(moveCell.position, moveCell.newPosition);
                    //
                    string newName = "";
                    board.AreFigure(moveCell.newPosition, ref newName, ref color);
                    if(name != newName && name == "Pawn")
                    {
                        interfaceBoard.PawnToQueen(moveCell.newPosition, newName);
                    }
                    //
                    changeCourse();
                    checkFinishAndMat();                  
                    if (isGame)
                    {
                        AITurn();
                        changeCourse();
                        checkFinishAndMat();
                    }
                }
            }
        }
        else
        {
            string name = "";
            string colorFigure = "";
            if(board.AreFigure(position, ref name, ref colorFigure))
                if(color == colorFigure)
                {
                    List<Position> list = board.ListCoordinatesMoves(position);
                    if (list.Count != 0)
                    {
                        moveCell.position = position;
                        moveCell.isMove = true;
                        interfaceBoard.HintOn(list);
                    }
                }
        }
        checkFinishAndMat();
    }
}
