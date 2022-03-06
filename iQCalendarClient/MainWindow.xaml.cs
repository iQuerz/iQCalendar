﻿using iQCalendarClient.Business;
using iQCalendarClient.Business.Models.Types;
using iQCalendarClient.Properties;
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
        readonly Manager Manager;
        readonly WindowSettings windowSettings;

        // Constructor with initialization for... everything, kinda
        public MainWindow()
        {
            //startup
            InitializeComponent();
            windowSettings = WindowSettings.Default;
            loadWindowState();

            //some base stuff
            Manager = new Manager();
            Loaded += Window_Loaded;
            Closing += Window_Closing;

            //clicks
            loadClickEventHandlers();

            // ubij me ne znam
            //SearchTextBox.MouseDoubleClick += SearchBox_GotFocus;
            SearchTextBox.GotKeyboardFocus += SearchBox_GotFocus;
            SearchTextBox.TextChanged += SearchBox_GotFocus;
            SearchTextBox.KeyDown += SearchBox_GotFocus;

            SearchTextBox.LostFocus += SearchBox_LostFocus;
        }

        #region App load & App close related
        private async void Window_Loaded(object sender, EventArgs e)
        {
            Cells = getCellMatrix();

            try { await Manager.loadEventsAsync(); }
            catch (Exception ex) { showMsgBoxError(ex.Message); }

            setupLabels();
            setupCalendarCells();

            Title = $"iQCalendar - {Manager.Account.Name}";
        }
        private void Window_Closing(object sender, EventArgs e)
        {
            saveWindowState();
        }

        private void loadWindowState()
        {
            if (windowSettings.FirstBoot)
            {
                windowSettings.FirstBoot = false;
                return;
            }

            Top = windowSettings.Top;
            Left = windowSettings.Left;
            Height = windowSettings.Height;
            Width = windowSettings.Width;

            if (windowSettings.Maximized)
                WindowState = WindowState.Maximized;
        }
        private void saveWindowState()
        {
            if (WindowState == WindowState.Maximized)
            {
                windowSettings.Top = RestoreBounds.Top;
                windowSettings.Left = RestoreBounds.Left;
                windowSettings.Height = RestoreBounds.Height;
                windowSettings.Width = RestoreBounds.Width;
                windowSettings.Maximized = true;
            }
            else
            {
                windowSettings.Top = Top;
                windowSettings.Left = Left;
                windowSettings.Height = Height;
                windowSettings.Width = Width;
                windowSettings.Maximized = false;
            }
            windowSettings.Save();
        }
        #endregion

        #region Calendar cells - style related
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
                    Cells[i, j].Event.Text = "";
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
                    Cells[i, j].Event.Text = "";
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
            showEventsOnCalendar();
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

        /// <summary>
        /// Sets the cell borders to black and generates their thickness to 0.25 each.
        /// Left, right, top and bottom rows have their edge borders set to 0.5.
        /// </summary>
        /// <param name="i">Row parameter</param>
        /// <param name="j">Column parameter</param>
        private void setCellBorders(int i, int j)
        {
            Border cell = Cells[i, j].Border;
            cell.BorderThickness = new Thickness(0.25);
            cell.BorderBrush = Brushes.Black;

            if (i == 0)
                cell.BorderThickness = new Thickness(0.25, 0.5, 0.25, 0.25);
            else if (i == 5)
                cell.BorderThickness = new Thickness(0.25, 0.25, 0.25, 0.5);
            else
                cell.BorderThickness = new Thickness(0.25);

            if (j == 0)
                cell.BorderThickness = new Thickness(0.5, cell.BorderThickness.Top, 0.25, cell.BorderThickness.Bottom);
            else if (j == 6)
                cell.BorderThickness = new Thickness(0.25, cell.BorderThickness.Top, 0.5, cell.BorderThickness.Bottom);
            else
                cell.BorderThickness = new Thickness(0.25, cell.BorderThickness.Top, 0.25, cell.BorderThickness.Bottom);
        }

        private void setupLabels()
        {
            MonthLabel.Text = getMonthName(Manager.CurrentMonth);
            YearLabel.Text = $"{Manager.CurrentYear}.";
            Focus();
        }

        #endregion

        #region App Logic related
        private void showEventsOnCalendar()
        {
            int startI, startJ;
            getStartCoords(out startI, out startJ);

            for (int i = 0; i < Manager.Events.Count; i++)
            {
                int day = Manager.Events[i].Date.Day;
                int column = (startJ + day - 1) % 7;
                int row = (startI + day) / 7;
                Cells[row, column].Event.Text = Manager.Events[i].Name;

                // apply 
                if(Manager.Events[i].RecurringType != RecurringType.NonRecurring)
                {
                    DateTime currentDate = Manager.Events[i].Date;
                    while(currentDate.Month == Manager.CurrentMonth)
                    {
                        day = currentDate.Day;
                        column = (startJ + day - 1) % 7;
                        row = (startI + day) / 7;
                        Cells[row, column].Event.Text = Manager.Events[i].Name;

                        currentDate = getNextDateFromRecurringType(currentDate, Manager.Events[i].RecurringType);
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
            foreach (var child in children)
            {
                Border b = (Border)child;
                Grid g = (Grid)b.Child;
                var gChildren = g.Children;

                cells[i, j].Border = b;
                cells[i, j].Date = (TextBlock)((Viewbox)gChildren[0]).Child;
                cells[i, j].Event = (TextBlock)((Viewbox)gChildren[1]).Child;
                cells[i, j].CheckBox = (CheckBox)((Viewbox)gChildren[2]).Child;

                cells[i, j].Event.Text = string.Empty;

                i++;
                if (i >= 6) { i = 0; j++; }
            }

            return cells;
        }

        #endregion

        #region Click events

        // load static event handlers
        private void loadClickEventHandlers()
        {
            PrevMonthButton.Click += PrevMonth_Click;
            NextMonthButton.Click += NextMonth_Click;
            PrevYearButton.Click += PrevYear_Click;
            NextYearButton.Click += NextYear_Click;
            AddEventButton.Click += AddEventButton_Click;
            EditEventButton.Click += EditEventButton_Click;
        }

        // calendar cell handlers
        private void CalendarCell_Click(object sender, EventArgs e)
        {

        }
        private void CalendarCell_DoubleClick(object sender, EventArgs e)
        {

        }

        // static button handlers
        private void PrevMonth_Click(object sender, EventArgs e)
        {
            Manager.CurrentMonth--;
            setupLabels();
            setupCalendarCells();
        }
        private void NextMonth_Click(object sender, EventArgs e)
        {
            Manager.CurrentMonth++;
            setupLabels();
            setupCalendarCells();
        }
        private void PrevYear_Click(object sender, RoutedEventArgs e)
        {
            Manager.CurrentYear--;
            setupLabels();
            setupCalendarCells();
        }
        private void NextYear_Click(object sender, RoutedEventArgs e)
        {
            Manager.CurrentYear++;
            setupLabels();
            setupCalendarCells();
        }

        private void AddEventButton_Click(object sender, RoutedEventArgs e) 
        {
            EventViewWindow eventViewWindow = new EventViewWindow(this);
            eventViewWindow.ShowDialog();
        }
        private void EditEventButton_Click(object sender, RoutedEventArgs e) 
        {
            EventViewWindow eventViewWindow = new EventViewWindow(this);
            eventViewWindow.ShowDialog();
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

        #region Helper functions

        /// <summary>
        /// Translates month numbers (1-12) to their Serbian language counterparts. (Januar, Februar, etc.)
        /// </summary>
        /// <param name="month" range="1-12"> One numeber representing a month. </param>
        /// <returns>A translated month string.</returns>
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

        /// <summary>
        /// Finds the first matrix cell coordinates to be populated,
        /// depending on the selected month and year inside the Manager object.
        /// </summary>
        /// <param name="StartI">Reference to the Row parameter</param>
        /// <param name="StartJ">Reference to the Column parameter</param>
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

        /// <summary>
        /// Finds the next date repetition in regards to its RecurringType
        /// </summary>
        /// <param name="date">Date from which to calculate.</param>
        /// <param name="recurringType">RecurringType used for calculation.</param>
        /// <returns>The next date calculated if RecurringType is above 0. Returns the same date otherwise</returns>
        static DateTime getNextDateFromRecurringType(DateTime date, RecurringType recurringType)
        {
            switch (recurringType)
            {
                case RecurringType.Daily:
                    return date.AddDays(1);
                case RecurringType.Weekly:
                    return date.AddDays(7);
                case RecurringType.Monthly:
                    return date.AddMonths(1);
                case RecurringType.Yearly:
                    return date.AddYears(1);
                default:
                    return date;
            }
        }

        private void showMsgBoxError(string message)
        {
            MessageBox.Show(this, message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion

    }
}
