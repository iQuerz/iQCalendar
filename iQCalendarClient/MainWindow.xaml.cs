using iQCalendarClient.Business;
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

namespace iQCalendarClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CalendarCellAccess[,] Cells;
        Manager Manager;

        public MainWindow()
        {
            InitializeComponent();
            Manager = new Manager();
            Loaded += Client_Loaded;
            LeftArrowButton.Click += LeftArrow_Click;
            RightArrowButton.Click += RightArrow_Click;
        }

        private void Client_Loaded(object sender, EventArgs e)
        {
            
            Cells = getCellMatrix();
           
            Manager.loadTestData();
            Manager.CurrentMonth = DateTime.Now.Month;
            Manager.CurrentYear = DateTime.Now.Year;
            Load_Calendar();
            //PreviewMouseDown += MouseDown;
        }

        private void Load_Calendar()
        {
            Cells[2, 2].Date.Background = Brushes.Orange;
            Cells[2, 2].Date.FontWeight = FontWeights.Bold;
            Cells[2, 2].Border.ToolTip = "Danasnji Dan";
            var firstDayOfMonth = new DateTime(Manager.CurrentYear, Manager.CurrentMonth, 1);


            int max = DateTime.DaysInMonth(Manager.CurrentYear, Manager.CurrentMonth);

            int tmpYear, tmpMonth;
            tmpYear = Manager.CurrentYear;
            tmpMonth = Manager.CurrentMonth - 1;
            if(Manager.CurrentMonth == 1) 
            {
                tmpMonth = 12;
                tmpYear--;
            }
            int maxLastMonth = DateTime.DaysInMonth(tmpYear, tmpMonth);


            

            //POPUNJAVANJE KALENDARA IQ200 PALI GASARA NA MAKSARU

            int startI, startJ;
            Brush b = Brushes.LightGray;

            switch (firstDayOfMonth.DayOfWeek)
            {

                case DayOfWeek.Monday:
                    startI = 1;
                    startJ = 0;
                    break;

                case DayOfWeek.Tuesday:
                    startI = 0;
                    startJ = 1;
                    break;

                case DayOfWeek.Wednesday:
                    startI = 0;
                    startJ = 2;
                    break;

                case DayOfWeek.Thursday:
                    startI = 0;
                    startJ = 3;
                    break;

                case DayOfWeek.Friday:
                    startI = 0;
                    startJ = 4;
                    break;

                case DayOfWeek.Saturday:
                    startI = 0;
                    startJ = 5;
                    break;

                case DayOfWeek.Sunday:
                    startI = 0;
                    startJ = 6;
                    break;

                default:
                    startI = 0;
                    startJ = 0;
                    break;

            }
            int counter = startI+startJ+2;

            counter = maxLastMonth+1;
            for (int i = startI; i >= 0; i--) 
            {
                if (i < startI) startJ = 6;
                for(int j = startJ; j >= 0; j--) 
                {
                    Cells[i, j].Date.Text = $"{counter--}.";
                    Cells[i, j].Border.Background = b;
                }
            }

            counter = 1;
            b = Brushes.AliceBlue;

            for (int i = startI; i < 6; i++)
            {
                if (i > startI) startJ = 0;
                for (int j = startJ; j < 7; j++)
                {
                    Cells[i, j].Date.Text = $"{counter}.";
                    Cells[i, j].Border.Background = b;
                    if (counter++ == max) 
                    { 
                        counter = 1;
                        b = Brushes.LightGray;
                    }
                }
            }
           
        }
        private CalendarCellAccess[,] getCellMatrix()
        {
            int i, j;
            CalendarCellAccess[,] cells = new CalendarCellAccess[6, 7];
            for (i = 0; i < 6; i++)
                for (j = 0; j < 7; j++)
                    cells[i, j] = new CalendarCellAccess();

            var children = CalendarGrid.Children;
            i = 0; j = 0;
            foreach(var child in children)
            {
                Border b = (Border)child;
                Grid g = (Grid)b.Child;
                var gChildren = g.Children;

                cells[i, j].Border = b;
                cells[i, j].Date = (TextBlock) ((Viewbox)gChildren[0]).Child;
                cells[i, j].Event = (TextBlock) ((Viewbox)gChildren[1]).Child;
                cells[i, j].CheckBox = (CheckBox) ((Viewbox)gChildren[2]).Child;

                cells[i, j].Event.Text = String.Empty;

                i++;
                if (i >= 6) { i = 0; j++; }
            }

            return cells;
        }

        private void MouseDown (object sender, EventArgs e)
        {
            if (AddEventButton.Visibility == Visibility.Visible)
                AddEventButton.Visibility = Visibility.Hidden;
            else
                AddEventButton.Visibility = Visibility.Visible;
        }

        private void LeftArrow_Click(object sender, EventArgs e)
        {
            Manager.CurrentMonth--;
            Load_Calendar();
        }

        private void RightArrow_Click(object sender, EventArgs e)
        {
            Manager.CurrentMonth++;
            Load_Calendar();
        }
    }
}
