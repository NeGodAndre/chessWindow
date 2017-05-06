using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

struct PossibleCourse
{
    public Position position;
    public Position newPosition;
    public float possible;
}

class AI
{
    private string myColor;
    private string colorEnemy;
    private int amountIterations;

    private ProbabilityFigures p;

    public AI(string color, int amountIterations)
    {
        this.myColor = color;
        if (color == "white")
            this.colorEnemy = "black";
        else
            this.colorEnemy = "white";
        this.amountIterations = amountIterations;

        p = new ProbabilityFigures(myColor, colorEnemy);
    }

    public void MakeMove(Board board, ref Position positionFigure, ref Position newPosition)
    {
        p.ValuesChange(board);

        float possible = 0;
        Calculation(board, ref positionFigure, ref newPosition, ref possible, amountIterations);

        p.Actual(board, positionFigure, newPosition);
    }

    private void Calculation(Board board, ref Position positionFigure, ref Position newPosition, ref float possible, int amount)
    {
        if (board.findKing(myColor) == null)
        {
            possible = -1;
            return;
        }
        if (board.findKing(colorEnemy) == null)
        {
            possible = 1;
            return;
        }

        List<PossibleCourse> course = ReturnCourse(board, myColor);

        for (int i = 0; i < course.Count; i++)
        {
            PossibleCourse buf = course[i];

            buf.possible = p.Probability(board, course[i].position, course[i].newPosition);
             /*string bufName = "";
            string bufColor = "";
            if (board.AreFigure(course[i].newPosition, ref bufName, ref bufColor))
            {
                if (bufName == "King")
                    buf.possible += 0.6f;
                else
                    buf.possible += 0.4f;
            }
            else
                buf.possible += 0.2f;*/
            course[i] = buf;
        }

        if (amount > 1)
        {
            if (board.findKing(colorEnemy) == null)
            {
                possible = 1;
                return;
            }

            for (int i = 0; i < course.Count; i++)
            {
                Board board1 = new Board(board);
                board1.Move(course[i].position, course[i].newPosition);
                if (board.findKing(colorEnemy) == null)
                {
                    possible = 1;
                    return;
                }
                List<PossibleCourse> courseEnemy = ReturnCourse(board1, colorEnemy);
                for (int j = 0; j < courseEnemy.Count; j++)
                {
                    Board board2 = new Board(board1);
                    board2.Move(courseEnemy[j].position, courseEnemy[j].newPosition);
                    //
                    Position p = new Position();
                    Position newP = new Position();
                    float pos = 0;
                    //
                    if (board.findKing(myColor) != null)
                        Calculation(board2, ref p, ref newP, ref pos, amount - 1);
                    else
                        pos = -1;
                    PossibleCourse pc1 = new PossibleCourse();
                    pc1.position = courseEnemy[j].position;
                    pc1.newPosition = courseEnemy[j].newPosition;
                    pc1.possible = pos;
                    courseEnemy[j] = pc1;
                }
                //Normalization(ref courseEnemy);
                if (courseEnemy.Count != 0)
                {
                    courseEnemy.Sort((a, b) => a.possible.CompareTo(b.possible));

                    List<PossibleCourse> rezult = new List<PossibleCourse>();
                    rezult.Add(courseEnemy[courseEnemy.Count - 1]);
                    for (int j = courseEnemy.Count - 2; j >= 0; j--)
                        if (course[i].possible == courseEnemy[courseEnemy.Count - 1].possible)
                            rezult.Add(course[i]);
                        else
                            break;

                    Random r = new Random();

                    PossibleCourse pc = course[i];
                    pc.possible += rezult[r.Next(0, rezult.Count)].possible;
                    course[i] = pc;
                }
            }
        }
       // Normalization(ref course);
        if(amount == amountIterations)
            ReturnFigure(course, ref positionFigure, ref newPosition);
    }

    private List<PossibleCourse> ReturnCourse(Board board, string color)
    {
        List<PossibleCourse> course = new List<PossibleCourse>();
        List<Position> listFigure = board.ListCoordinatesFigures(color);
        for (int i = 0; i < listFigure.Count; i++)
        {
            List<Position> move = board.ListCoordinatesMoves(listFigure[i]);
            for (int j = 0; j < move.Count; j++)
            {
                PossibleCourse buf = new PossibleCourse();
                buf.position = listFigure[i];
                buf.newPosition = move[j];
                buf.possible = 0.0f;
                course.Add(buf);
            }
        }
        return course;
    }

    private void ReturnFigure(List<PossibleCourse> course, ref Position positionFigure, ref Position newPosition)
    {
        course.Sort((a, b) => a.possible.CompareTo(b.possible));

        List<PossibleCourse> rezult = new List<PossibleCourse>();
        rezult.Add(course[course.Count - 1]);
        for (int i = course.Count - 2; i >= 0; i--)
            if (course[i].possible == course[course.Count - 1].possible)
                rezult.Add(course[i]);
            else
                break;

        Random r = new Random();
        int index = r.Next(0, rezult.Count);

        positionFigure.x = rezult[index].position.x;
        positionFigure.y = rezult[index].position.y;
        newPosition.x = rezult[index].newPosition.x;
        newPosition.y = rezult[index].newPosition.y;
    }

    private void Normalization(ref List<PossibleCourse> course)
    {
        float sum = 0;
        for(int i = 0; i < course.Count; i++)
            sum += course[i].possible;

        for(int i=0; i < course.Count; i++)
        {
            PossibleCourse posC = course[i];
            posC.possible /= sum;
            course[i] = posC;
        }
    }
    }
