using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        List<Figure> figures = new List<Figure>();
        
        const int rowHeight = 70;
        const int columnWidth = 73;
        string color1, color2;
        Figure clickedFigure;
        double clickedMousePosX, clickedMousePosY;
        double deltaX, deltaY;
        bool isFigureCliked = false;
        int[,] matrix = new int[8, 8];
        int[,] cloneMatrix = new int[8, 8];
        int[,] cloneCloneMatrix = new int[8, 8];
        Image[,] hints = new Image[8, 8];
        Image[,] image = new Image[8, 8];
        List<Figure> cloneFigures = new List<Figure>();

        public MainWindow()
        {
            InitializeComponent();

            

            StartGame();
        }

        public void StartGame()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    matrix[i, j] = 0;
                    cloneMatrix[i, j] = 0;
                    image[i, j] = new Image
                    {
                        Source = null,
                        Stretch = Stretch.Fill,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top,
                        Width = 74,
                        Height = 63
                    };
                    hints[i, j] = new Image
                    {
                        Source = null,
                        Stretch = Stretch.Fill,
                        HorizontalAlignment = HorizontalAlignment.Left,
                        VerticalAlignment = VerticalAlignment.Top,
                        Width = 74,
                        Height = 74
                    };
                    table.Children.Add(hints[i, j]);
                    table.Children.Add(image[i, j]);
                }
            }

            color1 = "white";
            color2 = "black";

            figures.Add(new Figure("pawn", 0, 6, color1, "Images/wp.png", 10));
            figures.Add(new Figure("pawn", 1, 6, color1, "Images/wp.png", 10));
            figures.Add(new Figure("pawn", 2, 6, color1, "Images/wp.png", 10));
            figures.Add(new Figure("pawn", 3, 6, color1, "Images/wp.png", 10));
            figures.Add(new Figure("pawn", 4, 6, color1, "Images/wp.png", 10));
            figures.Add(new Figure("pawn", 5, 6, color1, "Images/wp.png", 10));
            figures.Add(new Figure("pawn", 6, 6, color1, "Images/wp.png", 10));
            figures.Add(new Figure("pawn", 7, 6, color1, "Images/wp.png", 10));
            figures.Add(new Figure("pawn", 0, 1, color2, "Images/bp.png", -10));
            figures.Add(new Figure("pawn", 1, 1, color2, "Images/bp.png", -10));
            figures.Add(new Figure("pawn", 2, 1, color2, "Images/bp.png", -10));
            figures.Add(new Figure("pawn", 3, 1, color2, "Images/bp.png", -10));
            figures.Add(new Figure("pawn", 4, 1, color2, "Images/bp.png", -10));
            figures.Add(new Figure("pawn", 5, 1, color2, "Images/bp.png", -10));
            figures.Add(new Figure("pawn", 6, 1, color2, "Images/bp.png", -10));
            figures.Add(new Figure("pawn", 7, 1, color2, "Images/bp.png", -10));
            figures.Add(new Figure("bishop", 2, 7, color1, "Images/wb.png", 33));
            figures.Add(new Figure("bishop", 5, 7, color1, "Images/wb.png", 33));
            figures.Add(new Figure("bishop", 2, 0, color2, "Images/bb.png", -33));
            figures.Add(new Figure("bishop", 5, 0, color2, "Images/bb.png", -33));
            figures.Add(new Figure("rook", 7, 7, color1, "Images/wr.png", 50));
            figures.Add(new Figure("rook", 0, 7, color1, "Images/wr.png", 50));
            figures.Add(new Figure("rook", 0, 0, color2, "Images/br.png", -50));
            figures.Add(new Figure("rook", 7, 0, color2, "Images/br.png", -50));
            figures.Add(new Figure("knight", 1, 7, color1, "Images/wn.png", 30));
            figures.Add(new Figure("knight", 6, 7, color1, "Images/wn.png", 30));
            figures.Add(new Figure("knight", 1, 0, color2, "Images/bn.png", -30));
            figures.Add(new Figure("knight", 6, 0, color2, "Images/bn.png", -30));
            figures.Add(new Figure("queen", 3, 7, color1, "Images/wq.png", 90));
            figures.Add(new Figure("queen", 3, 0, color2, "Images/bq.png", -90));
            figures.Add(new Figure("king", 4, 7, color1, "Images/wk.png", 1000));
            figures.Add(new Figure("king", 4, 0, color2, "Images/bk.png", -1000));
            
            foreach (var figure in figures)
            {
                matrix[figure.x, figure.y] = figure.color == "white" ? 1 : -1;
                cloneMatrix[figure.x, figure.y] = figure.color == "white" ? 1 : -1;
                image[figure.x, figure.y].Source = new BitmapImage(new Uri(figure.imageSource, UriKind.Relative));
                image[figure.x, figure.y].Margin = new Thickness(columnWidth * figure.x, rowHeight * figure.y, 0, 0);
                
                Panel.SetZIndex(image[figure.x, figure.y], 2);

                cloneFigures.Add(new Figure(figure.name, figure.x, figure.y, figure.color, figure.imageSource, figure.weight));
            }
        }
        string source;

        private int GetWeightSum()
        {
            int weight = 0;

            foreach (var figure in figures)
                weight += figure.weight;

            return weight;
        }

        private string getLostFigureSource(int i, int j)
        {
            foreach(var figure in figures)
            {
                if (figure.x == i && figure.y == j)
                    return figure.imageSource;
            }

            return "";
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            clickedMousePosX = (e.GetPosition(this).X) / rowHeight;
            clickedMousePosY = (e.GetPosition(this).Y) / columnWidth;
            if (matrix[(int)clickedMousePosX, (int)clickedMousePosY] == 1)
            {
                isFigureCliked = true;
                clickedFigure = Figure.GetSelectedFigure(figures, (int)clickedMousePosX, (int)clickedMousePosY);
                source = clickedFigure.imageSource;
                if (clickedFigure != null)
                {
                    clickedFigure.AddHint(ref hints, ref cloneMatrix);
                    deltaX = e.GetPosition(this).X - image[(int)clickedMousePosX, (int)clickedMousePosY].Margin.Left;
                    deltaY = e.GetPosition(this).Y - image[(int)clickedMousePosX, (int)clickedMousePosY].Margin.Top;
                }
            }
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isFigureCliked = false;
            double mousePosX = (e.GetPosition(this).X) / columnWidth;
            double mousePosY = (e.GetPosition(this).Y) / rowHeight;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (cloneMatrix[i, j] == 2)
                    {
                        hints[i, j].Source = null;
                    }
                }
            }

            if (cloneMatrix[(int)mousePosX, (int)mousePosY] == 2)
            {
                Figure imFig = Figure.GetSelectedFigure(figures, (int)clickedMousePosX, (int)clickedMousePosY);
                
                Figure fig = GetOpponentFigureIndex((int)mousePosX, (int)mousePosY);
                if (fig != null)
                {
                   
                    if (fig.name == "king")
                    {
                        MessageBox.Show("Check");

                    }
                    else
                        figures.Remove(fig);
                   
                }
                imFig.x = (int)mousePosX;
                imFig.y = (int)mousePosY;
                image[(int)clickedMousePosX, (int)clickedMousePosY].Source = null;
                image[imFig.x, imFig.y].Source = new BitmapImage(new Uri(imFig.imageSource, UriKind.Relative));
                image[imFig.x, imFig.y].Margin = new Thickness(imFig.x * columnWidth, imFig.y * rowHeight, 0, 0);

                matrix[imFig.x, imFig.y] = 1;
                matrix[(int)clickedMousePosX, (int)clickedMousePosY] = 0;
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                        cloneMatrix[i, j] = matrix[i, j];
               
                Panel.SetZIndex(image[(int)clickedMousePosX, (int)clickedMousePosY], 2);

                OpponentMoves();
            }
            else
            {
                image[(int)clickedMousePosX, (int)clickedMousePosY].Margin = new Thickness((int)clickedMousePosX * columnWidth, (int)clickedMousePosY * rowHeight, 0, 0);
            }
        }

        private Figure GetOpponentFigureIndex(int x, int y)
        {
            foreach (var figure in figures)
            {
                if (figure.x == x && figure.y == y && figure.color == "black")
                    return figure;
            }

            return null;
        }

        private Figure GetFigureIndex(int x, int y)
        {
            foreach (var figure in figures)
            {
                if (figure.x == x && figure.y == y && figure.color == "white")
                    return figure;
            }

            return null;
        }

        private void OpponentMoves()
        {
            List<Figure> opponents = Figure.GetOpponentFigures(figures);
            Figure[] figs = opponents.ToArray();

            int? maxWeight = 0;
            int x = -1;
            int y = -1;
            int k = -1;
            for (int m = 0; m < figs.Length; m++)
            {
                opponents[m].AddOpponentHint(ref cloneMatrix);

                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                        if (cloneMatrix[i, j] == 2 && matrix[i, j] == 1)
                        {
                            if (maxWeight < GetFigureIndex(i, j)?.weight)
                            {
                                maxWeight = GetFigureIndex(i, j)?.weight;
                                x = i;
                                y = j;
                                k = m;
                            }
                        }

                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                        cloneMatrix[i, j] = matrix[i, j];
            }

            
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    cloneCloneMatrix[i, j] = matrix[i, j];

            if (maxWeight != 0)
            { 
               
               if (GetFigureIndex(x, y).name == "king")
                {
                    MessageBox.Show("Check");

                }
               else
                {
                    figures.Remove(GetFigureIndex(x, y));
                    image[opponents[k].x, opponents[k].y].Source = null;
                    image[x, y].Source = new BitmapImage(new Uri(opponents[k].imageSource, UriKind.Relative));
                    image[x, y].Margin = new Thickness(x * columnWidth, y * rowHeight, 0, 0);
                    matrix[x, y] = -1;
                    matrix[opponents[k].x, opponents[k].y] = 0;
                    GetOpponentFigureIndex(opponents[k].x, opponents[k].y).x = x;
                    GetOpponentFigureIndex(opponents[k].x, opponents[k].y).y = y;
                    Panel.SetZIndex(image[x, y], 2);
                }
                
            }
            else
            {
                heuristicMoves();
               
            }

            returnClone();
            returnCloneClone();
        }

        private void returnCloneClone()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    cloneCloneMatrix[i, j] = matrix[i, j];
        }

        private void returnClone()
        {
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    cloneMatrix[i, j] = matrix[i, j];
        }
        
        private int getHintCount(List<Figure> opFigs)
        {
            int count = 0;

            foreach (var fig in opFigs)
                fig.AddOpponentHint(ref cloneCloneMatrix);

            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    if (cloneCloneMatrix[i, j] == 2)
                    {
                        count++;
                        
                    }

            returnCloneClone();
            
            return count;
        }

        private void heuristicMoves()
        {
            List<Figure> opFigs = Figure.GetOpponentFigures(figures);

            int max = 0;
            int destX = 0;
            int destY = 0;
            int startX = 0;
            int startY = 0;
            
            foreach (var figure in opFigs)
            {
                figure.AddOpponentHint(ref cloneMatrix);

                int miniMax = 0;
                
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (cloneMatrix[i, j] == 2)
                        {
                            
                            int curX = figure.x;
                            int curY = figure.y;
                            
                            figure.x = i;
                            figure.y = j;
                            cloneMatrix[curX, curY] = 0;
                            cloneCloneMatrix[i, j] = -1;
                            
                            if (miniMax < getHintCount(opFigs))
                            {
                                
                                miniMax = getHintCount(opFigs);
                                destX = i;
                                destY = j;
                              
                            }
                                
                            figure.x = curX;
                            figure.y = curY;
                          
                        }
                    }

                }

                returnClone();
              
                if (max < miniMax)
                {
                    max = miniMax;
                    startX = figure.x;
                    startY = figure.y;
                }
            }
            
            Figure f = GetOpponentFigureIndex(startX, startY);
            
            image[startX, startY].Source = null;
            image[destX, destY].Source = new BitmapImage(new Uri(f.imageSource, UriKind.Relative));
            image[destX, destY].Margin = new Thickness(destX * columnWidth, destY * rowHeight, 0, 0);
            matrix[startX, startY] = 0;
            matrix[destX, destY] = -1;
            f.x = destX;
            f.y = destY;
            Panel.SetZIndex(image[destX, destY], 2);
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (isFigureCliked)
            {
                image[(int)clickedMousePosX, (int)clickedMousePosY].Margin = new Thickness(e.GetPosition(this).X - deltaX, e.GetPosition(this).Y - deltaY, 0, 0);
            }
        }
    }
}
