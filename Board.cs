using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Board
{
    private IFigureInterface[,] board;
    private int cell = 8;

    private int checkWhite = 0;
    private int checkBlack = 0;
    private string checkmate;

    private bool killWhite;
    private bool killBlack;

    private List<Position> listCheckWhite;
    private List<Position> listCheckBlack;

    public Board()
    {
        board = new IFigureInterface[cell, cell];
        createBlackFigure();
        createWhiteFigure();


        listCheckWhite = new List<Position>();
        listCheckBlack = new List<Position>();
        checkBlack = 0;
        checkWhite = 0;
        checkmate = "";
    }
    public Board(Board board)
    {
        this.board = board.getBoard();

        checkBlack = 0;
        checkWhite = 0;
        checkmate = "nobody";
        listCheckWhite = new List<Position>();
        listCheckBlack = new List<Position>();

    }

    protected IFigureInterface[,] getBoard()
    {
        IFigureInterface[,] matrix = new IFigureInterface[cell, cell];

        for(int i = 0; i < cell; i++)
            for(int j= 0; j < cell; j++)
                if (board[i, j] != null)
                    matrix[i, j] = board[i, j].DeepCopy();
                else
                    matrix[i, j] = null;

        return matrix;
    }

    private void createWhiteFigure()
    {
        string color = "white";
        for (int i = 0; i < cell; i++)
        {
            board[1, i] = new Pawn("Pawn", color, new Position(1, i));
        }

        board[0, 0] = new Rook("Rook", color, new Position(0, 0));
        board[0, 1] = new Knight("Knight", color, new Position(0, 1));
        board[0, 2] = new Bishop("Bishop", color, new Position(0, 2));
        board[0, 3] = new Queen("Queen", color, new Position(0, 3));
        board[0, 4] = new King("King", color, new Position(0, 4));
        board[0, 5] = new Bishop("Bishop", color, new Position(0, 5));
        board[0, 6] = new Knight("Knight", color, new Position(0, 6));
        board[0, 7] = new Rook("Rook", color, new Position(0, 7));
    }
    private void createBlackFigure()
    {
        string color = "black";
        for (int i = 0; i < cell; i++)
        {
            board[6, i] = new Pawn("Pawn", color, new Position(6, i));
        }

        board[7, 0] = new Rook("Rook", color, new Position(7, 0));
        board[7, 1] = new Knight("Knight", color, new Position(7, 1));
        board[7, 2] = new Bishop("Bishop", color, new Position(7, 2));
        board[7, 3] = new Queen("Queen", color, new Position(7, 3));
        board[7, 4] = new King("King", color, new Position(7, 4));
        board[7, 5] = new Bishop("Bishop", color, new Position(7, 5));
        board[7, 6] = new Knight("Knight", color, new Position(7, 6));
        board[7, 7] = new Rook("Rook", color, new Position(7, 7));
    }

    private IFigureInterface returnFigure(Position figure)
    {
        return board[figure.x, figure.y];
    }
    private void swapFigure(Position figure1, Position figure2)
    {
        board[figure2.x, figure2.y] = board[figure1.x, figure1.y];
        board[figure1.x, figure1.y] = null;
    }

    public IFigureInterface findKing(string color)
    {
        for (int i = 0; i < cell; i++)
            for (int j = 0; j < cell; j++)
                if (board[i, j] != null)
                    if (board[i, j].GetColor() == color)
                        if (board[i, j].GetName() == "King")
                            return board[i, j];
        return null;
    }
    /*
    \-----OLD-----\
    private List<Position> cheekKing(string color)// проверка короля
    {
        IFigureInterface king = null;
        Position kingPosition;

        king = findKing(color);
        if (king == null)
            return null;

        kingPosition = king.GetPosition();

        List<Position> list = new List<Position>();
        if (color == "white")
            list = ListCoordinatesFigures("black");
        else
            list = ListCoordinatesFigures("white");

        checkBlack = 0;
        checkWhite = 0;

        List<Position> listMoves = new List<Position>();
        for(int i = 0; i < list.Count; i++)
        {
            List<Position> buf = returnFigure(list[i]).PossibleMoves(board, cell);
            for(int j = 0; j < buf.Count; j++)
            {
                Position bufPosition = buf[j];
                if(kingPosition.x == bufPosition.x && kingPosition.y == bufPosition.y)
                {
                    if (color == "white")
                        checkWhite += 1;
                    if (color == "black")
                        checkBlack += 1;
                    listMoves.AddRange(buf);
                    break;
                }
            }
        }
        return listMoves;
    } 

            private List<Position> checkMoveFigure(List<Position> checkFigure, List<Position> list)
    {
        if (list.Count == 0)
            return checkFigure;

        List<Position> l = new List<Position>();
        for (int i = 0; i < checkFigure.Count; i++)
        {
            int buf = 0;
            for (int j = 0; j < list.Count; j++)
                if (checkFigure[i].x == list[j].x && checkFigure[i].y == list[j].y)
                    buf += 1;
            if (buf != 0)
            {
                l.Add(checkFigure[i]);
            }
        }
        return l;
    }
        */

    public bool WhoWin(ref string str)
    {
        str = checkmate;

        if(checkmate == "check and mate Black" || checkmate == "check and mate White")
        {
            return true;
        }

        return false;
    }

    private void PrivateWhoWin()
    {
        IFigureInterface king = null;
        king = findKing("black");
        if (king == null)
        {
            checkmate = "check and mate Black";
            return;
        }
        listCheckBlack = cheekKing("black");
        if (listCheckBlack.Count != 0)
        {
            checkmate = "check Black";
            return;
        }
       // ListCoordinatesFigures("black")
        king = findKing("white");
        if (king == null)
        {
            checkmate = "check and mate White";
            return;
        }
        listCheckWhite = cheekKing("white");
        if (listCheckWhite.Count != 0)
        {
            checkmate = "check White";
            return;
        }

        checkmate = "";
    } 

    /*
    массив заполняется координатами фигур,
    он передается в функций checkMove*, там происходит проверка может ли фигура в новой позиции защитить короля(королю уйти от атаки)
        */
    private List<Position> cheekKing(string color)// проверка короля, возвращает вектор с кординатами врагов, если они ставят шах и не возвращает ни чего, если король не под шахом
    {
        IFigureInterface king = null;
        Position kingPosition;

        king = findKing(color);
        if (king == null)
            return null;

        kingPosition = king.GetPosition();

        List<Position> list = new List<Position>();
        if (color == "white")
            list = ListCoordinatesFigures("black");
        else
            list = ListCoordinatesFigures("white");

        checkBlack = 0;
        checkWhite = 0;

        List<Position> listMoves = new List<Position>();
        for(int i = 0; i < list.Count; i++)
        {
            List<Position> buf = returnFigure(list[i]).PossibleMoves(board, cell);
            for(int j = 0; j < buf.Count; j++)
            {
                Position bufPosition = buf[j];
                if(kingPosition.x == bufPosition.x && kingPosition.y == bufPosition.y)
                {
                    if (color == "white") 
                        checkWhite += 1;
                    if (color == "black")
                        checkBlack += 1;
                    listMoves.Add(list[i]);//изменить
                    break;
                }
            }
        }
        return listMoves;
    }

    private List<Position> checkMoveFigure(List<Position> checkFigure, List<Position> list, Position king)//тут баг
        //checkFigure - как ходит фигура, king - позиция короля, list - позиции фигур которые ставят шах королю
    {
        if (list.Count == 0)
            return checkFigure;
        if (checkFigure.Count == 0)
            return checkFigure;

        List<Position> l = new List<Position>();
        for (int i = 0; i < checkFigure.Count; i++)
            for (int j = 0; j < list.Count; j++)

                if (Math.Abs(Math.Sqrt(Math.Pow(list[j].x - checkFigure[i].x, 2) + Math.Pow(list[j].y - checkFigure[i].y, 2)) + Math.Sqrt(Math.Pow(king.x - checkFigure[i].x, 2) + Math.Pow(king.y - checkFigure[i].y, 2)) - Math.Sqrt(Math.Pow(king.x - list[j].x, 2) + Math.Pow(king.y - list[j].y, 2))) < 0.01)
                    l.Add(checkFigure[i]);

        return l;
    }
    private List<Position> checkMoveKing(List<Position> checkKing, List<Position> list, Position king) //идет сравнение checkKing с list
        ////КОСЯК, ХОДИТ НА СМЕРТЬ
    {
        if (list.Count == 0)
            return checkKing;

        List<Position> l = new List<Position>();
        for (int i = 0; i < checkKing.Count; i++)
            for (int j = 0; j < list.Count; j++)
                if (!(Math.Abs(Math.Sqrt(Math.Pow(list[j].x - checkKing[i].x, 2) + Math.Pow(list[j].y - checkKing[i].y, 2)) + Math.Sqrt(Math.Pow(king.x - checkKing[i].x, 2) + Math.Pow(king.y - checkKing[i].y, 2)) - Math.Sqrt(Math.Pow(king.x - list[j].x, 2) + Math.Pow(king.y - list[j].y, 2))) < 0.01))
                    l.Add(checkKing[i]);

        return l;
    }

   /* private void reviewCheckmate()
    {
        listCheckWhite = cheekKing("white");
        listCheckBlack = cheekKing("black");

        if (checkmate == "nobody")
        {
            List<Position> list;
            List<Position> listCheck;
            if (findKing("white") != null)
            {
                list = findKing("white").PossibleMoves(board, cell);
                listCheck = checkMoveKing(list, listCheckWhite, findKing("white").GetPosition());
                if (listCheck.Count == 0 && list.Count != 0)
                {
                    checkmate = "white";
                    return;
                }
                if (list.Count == listCheck.Count)
                    checkWhite = 0;
            }
            else
            {
                checkmate = "white";
                return;
            }

            if (findKing("black") != null)
            {
                list = findKing("black").PossibleMoves(board, cell);
                listCheck = checkMoveKing(list, listCheckBlack, findKing("black").GetPosition());
                if (listCheck.Count == 0 && list.Count != 0)
                {
                    checkmate = "black";
                    return;
                }
                if (list.Count == listCheck.Count)
                    checkBlack = 0;
            }
            else
            {
                checkmate = "black";
                return;
            }
        }
        checkmate = "nobody";
    }*/

    private void PawnToQueen(Position position, string color)
    {
        board[position.x, position.y] = new Queen("Queen", color, new Position(position.x, position.y));
    }

    public bool AreFigure(Position position, ref string name, ref string color)//в данной клетке фигура?
    {
        if (returnFigure(position) != null)
        {
            name = returnFigure(position).GetName();
            color = returnFigure(position).GetColor();
            return true;
        }
        else
            return false;
    }

    public List<Position> ListCoordinatesFigures(string color)//список фигур заданого цвета
    {
        List<Position> list = new List<Position>();

        for (int i = 0; i < cell; i++)
            for (int j = 0; j < cell; j++)
                if (board[i, j] != null)
                    if (board[i, j].GetColor() == color)
                        list.Add(new Position(i, j));

        return list;
    }

    public List<Position> ListCoordinatesMoves(Position figure)//список куда может сходит фигура, КОСЯК!!!!!!!!!!!!
        //БОЛЬШОЙ КОСЯК
    {
        List<Position> list = new List<Position>();

        List<Position> returnList = new List<Position>();

        if (returnFigure(figure) != null)
            list = returnFigure(figure).PossibleMoves(board, cell);

        if (returnFigure(figure).GetName() == "King")
            if (returnFigure(figure).GetColor() == "white")
                returnList = checkMoveKing(list, listCheckWhite, findKing("white").GetPosition());
            else
                returnList = checkMoveKing(list, listCheckBlack, findKing("black").GetPosition());
        else
        {
            if (returnFigure(figure).GetColor() == "white")
                returnList = checkMoveFigure(list, listCheckWhite, findKing("white").GetPosition());
            else
                returnList = checkMoveFigure(list, listCheckBlack, findKing("black").GetPosition());
        }
        //reviewCheckmate();
        return returnList;
    }

    public bool Move(Position figure, Position newPosition)
    {
        List<Position> list = ListCoordinatesMoves(figure);
        bool flag = false;
        for(int i = 0; i < list.Count; i++)
            if (list[i].x == newPosition.x && list[i].y == newPosition.y)
                flag = true;
        if (!flag)
            return false;

        if (returnFigure(figure) == null)
            return false;
        if (returnFigure(newPosition) != null)
        {
            if (returnFigure(figure).GetColor() == returnFigure(newPosition).GetColor())
                return false;
            else
            {
                if (returnFigure(newPosition).GetName() == "King")
                {
                    if (returnFigure(newPosition).GetColor() == "white")
                        killWhite = true;
                    if (returnFigure(newPosition).GetColor() == "black")
                        killBlack = true;
                }
            }
        }

        returnFigure(figure).Move(newPosition);
        swapFigure(figure, newPosition);

        if (returnFigure(newPosition).GetName() == "Pawn")
            if ((returnFigure(newPosition).GetColor() == "white" && newPosition.x == 7) || (returnFigure(newPosition).GetColor() == "black" && newPosition.x == 0))
                PawnToQueen(newPosition, returnFigure(newPosition).GetColor());
        //reviewCheckmate();
        PrivateWhoWin();
        return true;
    }

    public string IsCheckmate()
    {
        return checkmate;
    }

    public bool IsCheckWhite()
    {
        if (checkWhite != 0)
            return true;
        else
            return false;
    }
    public bool IsCheckBlack()
    {
        if (checkBlack != 0)
            return true;
        else
            return false;
    }

    public string IsKillKing()
    {
        if (killBlack)
            return "black";
        if (killWhite)
            return "white";
        return "";
    }
}
