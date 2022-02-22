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
            loadCalendar();
            //PreviewMouseDown += MouseDown;
        }

        private void loadCalendar()
        {

            Cells[2, 2].Border.BorderBrush = Brushes.Orange;
            Cells[2, 2].Border.BorderThickness = new Thickness(3);
            Cells[2, 2].Date.FontWeight = FontWeights.Bold;
            Cells[2, 2].Border.ToolTip = "Danasnji Dan";

            //POPUNJAVANJE KALENDARA IQ200 PALI GASARA NA MAKSARU

            int startI1 = 0, startJ1, startI2, startJ2;
            DateTime firstDayOfMonth = new DateTime(Manager.CurrentYear, Manager.CurrentMonth, 1);
            switch (firstDayOfMonth.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    startI1 = 1;
                    startJ1 = 0;
                    break;

                case DayOfWeek.Tuesday:
                    startJ1 = 1;
                    break;

                case DayOfWeek.Wednesday:
                    startJ1 = 2;
                    break;

                case DayOfWeek.Thursday:
                    startJ1 = 3;
                    break;

                case DayOfWeek.Friday:
                    startJ1 = 4;
                    break;

                case DayOfWeek.Saturday:
                    startJ1 = 5;
                    break;

                case DayOfWeek.Sunday:
                    startJ1 = 6;
                    break;

                default:
                    startJ1 = 0;
                    break;
            }


            // first loop variables (last month cells)
            startI2 = startI1;
            startJ2 = startJ1 - 1;
            if (startJ2 < 0) { startJ2 = 6; startI2--; }

            int tmpYear, tmpMonth;
            tmpYear = Manager.CurrentYear;
            tmpMonth = Manager.CurrentMonth - 1;
            if (Manager.CurrentMonth == 1)
            {
                tmpMonth = 12;
                tmpYear--;
            }
            int daysInLastMonth = DateTime.DaysInMonth(tmpYear, tmpMonth);
            Brush b = Brushes.LightGray;
            int counter = daysInLastMonth;

            for (int i = startI2; i >= 0; i--) 
            {
                if (i < startI2) startJ2 = 6;
                for(int j = startJ2; j >= 0; j--) 
                {
                    Cells[i, j].Date.Text = $"{counter--}.";
                    Cells[i, j].Border.Background = b;
                }
            }

            // second loop variables (current & next month cells)
            counter = 1;
            b = Brushes.AliceBlue;
            int daysInMonth = DateTime.DaysInMonth(Manager.CurrentYear, Manager.CurrentMonth);

            for (int i = startI1; i < 6; i++)
            {
                if (i > startI1) startJ1 = 0;
                for (int j = startJ1; j < 7; j++)
                {
                    Cells[i, j].Date.Text = $"{counter}.";
                    Cells[i, j].Border.Background = b;
                    if (counter++ == daysInMonth) 
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

                cells[i, j].Event.Text = string.Empty;

                i++;
                if (i >= 6) { i = 0; j++; }
            }

            return cells;
        }

        #region Click events
        private void LeftArrow_Click(object sender, EventArgs e)
        {
            Manager.CurrentMonth--;
            loadCalendar();
        }
        private void RightArrow_Click(object sender, EventArgs e)
        {
            Manager.CurrentMonth++;
            loadCalendar();
        }
        #endregion

    }
}
