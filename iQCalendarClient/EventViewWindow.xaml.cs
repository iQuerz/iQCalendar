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
        public EventViewWindow(double width, double height)
        {
            InitializeComponent();
            this.Width = width;
            this.Height = height;
            CloseButton.Click += CloseButton_Click;

        }

        public void CloseButton_Click(object sender, EventArgs e) 
        {
            EventViewWindow eventViewWindow = new EventViewWindow(this.Width, this.Height);
            this.Close();
        }
    }
}
