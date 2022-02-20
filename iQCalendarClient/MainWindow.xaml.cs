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
        
        Manager Manager;

        public MainWindow()
        {
            InitializeComponent();
            Manager = new Manager();
            Loaded += Client_Loaded;
            LeftArrowButton.Click += LeftArrow_Click;
        }

        private void Client_Loaded(object sender, EventArgs e)
        {
            CalendarCellAccess[,] Cells;
            //Cells = getCellMatrix();

            //PreviewMouseDown += MouseDown;
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
            MonthLabel.Text = "Januar";
        }
    }
}
