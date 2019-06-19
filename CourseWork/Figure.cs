using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace CourseWork
{
    class Figure
    {
        public string name { get; set; }
        public string color { get; set; }
        public string imageSource { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int weight { get; set; }
        
        public Figure(string name, int x, int y, string color, string imageSource, int weight)
        {
            this.name = name;
            this.imageSource = imageSource;
            this.color = color;
            this.x = x;
            this.y = y;
            this.weight = weight;
        }

        public static Figure GetSelectedFigure(List<Figure> list, int x, int y)
        {
            foreach(var l in list)
            {
                if (l.x == x && l.y == y && l.color != "black")
                {
                    return l;
                }
            }

            return null;
        }

        public static List<Figure> GetOpponentFigures(List<Figure> list)
        {
            List<Figure> figures = new List<Figure>();

            foreach (var figure in list)
            {
                if (figure.color == "black")
                    figures.Add(figure);
            }

            return figures;
        }

        public void AddOpponentHint(ref int[,] matrix)
        {
            switch (name)
            {
                case "pawn":
                    pawnHint(ref matrix);
                    break;
                case "king":
                    kingHint(ref matrix);
                    break;
                case "queen":
                    queenHint(ref matrix);
                    break;
                case "knight":
                    knightHint(ref matrix);
                    break;
                case "rook":
                    rookHint(ref matrix);
                    break;
                default:
                    bishopHint(ref matrix);
                    break;
            }
        }

        public void AddHint(ref Image[,] hints, ref int[,] matrix)
        {
            switch (name)
            {
                case "pawn":
                    pawnHint(ref hints, ref matrix);
                    break;
                case "king":
                    kingHint(ref hints, ref matrix);
                    break;
                case "queen":
                    queenHint(ref hints, ref matrix);
                    break;
                case "knight":
                    knightHint(ref hints, ref matrix);
                    break;
                case "rook":
                    rookHint(ref hints, ref matrix);
                    break;
                default:
                    bishopHint(ref hints, ref matrix);
                    break;
            }
        }

        private void bishopHint(ref Image[,] hints, ref int[,] matrix)
        {
            int k = x;
            int j = y;
            bool t = false;
            while (k < 7 && j < 7 && matrix[k + 1, j + 1] != 1)
            {

                if (matrix[k + 1, j + 1] == -1)
                    t = true;

                hints[k + 1, j + 1].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[k + 1, j + 1].Margin = new Thickness((k + 1) * 73, (j + 1) * 70, 0, 0);
                hints[k + 1, j + 1].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[k + 1, j + 1], 1);
                matrix[k + 1, j + 1] = 2;

                if (t)
                    break;
                k++;
                j++;
            }
            k = x;
            j = y;
            t = false;
            while (k > 0 && j > 0 && matrix[k - 1, j - 1] != 1)
            {
                if (matrix[k - 1, j - 1] == -1)
                    t = true;

                hints[k - 1, j - 1].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[k - 1, j - 1].Margin = new Thickness((k - 1) * 73, (j - 1) * 70, 0, 0);
                hints[k - 1, j - 1].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[k - 1, j - 1], 1);
                matrix[k - 1, j - 1] = 2;

                if (t)
                    break;

                k--;
                j--;
            }
            k = x;
            j = y;
            t = false;
            while (k > 0 && j < 7 && matrix[k - 1, j + 1] != 1 && t == false)
            {
                if (matrix[k - 1, j + 1] == -1)
                    t = true;

                hints[k - 1, j + 1].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[k - 1, j + 1].Margin = new Thickness((k - 1) * 73, (j + 1) * 70, 0, 0);
                hints[k - 1, j + 1].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[k - 1, j + 1], 1);
                matrix[k - 1, j + 1] = 2;

                if (t)
                    break;

                k--;
                j++;
            }
            k = x;
            j = y;
            t = false;
            while (k < 7 && j > 0 && matrix[k + 1, j - 1] != 1)
            {
                if (matrix[k + 1, j - 1] == -1)
                {
                    t = true;
                }

                hints[k + 1, j - 1].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[k + 1, j - 1].Margin = new Thickness((k + 1) * 73, (j - 1) * 70, 0, 0);
                hints[k + 1, j - 1].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[k + 1, j - 1], 1);
                matrix[k + 1, j - 1] = 2;

                if (t)
                    break;
                k++;
                j--;
            }
        }

        private void rookHint(ref Image[,] hints, ref int[,] matrix)
        {
            int k = y;
            bool t = false;
            while (k < 7 && matrix[x, k + 1] != 1)
            {

                if (matrix[x, k + 1] == -1)
                    t = true;

                hints[x, k + 1].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[x, k + 1].Margin = new Thickness(x * 73, (k + 1) * 70, 0, 0);
                hints[x, k + 1].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[x, k + 1], 1);
                matrix[x, k + 1] = 2;

                if (t)
                    break;

                k++;
            }
            k = y;
            t = false;
            while (k > 0 && matrix[x, k - 1] != 1)
            {

                if (matrix[x, k - 1] == -1)
                    t = true;

                hints[x, k - 1].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[x, k - 1].Margin = new Thickness(x * 73, (k - 1) * 70, 0, 0);
                hints[x, k - 1].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[x, k - 1], 1);
                matrix[x, k - 1] = 2;

                if (t)
                    break;

                k--;
            }
            k = x;
            t = false;
            while (k > 0 && matrix[k - 1, y] != 1)
            {

                if (matrix[k - 1, y] == -1)
                    t = true;

                hints[k - 1, y].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[k - 1, y].Margin = new Thickness((k - 1) * 73, y * 70, 0, 0);
                hints[k - 1, y].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[k - 1, y], 1);
                matrix[k - 1, y] = 2;

                if (t)
                    break;

                k--;
            }
            k = x;
            t = false;

            while (k < 7 && matrix[k + 1, y] != 1)
            {

                if (matrix[k + 1, y] == -1)
                    t = true;

                hints[k + 1, y].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[k + 1, y].Margin = new Thickness((k + 1) * 73, y * 70, 0, 0);
                hints[k + 1, y].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[k + 1, y], 1);
                matrix[k + 1, y] = 2;

                if (t)
                    break;

                k++;

            }
        }

        private void knightHint(ref Image[,] hints, ref int[,] matrix)
        {
            if (x > 0 && y > 1 && matrix[x - 1, y - 2] != 1)
            {
                hints[x - 1, y - 2].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[x - 1, y - 2].Margin = new Thickness((x - 1) * 73, (y - 2) * 70, 0, 0);
                hints[x - 1, y - 2].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[x - 1, y - 2], 1);
                matrix[x - 1, y - 2] = 2;
            }

            if (x > 1 && y > 0 && matrix[x - 2, y - 1] != 1)
            {
                hints[x - 2, y - 1].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[x - 2, y - 1].Margin = new Thickness((x - 2) * 73, (y - 1) * 70, 0, 0);
                hints[x - 2, y - 1].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[x - 2, y - 1], 1);
                matrix[x - 2, y - 1] = 2;
            }

            if (x < 6 && y > 0 && matrix[x + 2, y - 1] != 1)
            {
                hints[x + 2, y - 1].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[x + 2, y - 1].Margin = new Thickness((x + 2) * 73, (y - 1) * 70, 0, 0);
                hints[x + 2, y - 1].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[x + 2, y - 1], 1);
                matrix[x + 2, y - 1] = 2;
            }

            if (x < 7 && y > 1 && matrix[x + 1, y - 2] != 1)
            {
                hints[x + 1, y - 2].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[x + 1, y - 2].Margin = new Thickness((x + 1) * 73, (y - 2) * 70, 0, 0);
                hints[x + 1, y - 2].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[x + 1, y - 2], 1);
                matrix[x + 1, y - 2] = 2;
            }

            if (x < 7 && y < 6 && matrix[x + 1, y + 2] != 1)
            {
                hints[x + 1, y + 2].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[x + 1, y + 2].Margin = new Thickness((x + 1) * 73, (y + 2) * 70, 0, 0);
                hints[x + 1, y + 2].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[x + 1, y + 2], 1);
                matrix[x + 1, y + 2] = 2;
            }

            if (x < 6 && y < 7 && matrix[x + 2, y + 1] != 1)
            {
                hints[x + 2, y + 1].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[x + 2, y + 1].Margin = new Thickness((x + 2) * 73, (y + 1) * 70, 0, 0);
                hints[x + 2, y + 1].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[x + 2, y + 1], 1);
                matrix[x + 2, y + 1] = 2;
            }

            if (x > 1 && y < 7 && matrix[x - 2, y + 1] != 1)
            {
                hints[x - 2, y + 1].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[x - 2, y + 1].Margin = new Thickness((x - 2) * 73, (y + 1) * 70, 0, 0);
                hints[x - 2, y + 1].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[x - 2, y + 1], 1);
                matrix[x - 2, y + 1] = 2;
            }

            if (x > 0 && y < 6 && matrix[x - 1, y + 2] != 1)
            {
                hints[x - 1, y + 2].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[x - 1, y + 2].Margin = new Thickness((x - 1) * 73, (y + 2) * 70, 0, 0);
                hints[x - 1, y + 2].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[x - 1, y + 2], 1);
                matrix[x - 1, y + 2] = 2;
            }


        }

        private void queenHint(ref Image[,] hints, ref int[,] matrix)
        {
            int k = y;
            bool t = false;
            while (k < 7 && matrix[x, k + 1] != 1)
            {
                
                if (matrix[x, k + 1] == -1)
                    t = true;

                hints[x, k + 1].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[x, k + 1].Margin = new Thickness(x * 73, (k + 1) * 70, 0, 0);
                hints[x, k + 1].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[x, k + 1], 1);
                matrix[x, k + 1] = 2;

                if (t)
                    break;

                k++;
            }
            k = y;
            t = false;
            while (k > 0 && matrix[x, k - 1] != 1)
            {
                
                if (matrix[x, k - 1] == -1)
                    t = true;

                hints[x, k - 1].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[x, k - 1].Margin = new Thickness(x * 73, (k - 1) * 70, 0, 0);
                hints[x, k - 1].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[x, k - 1], 1);
                matrix[x, k - 1] = 2;

                if (t)
                    break;

                k--;
            }
            k = x;
            t = false;
            while (k > 0 && matrix[k - 1, y] != 1)
            {
                
                if (matrix[k - 1, y] == -1)
                    t = true;

                hints[k - 1, y].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[k - 1, y].Margin = new Thickness((k - 1) * 73, y * 70, 0, 0);
                hints[k - 1, y].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[k - 1, y], 1);
                matrix[k - 1, y] = 2;

                if (t)
                    break;

                k--;
            }
            k = x;
            t = false;

            while (k < 7 && matrix[k + 1, y] != 1)
            {
               
                if (matrix[k + 1, y] == -1)
                    t = true;

                hints[k + 1, y].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[k + 1, y].Margin = new Thickness((k + 1) * 73, y * 70, 0, 0);
                hints[k + 1, y].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[k + 1, y], 1);
                matrix[k + 1, y] = 2;

                if (t)
                    break;

                k++;
               
            }
            k = x;
            int j = y;
            t = false;
            while (k < 7 && j < 7 && matrix[k + 1, j + 1] != 1)
            {
                
                if (matrix[k + 1, j + 1] == -1)
                    t = true;

                hints[k + 1, j + 1].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[k + 1, j + 1].Margin = new Thickness((k + 1) * 73, (j + 1) * 70, 0, 0);
                hints[k + 1, j + 1].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[k + 1, j + 1], 1);
                matrix[k + 1, j + 1] = 2;

                if (t)
                    break;
                k++;
                j++;
            }
            k = x;
            j = y;
            t = false;
            while (k > 0 && j > 0 && matrix[k - 1, j - 1] != 1)
            {
                if (matrix[k - 1, j - 1] == -1)
                    t = true;

                hints[k - 1, j - 1].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[k - 1, j - 1].Margin = new Thickness((k - 1) * 73, (j - 1) * 70, 0, 0);
                hints[k - 1, j - 1].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[k - 1, j - 1], 1);
                matrix[k - 1, j - 1] = 2;

                if (t)
                    break;

                k--;
                j--;
            }
            k = x;
            j = y;
            t = false;
            while (k > 0 && j < 7 && matrix[k - 1, j + 1] != 1 && t == false)
            {
                if (matrix[k - 1, j + 1] == -1)
                    t = true;

                hints[k - 1, j + 1].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[k - 1, j + 1].Margin = new Thickness((k - 1) * 73, (j + 1) * 70, 0, 0);
                hints[k - 1, j + 1].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[k - 1, j + 1], 1);
                matrix[k - 1, j + 1] = 2;

                if (t)
                    break;

                k--;
                j++;
            }
            k = x;
            j = y;
            t = false;
            while (k < 7 && j > 0 && matrix[k + 1, j - 1] != 1)
            {
                if (matrix[k + 1, j - 1] == -1)
                {
                    t = true;
                }
                
                hints[k + 1, j - 1].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[k + 1, j - 1].Margin = new Thickness((k + 1) * 73, (j - 1) * 70, 0, 0);
                hints[k + 1, j - 1].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[k + 1, j - 1], 1);
                matrix[k + 1, j - 1] = 2;

                if (t)
                    break;
                k++;
                j--;
            }
        }

        private void kingHint(ref Image[,] hints, ref int[,] matrix)
        {
            
            if (y < 7 && matrix[x, y + 1] != 1)
            {
                hints[x, y + 1].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[x, y + 1].Margin = new Thickness(x * 73, (y + 1) * 70, 0, 0);
                hints[x, y + 1].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[x, y + 1], 1);

                matrix[x, y + 1] = 2;
            }
            if (y > 0 && matrix[x, y - 1] != 1)
            {
                hints[x, y - 1].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[x, y - 1].Margin = new Thickness(x * 73, (y - 1) * 70, 0, 0);
                hints[x, y - 1].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[x, y - 1], 1);
                matrix[x, y - 1] = 2;
            }
            if (x > 0 && matrix[x - 1, y] != 1)
            {
                hints[x - 1, y].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[x - 1, y].Margin = new Thickness((x - 1) * 73, y * 70, 0, 0);
                hints[x - 1, y].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[x - 1, y], 1);
                matrix[x - 1, y] = 2;
            }
            if (x < 7 && matrix[x + 1, y] != 1)
            {
                hints[x + 1, y].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[x + 1, y].Margin = new Thickness((x + 1) * 73, y * 70, 0, 0);
                hints[x + 1, y].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[x + 1, y], 1);
                matrix[x + 1, y] = 2;
            }
            if (x < 7 && y < 7 && matrix[x + 1, y + 1] != 1)
            {
                hints[x + 1, y + 1].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[x + 1, y + 1].Margin = new Thickness((x + 1) * 73, (y + 1) * 70, 0, 0);
                hints[x + 1, y + 1].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[x + 1, y + 1], 1);
                matrix[x + 1, y + 1] = 2;
            }
            if (x > 0 && y > 0 && matrix[x - 1, y - 1] != 1)
            {
                hints[x - 1, y - 1].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[x - 1, y - 1].Margin = new Thickness((x - 1) * 73, (y - 1) * 70, 0, 0);
                hints[x - 1, y - 1].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[x - 1, y - 1], 1);
                matrix[x - 1, y - 1] = 2;
            }
            if (x > 0 && y < 7 && matrix[x - 1, y + 1] != 1)
            {
                hints[x - 1, y + 1].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[x - 1, y + 1].Margin = new Thickness((x - 1) * 73, (y + 1) * 70, 0, 0);
                hints[x - 1, y + 1].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[x - 1, y + 1], 1);
                matrix[x - 1, y + 1] = 2;
            }
            if (x < 7 && y > 0 && matrix[x + 1, y - 1] != 1)
            {
                hints[x + 1, y - 1].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[x + 1, y - 1].Margin = new Thickness((x + 1) * 73, (y - 1) * 70, 0, 0);
                hints[x + 1, y - 1].Stretch = System.Windows.Media.Stretch.Fill;
                Panel.SetZIndex(hints[x + 1, y - 1], 1);
                matrix[x + 1, y - 1] = 2;
            }
        }

        private void pawnHint(ref Image[,] hints, ref int[,] matrix)
        {
            if (matrix[x, y - 1] != 1 && matrix[x, y - 1] != -1)
            {
                hints[x, y - 1].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[x, y - 1].Margin = new Thickness(x * 73, (y - 1) * 70, 0, 0);
                hints[x, y - 1].Stretch = System.Windows.Media.Stretch.Fill;
                matrix[x, y - 1] = 2;

                if (y - 1 > 4 && matrix[x, y - 2] != 1 && matrix[x, y - 2] != -1)
                {
                    hints[x, y - 2].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                    hints[x, y - 2].Margin = new Thickness(x * 73, (y - 2) * 70, 0, 0);
                    hints[x, y - 2].Stretch = System.Windows.Media.Stretch.Fill;
                    matrix[x, y - 2] = 2;
                }

                

               
            }
            if (x < 7 && matrix[x + 1, y - 1] == -1)
            {
                hints[x + 1, y - 1].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[x + 1, y - 1].Margin = new Thickness((x + 1) * 73, (y - 1) * 70, 0, 0);
                hints[x + 1, y - 1].Stretch = System.Windows.Media.Stretch.Fill;
                matrix[x + 1, y - 1] = 2;
            }
            if (x > 0 && matrix[x - 1, y - 1] == -1)
            {
                hints[x - 1, y - 1].Source = new BitmapImage(new Uri("hint.png", UriKind.Relative));
                hints[x - 1, y - 1].Margin = new Thickness((x - 1) * 73, (y - 1) * 70, 0, 0);
                hints[x - 1, y - 1].Stretch = System.Windows.Media.Stretch.Fill;
                matrix[x - 1, y - 1] = 2;
            }
        }

        private void bishopHint(ref int[,] matrix)
        {
            int k = x;
            int j = y;
            bool t = false;
            while (k < 7 && j < 7 && matrix[k + 1, j + 1] != -1)
            {

                if (matrix[k + 1, j + 1] == 1)
                    t = true;

                matrix[k + 1, j + 1] = 2;

                if (t)
                    break;
                k++;
                j++;
            }
            k = x;
            j = y;
            t = false;
            while (k > 0 && j > 0 && matrix[k - 1, j - 1] != -1)
            {
                if (matrix[k - 1, j - 1] == 1)
                    t = true;
                
                matrix[k - 1, j - 1] = 2;

                if (t)
                    break;

                k--;
                j--;
            }
            k = x;
            j = y;
            t = false;
            while (k > 0 && j < 7 && matrix[k - 1, j + 1] != -1 && t == false)
            {
                if (matrix[k - 1, j + 1] == 1)
                    t = true;

                matrix[k - 1, j + 1] = 2;

                if (t)
                    break;

                k--;
                j++;
            }
            k = x;
            j = y;
            t = false;
            while (k < 7 && j > 0 && matrix[k + 1, j - 1] != -1)
            {
                if (matrix[k + 1, j - 1] == 1)
                {
                    t = true;
                }

                matrix[k + 1, j - 1] = 2;

                if (t)
                    break;
                k++;
                j--;
            }
        }

        private void rookHint(ref int[,] matrix)
        {
            int k = y;
            bool t = false;
            while (k < 7 && matrix[x, k + 1] != -1)
            {

                if (matrix[x, k + 1] == 1)
                    t = true;

                matrix[x, k + 1] = 2;

                if (t)
                    break;

                k++;
            }
            k = y;
            t = false;
            while (k > 0 && matrix[x, k - 1] != -1)
            {

                if (matrix[x, k - 1] == 1)
                    t = true;

                matrix[x, k - 1] = 2;

                if (t)
                    break;

                k--;
            }
            k = x;
            t = false;
            while (k > 0 && matrix[k - 1, y] != -1)
            {

                if (matrix[k - 1, y] == 1)
                    t = true;

                matrix[k - 1, y] = 2;

                if (t)
                    break;

                k--;
            }
            k = x;
            t = false;

            while (k < 7 && matrix[k + 1, y] != -1)
            {

                if (matrix[k + 1, y] == 1)
                    t = true;

                matrix[k + 1, y] = 2;

                if (t)
                    break;

                k++;

            }
        }

        private void knightHint(ref int[,] matrix)
        {
            if (x > 0 && y > 1 && matrix[x - 1, y - 2] != -1)
            {
                matrix[x - 1, y - 2] = 2;
            }

            if (x > 1 && y > 0 && matrix[x - 2, y - 1] != -1)
            {
                matrix[x - 2, y - 1] = 2;
            }

            if (x < 6 && y > 0 && matrix[x + 2, y - 1] != -1)
            {
                matrix[x + 2, y - 1] = 2;
            }

            if (x < 7 && y > 1 && matrix[x + 1, y - 2] != -1)
            {
                matrix[x + 1, y - 2] = 2;
            }

            if (x < 7 && y < 6 && matrix[x + 1, y + 2] != -1)
            {
                matrix[x + 1, y + 2] = 2;
            }

            if (x < 6 && y < 7 && matrix[x + 2, y + 1] != -1)
            {
                matrix[x + 2, y + 1] = 2;
            }

            if (x > 1 && y < 7 && matrix[x - 2, y + 1] != -1)
            {
                matrix[x - 2, y + 1] = 2;
            }

            if (x > 0 && y < 6 && matrix[x - 1, y + 2] != -1)
            {
                matrix[x - 1, y + 2] = 2;
            }
        }

        private void queenHint(ref int[,] matrix)
        {
            int k = y;
            bool t = false;
            while (k < 7 && matrix[x, k + 1] != -1)
            {

                if (matrix[x, k + 1] == 1)
                    t = true;

                matrix[x, k + 1] = 2;

                if (t)
                    break;

                k++;
            }
            k = y;
            t = false;
            while (k > 0 && matrix[x, k - 1] != -1)
            {

                if (matrix[x, k - 1] == 1)
                    t = true;

                matrix[x, k - 1] = 2;

                if (t)
                    break;

                k--;
            }
            k = x;
            t = false;
            while (k > 0 && matrix[k - 1, y] != -1)
            {

                if (matrix[k - 1, y] == 1)
                    t = true;

                matrix[k - 1, y] = 2;

                if (t)
                    break;

                k--;
            }
            k = x;
            t = false;

            while (k < 7 && matrix[k + 1, y] != -1)
            {

                if (matrix[k + 1, y] == 1)
                    t = true;

                matrix[k + 1, y] = 2;

                if (t)
                    break;

                k++;

            }
            k = x;
            int j = y;
            t = false;
            while (k < 7 && j < 7 && matrix[k + 1, j + 1] != -1)
            {

                if (matrix[k + 1, j + 1] == 1)
                    t = true;

                matrix[k + 1, j + 1] = 2;

                if (t)
                    break;
                k++;
                j++;
            }
            k = x;
            j = y;
            t = false;
            while (k > 0 && j > 0 && matrix[k - 1, j - 1] != -1)
            {
                if (matrix[k - 1, j - 1] == 1)
                    t = true;

                matrix[k - 1, j - 1] = 2;

                if (t)
                    break;

                k--;
                j--;
            }
            k = x;
            j = y;
            t = false;
            while (k > 0 && j < 7 && matrix[k - 1, j + 1] != -1 && t == false)
            {
                if (matrix[k - 1, j + 1] == 1)
                    t = true;

                matrix[k - 1, j + 1] = 2;

                if (t)
                    break;

                k--;
                j++;
            }
            k = x;
            j = y;
            t = false;
            while (k < 7 && j > 0 && matrix[k + 1, j - 1] != -1)
            {
                if (matrix[k + 1, j - 1] == 1)
                {
                    t = true;
                }

                matrix[k + 1, j - 1] = 2;

                if (t)
                    break;
                k++;
                j--;
            }
        }

        private void kingHint(ref int[,] matrix)
        {

            if (y < 7 && matrix[x, y + 1] != -1)
            {
                matrix[x, y + 1] = 2;
            }
            if (y > 0 && matrix[x, y - 1] != -1)
            {
                matrix[x, y - 1] = 2;
            }
            if (x > 0 && matrix[x - 1, y] != -1)
            {
                matrix[x - 1, y] = 2;
            }
            if (x < 7 && matrix[x + 1, y] != -1)
            {
                matrix[x + 1, y] = 2;
            }
            if (x < 7 && y < 7 && matrix[x + 1, y + 1] != -1)
            {
                matrix[x + 1, y + 1] = 2;
            }
            if (x > 0 && y > 0 && matrix[x - 1, y - 1] != -1)
            {
                matrix[x - 1, y - 1] = 2;
            }
            if (x > 0 && y < 7 && matrix[x - 1, y + 1] != -1)
            {
                matrix[x - 1, y + 1] = 2;
            }
            if (x < 7 && y > 0 && matrix[x + 1, y - 1] != -1)
            {
                matrix[x + 1, y - 1] = 2;
            }
        }

        private void pawnHint(ref int[,] matrix)
        {
            if (matrix[x, y + 1] != -1 && matrix[x, y + 1] != 1)
            {
                matrix[x, y + 1] = 2;

                if (y + 1 < 3 && matrix[x, y + 2] != -1 && matrix[x, y + 2] != 1)
                {
                    matrix[x, y + 2] = 2;
                }
            }

            if (x < 7 && matrix[x + 1, y + 1] == 1)
                matrix[x + 1, y + 1] = 2;

            if (x > 0 && matrix[x - 1, y + 1] == 1)
                matrix[x - 1, y + 1] = 2;
        }
    }
}
