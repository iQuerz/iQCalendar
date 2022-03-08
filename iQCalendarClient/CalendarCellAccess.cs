using iQCalendarClient.Business.Models;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace iQCalendarClient
{
    class CalendarCellAccess
    {
        public Border Border { get; set; }

        public TextBlock DateText { get; set; }
        public int Day => Convert.ToInt32(DateText.Text.Trim('.'));

        public TextBlock OverflowText { get; set; }

        public TextBlock EventText { get; set; }

        public CheckBox CheckBox { get; set; }

        public List<Event> Events { get; set; }

        private int _activeIndex { get; set; }
        public int ActiveIndex
        {
            get => _activeIndex;
            set
            {
                if (value > Events.Count - 1)
                    _activeIndex = 0;
                else if (value < 0)
                    _activeIndex = Events.Count - 1;
                else
                    _activeIndex = value;

                EventText.Text = Events[_activeIndex].Name.ToString();
            }
        }

        public CalendarCellAccess()
        {
            _activeIndex = -1; // ovo zapravo nije potrebno ali kazacemo da je tu za svaaaki slucaj... kosta nas O(42) i to samo jednom tkd nije skupo
            Events = new List<Event>();
        }


        /// <summary>
        /// Adds an event to the list and sets <see cref="ActiveIndex"/> to point to it. 
        /// It handles:
        ///  The overflow text at the cell's top right corner and 
        ///  <see cref="EventText"/> content with appropriate text.
        /// </summary>
        /// <param name="e"></param>
        public void AddEvent(Event e)
        {
            Events.Add(e);
            ActiveIndex = Events.Count - 1;

            if (Events.Count > 1)
                OverflowText.Text = $"+{Events.Count - 1}";
        }

        /// <summary>
        /// Removes the active event from the list and updates the event text inside the cell.
        /// </summary>
        public void RemoveEvent()
        {
            if (Events.Count == 0)
            {
                return;
            }

            if(Events.Count == 1)
            {
                EventText.Text = "";
                Events.RemoveAt(0);
                _activeIndex = -1;
                return;
            }

            Events.RemoveAt(ActiveIndex--);
            if (Events.Count < 2)
                OverflowText.Text = "";
        }

        public void ClearEvents()
        {
            Events.Clear();
        }

    }
}