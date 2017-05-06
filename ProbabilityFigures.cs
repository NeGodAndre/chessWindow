using System;
using System.Collections.Generic;
using System.IO;

struct ProbabilityFigure
{
    public string name;
    public float course;
    public float attack;
    public float attackKing;
}

class ProbabilityFigures
{
    private const float index = 0.003f;

    private ProbabilityFigure[] probFigur;

    private string[] nameFigure;
    string fileName = "txt.txt";

    private Board board1;

    private ProbabilityFigure actualProbFigure;
    Position actualFigure;
    Position actualNewPosition;

    private string myColor;
    private string colorEnemy;

    public ProbabilityFigures(string myColor, string colorEnemy)
    {
        this.myColor = myColor;
        this.colorEnemy = colorEnemy;
        actualProbFigure.name = "";
        probFigur = new ProbabilityFigure[6];

        string[] buf = { "Pawn" , "Bishop", "Rook", "Knight", "Queen", "King" };
        nameFigure = buf;

        OpenFile();
    }

    public float Probability(Board board, Position figure, Position newFigure)
    {
        ProbabilityFigure buf = new ProbabilityFigure();

        string nameFigure = "";
        string colorFigure = "";

        if (!board.AreFigure(figure, ref nameFigure, ref colorFigure))
            return 0;
        else
        {
            bool p = false;
            for (int i = 0; i < 6; i++)
                if (nameFigure == probFigur[i].name)
                {
                    buf = probFigur[i];
                    p = true;
                }
            if (p)
                return 0;

            string nameFigure1 = "";
            string colorFigure1 = "";
            if (!board.AreFigure(newFigure, ref nameFigure1, ref colorFigure1))
                return buf.course;
            else
            {
                if (nameFigure1 == "King")
                    return buf.attackKing;
                else
                    return buf.attack;
            }
        }
    }

    public void ValuesChange(Board board)
    {
        if (actualProbFigure.name != "")
        {
            Board board2 = new Board(board);

            int numBoard1My = board1.ListCoordinatesFigures(myColor).Count;
            int numBoard1Enemy = board1.ListCoordinatesFigures(colorEnemy).Count;

            int numBoard2My = board2.ListCoordinatesFigures(myColor).Count;
            int numBoard2Enemy = board2.ListCoordinatesFigures(colorEnemy).Count;
            ///////////////////////
            float k = 0;
            if (numBoard1My > numBoard2My)
                k = -index;
            else
                k = index;
            ///////////////////////
            string nameFigure1 = "";
            string colorFigure1 = "";
            if (!board.AreFigure(actualNewPosition, ref nameFigure1, ref colorFigure1))
                actualProbFigure.course += k;
            else
            {
                if (nameFigure1 == "King")
                    actualProbFigure.attackKing += 3 * k;
                else
                    actualProbFigure.attack += 2 * k;
            }
            ValueCorrection();
            WriteFile();
        }
    }

    public void Actual(Board board, Position figure, Position newFigure)
    {
        board1 = new Board(board);

        actualFigure.x = figure.x;
        actualFigure.y = figure.y;

        actualNewPosition.x = newFigure.x;
        actualNewPosition.y = newFigure.y;

        string nameFigure = "";
        string colorFigure = "";

        board1.AreFigure(actualFigure, ref nameFigure, ref colorFigure);
        for (int i = 0; i < 6; i++)
            if (nameFigure == probFigur[i].name)
                actualProbFigure = probFigur[i];
    }

    private void ValueCorrection()
    {
        for (int i = 0; i < 6; i++)
            if (actualProbFigure.name == probFigur[i].name)
                probFigur[i] = actualProbFigure;
    }

    private void OpenFile()
    {
        string[] str;
        if (!File.Exists(fileName))
        {
            Alt();
            WriteFile();
        }
        else
        {
            str = File.ReadAllLines(fileName, System.Text.Encoding.UTF8); //sr.ReadToEnd();

            float[,] buf = new float[6, 3];
            for (int i = 0; i < 6; i++)
            {
                int k = 0;
                int kk = k;
                for (int j = 0; j < 3; j++)
                {
                    for (k = kk; k < str[i].Length ? str[i][k] != ' ' : false; k++) ;

                    buf[i, j] = float.Parse(str[i].Substring(kk, k - kk));
                    kk = k + 1;
                }
            }

            probFigur = new ProbabilityFigure[6];
            for (int i = 0; i < 6; i++)
            {
                probFigur[i].name = nameFigure[i];
                probFigur[i].course = buf[i, 0];
                probFigur[i].attack = buf[i, 1];
                probFigur[i].attackKing = buf[i, 2];
            }
        }
    }

    private void WriteFile()
    {
        string str = "";

        for (int i = 0; i < 6; i++)
            str += probFigur[i].course.ToString() + " " + probFigur[i].attack.ToString() + " " + probFigur[i].attackKing.ToString() + Environment.NewLine;
        File.WriteAllText(fileName, str);
    }

    private void Alt()
    {
        probFigur[0].name = "Pawn";
        probFigur[0].course = 0.2f;
        probFigur[0].attack = 0.4f;
        probFigur[0].attackKing = 0.6f;

        probFigur[1].name = "Bishop";
        probFigur[1].course = 0.2f;
        probFigur[1].attack = 0.4f;
        probFigur[1].attackKing = 0.6f;

        probFigur[2].name = "Rook";
        probFigur[2].course = 0.2f;
        probFigur[2].attack = 0.4f;
        probFigur[2].attackKing = 0.6f;

        probFigur[3].name = "Knight";
        probFigur[3].course = 0.2f;
        probFigur[3].attack = 0.4f;
        probFigur[3].attackKing = 0.6f;

        probFigur[4].name = "Queen";
        probFigur[4].course = 0.2f;
        probFigur[4].attack = 0.4f;
        probFigur[4].attackKing = 0.6f;

        probFigur[5].name = "King";
        probFigur[5].course = 0.2f;
        probFigur[5].attack = 0.4f;
        probFigur[5].attackKing = 0.6f;
    }
}
