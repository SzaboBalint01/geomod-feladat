using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BezierDemo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetupCanvas();
        }

        private void SetupCanvas()
        {
            canvas.Background = Brushes.WhiteSmoke;
            canvas.MouseLeftButtonDown += Canvas_MouseLeftButtonDown;
            canvas.MouseMove += Canvas_MouseMove;
            canvas.MouseLeftButtonUp += Canvas_MouseLeftButtonUp;
            DrawLine();
        }

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point clickPoint = e.GetPosition(canvas);
            for (int i = 0; i < controlPointShapes.Count; i++)
            {
                Ellipse ellipse = controlPointShapes[i];
                Point centerPoint = new Point(
                    Canvas.GetLeft(ellipse) + ellipse.Width / 2,
                    Canvas.GetTop(ellipse) + ellipse.Height / 2
                );

                if (Distance(clickPoint, centerPoint) < 10)
                {
                    selectedPoint = ellipse;
                    selectedIndex = i;
                    return;
                }
            }
            AddControlPoint(clickPoint);
            UpdateLine();
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (selectedPoint != null)
            {
                Point mousePos = e.GetPosition(canvas);
                Canvas.SetLeft(selectedPoint, mousePos.X - selectedPoint.Width / 2);
                Canvas.SetTop(selectedPoint, mousePos.Y - selectedPoint.Height / 2);
                controlPoints[selectedIndex] = mousePos;
                UpdateLine();
                if (canvas.Children[canvas.Children.IndexOf(selectedPoint) + 1] is TextBlock label)
                {
                    Canvas.SetLeft(label, mousePos.X + 10);
                    Canvas.SetTop(label, mousePos.Y - 15);
                }
            }
        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            selectedPoint = null;
            selectedIndex = -1;
        }

        private void AddControlPoint(Point point)
        {
            controlPoints.Add(point);
            int pointIndex = controlPoints.Count - 1;
            Ellipse ellipse = new Ellipse
            {
                Width = 10,
                Height = 10,
                Fill = Brushes.Red,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };
            Canvas.SetLeft(ellipse, point.X - ellipse.Width / 2);
            Canvas.SetTop(ellipse, point.Y - ellipse.Height / 2);
            canvas.Children.Add(ellipse);
            controlPointShapes.Add(ellipse);
            TextBlock pointLabel = new TextBlock
            {
                Text = $"P{pointIndex}",
                Foreground = Brushes.Black,
                FontWeight = FontWeights.Bold
            };
            Canvas.SetLeft(pointLabel, point.X + 10);
            Canvas.SetTop(pointLabel, point.Y - 15);
            canvas.Children.Add(pointLabel);
        }

        private void UpdateLine()
        {
            controlPolyline.Points.Clear();
            foreach (Point p in controlPoints)
            {
                controlPolyline.Points.Add(p);
            }
        }

        private double Distance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            controlPoints.Clear();
            canvas.Children.Clear();
            controlPointShapes.Clear();
            controlPolyline.Points.Clear();
            DrawLine();
        }

        private void DrawLine()
        {
            controlPolyline = new Polyline
            {
                Stroke = Brushes.Gray,
                StrokeThickness = 1,
                StrokeDashArray = new DoubleCollection { 4, 2 }
            };
            canvas.Children.Add(controlPolyline);
        }

        private List<Point> controlPoints = new List<Point>();
        private List<Ellipse> controlPointShapes = new List<Ellipse>();
        private Polyline controlPolyline;
        private Ellipse selectedPoint = null;
        private int selectedIndex = -1;
    }
}