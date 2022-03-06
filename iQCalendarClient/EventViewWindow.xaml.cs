using iQCalendarClient.Business.Models;
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
using System.Windows.Shapes;

namespace iQCalendarClient
{
    /// <summary>
    /// Interaction logic for EventViewWindow.xaml
    /// </summary>
    public partial class EventViewWindow : Window
    {
        public EventViewWindow(MainWindow main, DateTime date, Event @event = null)
        {
            InitializeComponent();
            Owner = main;
            Width = main.ParentGrid.DesiredSize.Width;
            Height = main.ParentGrid.DesiredSize.Height;

            CloseButton.Click += CloseButton_Click;
        }

        private void setupWindow(Event e, DateTime date)
        {
            EventDatePicker.SelectedDate = date;

            if (e == null)
                return;
            
        }

        public void CloseButton_Click(object sender, EventArgs e) 
        {
            Close();
        }
        private void ButtonXClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
