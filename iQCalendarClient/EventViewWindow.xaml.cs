using iQCalendarClient.Business.Models;
using iQCalendarClient.Business.Models.Types;
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
        bool changes = false;
        public EventViewWindow(MainWindow main, DateTime date, Event @event = null)
        {

            InitializeComponent();
            Owner = main;
            Width = main.ParentGrid.DesiredSize.Width;
            Height = main.ParentGrid.DesiredSize.Height;

            Loaded += OnLoad;
            KeyDown += Global_Keydown;

            CloseButton.Click += CloseButton_Click;
            ButtonXClose.Click += CloseButton_Click;

            EventDatePicker.PreviewMouseUp += Date_MouseUp;
            IntegerNotifTextBox.KeyDown += IntTextBox_KeyDown;
            NotifsList.PreviewMouseWheel += NotifsList_Scroll;


            setupColorsComboBox();
            setupRecurringTypePicker();
            loadWindowWithValues(@event, date);
        }

        private void Date_MouseUp(object sender, MouseButtonEventArgs e)
        {
            EventDatePicker.IsDropDownOpen = true;
        }

        private void NotifsList_Scroll(object sender, MouseWheelEventArgs e)
        {//we doing this jer nece da scroll kada je mis preko elementa
            MainScrollViewer.ScrollToVerticalOffset(MainScrollViewer.VerticalOffset - e.Delta);
        }

        private void Global_Keydown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                if (!changes)
                {
                    Close();
                    return;
                }

                var res = MessageBox.Show(this, "Izmene neće biti sačuvane. Zatvoriti prozor?", "Pronašli smo nesačuvane podatke", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (res != MessageBoxResult.Yes)
                    Close();
            }
        }

        private void OnLoad(object sender, EventArgs e)
        {
        }

        #region Recurring
        private void setupRecurringTypePicker()
        {
            EventRecurringCombo.MouseWheel += RecurringCombo_Wheel;

            EventRecurringCombo.Items.Add("Nikada");
            EventRecurringCombo.Items.Add("Dnevno");
            EventRecurringCombo.Items.Add("Nedeljno");
            EventRecurringCombo.Items.Add("Mesečno");
            EventRecurringCombo.Items.Add("Godišnje");

            EventRecurringCombo.SelectedIndex = 0;
        }
        private void RecurringCombo_Wheel(object sender, MouseWheelEventArgs e)
        {
            int count = EventRecurringCombo.Items.Count;
            int index = EventRecurringCombo.SelectedIndex;
            int delta = 0;

            if (e.Delta < 0)
                delta++;
            else
                delta--;

            if (index+delta < 0 || index+delta > count - 1)
                delta = 0;

            EventRecurringCombo.SelectedIndex += delta;

            e.Handled = true;
        }
        #endregion

        #region Color
        private void setupColorsComboBox()
        {
            EventColorComboBox.SelectionChanged += ColorCombo_Changed;
            EventColorComboBox.MouseWheel += ColorCombo_Wheel;

            EventColorComboBox.Items.Add("AliceBlue");
            EventColorComboBox.Items.Add("Red");
            EventColorComboBox.Items.Add("DarkRed");
            EventColorComboBox.Items.Add("OrangeRed");
            EventColorComboBox.Items.Add("Yellow");
            EventColorComboBox.Items.Add("Lime");
            EventColorComboBox.Items.Add("Green");
            EventColorComboBox.Items.Add("Blue");
            EventColorComboBox.Items.Add("BlueViolet");
            EventColorComboBox.Items.Add("Purple");

            EventColorComboBox.SelectedIndex = 0;
            SolidColorBrush b = (SolidColorBrush)new BrushConverter().ConvertFromString(EventColorComboBox.Text); //this doesn't make sense but it works
            EventColorBorder.Background = b;
        }
        private void ColorCombo_Changed(object sender, SelectionChangedEventArgs e)
        {
            SolidColorBrush b = (SolidColorBrush)new BrushConverter().ConvertFromString(e.AddedItems[0].ToString()); //this doesn't make sense but it works
            EventColorBorder.Background = b;
        }
        private void ColorCombo_Wheel(object sender, MouseWheelEventArgs e)
        {
            int count = EventColorComboBox.Items.Count;
            int index = EventColorComboBox.SelectedIndex;
            int delta = 0;

            if (e.Delta < 0)
                delta++;
            else
                delta--;

            if (index + delta < 0 || index + delta > count - 1)
                delta = 0;

            EventColorComboBox.SelectedIndex += delta;

            e.Handled = true;
        }
        #endregion

        #region Notifications
        private void IntTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;

            if (e.Key >= Key.D0      && e.Key <= Key.D9
             || e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                e.Handled = false;
        }
        private void CheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (cb.IsChecked.Value)
                NotifsList.Items.Add(cb.Content + " događaja");
            else
                NotifsList.Items.Remove(cb.Content + " događaja");
        }
        private void AddDay_Click(object sender, RoutedEventArgs e)
        {
            TextBox tb = IntegerNotifTextBox;
            if (tb.Text == "")
                return;
            int days = Convert.ToInt32(tb.Text);
            string message = days.ToString();
            message += (days % 10) == 1 ? " dan pre događaja" : " dana pre događaja";

            if (!NotifsList.Items.Contains(message))
                NotifsList.Items.Add(message);
            tb.Clear();
            tb.Focus();
        }
        private void RemoveDay_Click(object sender, RoutedEventArgs e)
        {
            string message = (string)NotifsList.SelectedItem;
            NotifsList.Items.Remove(message);
            switch (message.Split(' ')[0])
            {
                case "Nedelju":
                    WeekCB.IsChecked = false;
                    break;
                case "Dve":
                    TwoWeekCB.IsChecked = false;
                    break;
                case "Mesec":
                    MonthCB.IsChecked = false;
                    break;
                case "Godinu":
                    YearCB.IsChecked = false;
                    break;
                default:
                    break;
            }
        }

        #endregion

        private void loadWindowWithValues(Event e, DateTime date)
        {
            EventDatePicker.SelectedDate = date;

            if (e == null)
                return;

            NameTextbox.Text = e.Name;

            EventDescriptionTextBox.Text = e.Name;

            EventRecurringCombo.SelectedItem = recurringTypeToString(e.RecurringType);

            EventColorComboBox.SelectedItem = e.Color.ToString();

            string[] s = e.Notifications.Split(',');
            string itemString;
            foreach(var notification in s)
            {
                switch (notification)
                {
                    case "week":
                        WeekCB.IsChecked = true;
                        break;
                    case "twoweek":
                        TwoWeekCB.IsChecked = true;
                        break;
                    case "month":
                        MonthCB.IsChecked = true;
                        break;
                    case "year":
                        YearCB.IsChecked = true;
                        break;
                    default:
                        itemString = notification + " dan";
                        if (!notification.EndsWith("1"))//mnozina ima "a" na kraju. (dan[a]).
                            itemString += "a";
                        itemString += " pre događaja";
                        NotifsList.Items.Add(itemString);
                        break;
                }
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void ApplyButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        public RecurringType stringToRecurringType(string s)
        {
            return s switch
            {
                "Nikada" => RecurringType.NonRecurring,
                "Dnevno" => RecurringType.Daily,
                "Nedeljno" => RecurringType.Weekly,
                "Mesečno" => RecurringType.Monthly,
                "Godišnje" => RecurringType.Yearly,
                _ => RecurringType.NonRecurring,
            };
        }
        public string recurringTypeToString(RecurringType type)
        {
            return type switch
            {
                RecurringType.NonRecurring => "Nikada",
                RecurringType.Daily => "Dnevno",
                RecurringType.Weekly => "Nedeljno",
                RecurringType.Monthly => "Mesečno",
                RecurringType.Yearly => "Godišnje",
                _ => "Nikada",
            };
        }
    }
}
