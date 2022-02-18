using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SystemTrayIcon
{
    public class TrayIcon
    {
        NotifyIcon _trayIcon;
        ContextMenu _contextMenu;

        public TrayIcon(bool start)
        {
            _trayIcon = new NotifyIcon();
            _contextMenu = new ContextMenu();
            _trayIcon.Text = "iQCalendar";

            setupMenu();

            if (start)
                Start();
        }

        public void Start()
        {
            _trayIcon.Visible = true;
            _trayIcon.Icon = Icon.ExtractAssociatedIcon("Resources/icon.ico");
        }
        public void Stop()
        {
            _trayIcon.Dispose();
        }

        private void setupMenu()
        {
            MenuItem[] items = new MenuItem[4];
            items[0] = new MenuItem("Settings", Settings_OnClick);
            items[1] = new MenuItem("Logs", Logs_OnClick);
            items[2] = new MenuItem("Restart", Restart_OnClick);
            items[3] = new MenuItem("Stop", Stop_OnClick);

            foreach(var item in items)
                _contextMenu.MenuItems.Add(item);

            _trayIcon.ContextMenu = _contextMenu;
        }

        #region Handlers
        private void Settings_OnClick(object sender, EventArgs e)
        {

        }
        private void Logs_OnClick(object sender, EventArgs e)
        {

        }
        private void Restart_OnClick(object sender, EventArgs e)
        {

        }
        private void Stop_OnClick(object sender, EventArgs e)
        {

        }
        #endregion

    }
}