﻿using iQCalendarClient.Business;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
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
        ClientSettings Settings;
        Manager Manager;

       
        public MainWindow()
        {
            
            InitializeComponent();
            Manager = new Manager();
            Loaded += Client_Loaded;
            PrevMonthButton.Click += PrevMonth_Click;
            NextMonthButton.Click += NextMonth_Click;
            PrevYearButton.Click += PrevYear_Click;
            NextYearButton.Click += NextYear_Click;
            AddEventButton.Click += AddEventButton_Click;
            EditEventButton.Click += EditEventButton_Click;
            

            //SearchTextBox.MouseDoubleClick += SearchBox_GotFocus;
            SearchTextBox.GotKeyboardFocus += SearchBox_GotFocus;
            SearchTextBox.TextChanged += SearchBox_GotFocus;
            SearchTextBox.KeyDown += SearchBox_GotFocus;

            SearchTextBox.LostFocus += SearchBox_LostFocus;


           

        }


        private void Client_Loaded(object sender, EventArgs e)
        {
            Settings = new ClientSettings();
            Settings.loadSettings();

            Cells = getCellMatrix();
           
            Manager.loadTestData();
            Manager.CurrentMonth = DateTime.Now.Month;
            Manager.CurrentYear = DateTime.Now.Year;

            MonthLabel.Text = getMonthName(Manager.CurrentMonth);
            YearLabel.Text = $"{Manager.CurrentYear}.";
            setupCalendarCells();

            Title = $"iQCalendar - {Manager.Account.Name}";
            //NOVO OD OVE LINIJE DO KRAJA WINDOW CLOSING FUNKCIJE nista nisam zvao niti sam sta stigao ubacio sam novi properties file ako hoces veceras da probas ovo ako ne ja cu sutra da uradim
            // i ima jos nekih sitnih detalja sam pushovao nesto sam doradio
            this.Top = Properties.Settings.Default.Top;
            this.Left = Properties.Settings.Default.Left;
            this.Height = Properties.Settings.Default.Height;
            this.Width = Properties.Settings.Default.Width;
            // Very quick and dirty - but it does the job
            if (Properties.Settings.Default.Maximized)
            {
                WindowState = WindowState.Maximized;
            }
        }

        private void Window_Closing(object sender, EventArgs e)
        {

            if (WindowState == WindowState.Maximized)
            {
                // Use the RestoreBounds as the current values will be 0, 0 and the size of the screen
                Properties.Settings.Default.Top = RestoreBounds.Top;
                Properties.Settings.Default.Left = RestoreBounds.Left;
                Properties.Settings.Default.Height = RestoreBounds.Height;
                Properties.Settings.Default.Width = RestoreBounds.Width;
                Properties.Settings.Default.Maximized = true;
            }
            else
            {
                Properties.Settings.Default.Top = this.Top;
                Properties.Settings.Default.Left = this.Left;
                Properties.Settings.Default.Height = this.Height;
                Properties.Settings.Default.Width = this.Width;
                Properties.Settings.Default.Maximized = false;
            }

            Properties.Settings.Default.Save();

        }




        private void setupCalendarCells()
        {//POPUNJAVANJE KALENDARA IQ200 PALI GASARA NA MAKSARU

            int startI1, startJ1;
            getStartCoords(out startI1, out startJ1);

            #region first loop (last month cells)

            int startI2 = startI1;
            int startJ2 = startJ1 - 1;
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
                    setCellBorders(i, j);
                    Cells[i, j].Border.ToolTip = null;
                }
            }

            #endregion

            #region second loop (current & next month cells)

            counter = 1;
            b = Brushes.AliceBlue;
            int daysInMonth = DateTime.DaysInMonth(Manager.CurrentYear, Manager.CurrentMonth);

            for (int i = startI1; i < 6; i++)
            {
                if(i > startI1)
                    startJ1 = 0;

                for (int j = startJ1; j < 7; j++)
                {
                    Cells[i, j].Date.Text = $"{counter}.";
                    Cells[i, j].Border.Background = b;
                    setCellBorders(i, j);
                    Cells[i, j].Border.ToolTip = null;
                    if (counter++ == daysInMonth)
                    {
                        counter = 1;
                        b = Brushes.LightGray;
                    }
                }
            }

            #endregion

            highlightCurrentDay();

        }

        private void getStartCoords(out int StartI, out int StartJ)
        {
            StartI = 0;
            DateTime firstDayOfMonth = new DateTime(Manager.CurrentYear, Manager.CurrentMonth, 1);
            switch (firstDayOfMonth.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    StartI = 1;
                    StartJ = 0;
                    break;

                case DayOfWeek.Tuesday:
                    StartJ = 1;
                    break;

                case DayOfWeek.Wednesday:
                    StartJ = 2;
                    break;

                case DayOfWeek.Thursday:
                    StartJ = 3;
                    break;

                case DayOfWeek.Friday:
                    StartJ = 4;
                    break;

                case DayOfWeek.Saturday:
                    StartJ = 5;
                    break;

                case DayOfWeek.Sunday:
                    StartJ = 6;
                    break;

                default:
                    StartJ = 0;
                    break;
            }
        }
        private void highlightCurrentDay()
        {
            DateTime now = DateTime.Now;
            if (Manager.CurrentMonth != now.Month || Manager.CurrentYear != now.Year)
                return;

            int startI, startJ;
            getStartCoords(out startI, out startJ);
            if (Manager.CurrentMonth == now.Month && Manager.CurrentYear == now.Year)
            {
                int nowRow = startI + (now.Day / 7);
                int nowColumn = startJ + (now.Day % 7) - 1; //   (」゜ロ゜)」✞  pls da radis
                Cells[nowRow, nowColumn].Border.BorderBrush = Brushes.Orange;
                Cells[nowRow, nowColumn].Border.BorderThickness = new Thickness(3);
                Cells[nowRow, nowColumn].Date.FontWeight = FontWeights.Bold;
                Cells[nowRow, nowColumn].Border.ToolTip = "Današnji Dan";
            }
        }
        private void setCellBorders(int i, int j)
        {
            Border cell = Cells[i, j].Border;
            cell.BorderThickness = new Thickness(0.25);

            if (i == 0)
                cell.BorderThickness = new Thickness(0.25, 0.5, 0.25, 0.25);
            else if (i == 5)
                cell.BorderThickness = new Thickness(0.25, 0.25, 0.25, 0.5);
            else
                cell.BorderThickness = new Thickness(0.25);

            if(j==0)
                cell.BorderThickness = new Thickness(0.5, cell.BorderThickness.Top, 0.25, cell.BorderThickness.Bottom);
            else if(j==6)
                cell.BorderThickness = new Thickness(0.25, cell.BorderThickness.Top, 0.5, cell.BorderThickness.Bottom);
            else
                cell.BorderThickness = new Thickness(0.25, cell.BorderThickness.Top, 0.25, cell.BorderThickness.Bottom);
                
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
        static string getMonthName(int month)
        {
            switch (month)
            {
                case 1:
                    return "Januar";
                case 2:
                    return "Februar";
                case 3:
                    return "Mart";
                case 4:
                    return "April";
                case 5:
                    return "Maj";
                case 6:
                    return "Jun";
                case 7:
                    return "Jul";
                case 8:
                    return "Avgust";
                case 9:
                    return "Septembar";
                case 10:
                    return "Oktobar";
                case 11:
                    return "Novembar";
                case 12:
                    return "Decembar";
                default:
                    return "Invalid Month";
            }
        }

        #region Click events
        private void PrevMonth_Click(object sender, EventArgs e)
        {
            Manager.CurrentMonth--;
            MonthLabel.Text = getMonthName(Manager.CurrentMonth);
            YearLabel.Text = $"{Manager.CurrentYear}.";
            setupCalendarCells();
            Focus();
        }
        private void NextMonth_Click(object sender, EventArgs e)
        {
            Manager.CurrentMonth++;
            MonthLabel.Text = getMonthName(Manager.CurrentMonth);
            YearLabel.Text = $"{Manager.CurrentYear}.";
            setupCalendarCells();
            Focus();
        }
        private void PrevYear_Click(object sender, RoutedEventArgs e)
        {
            Manager.CurrentYear--;
            YearLabel.Text = $"{Manager.CurrentYear}.";
            setupCalendarCells();
            Focus();
        }
        private void NextYear_Click(object sender, RoutedEventArgs e)
        {
            Manager.CurrentYear++;
            YearLabel.Text = $"{Manager.CurrentYear}.";
            setupCalendarCells();
            Focus();
        }

        private void AddEventButton_Click(object sender, RoutedEventArgs e) 
        {
            double width = ParentGrid.DesiredSize.Width;
            double height = ParentGrid.DesiredSize.Height;
            EventViewWindow eventViewWindow1 = new EventViewWindow(width, height);
            eventViewWindow1.Owner = this;            
            eventViewWindow1.ShowDialog();
        }
        private void EditEventButton_Click(object sender, RoutedEventArgs e) 
        {
            double width = ParentGrid.DesiredSize.Width;
            double height = ParentGrid.DesiredSize.Height;
            EventViewWindow eventViewWindow1 = new EventViewWindow(width, height);
            eventViewWindow1.Owner = this;
            eventViewWindow1.ShowDialog();
        }

        
        #endregion

        #region Search UI Events

        private void SearchBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if(SearchTextBox.Text == "Pretrazi...")
            {
                SearchTextBox.Text = "";
            }
        }
        private void SearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "Pretrazi...";
        }
        #endregion

    }
}
