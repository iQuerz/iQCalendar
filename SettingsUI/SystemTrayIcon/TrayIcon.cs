using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

using SettingsUI;

namespace SystemTrayIcon
{
    public class TrayIcon
    {
        NotifyIcon _trayIcon;
        ContextMenu _contextMenu;
        SettingsForm _settingsForm;

        public TrayIcon(bool start, SettingsForm form)
        {
            _settingsForm = form;
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

            foreach (var item in items)
                _contextMenu.MenuItems.Add(item);

            _trayIcon.ContextMenu = _contextMenu;
        }

        #region Handlers
        private void Settings_OnClick(object sender, EventArgs e)
        {
            _settingsForm.Show();
            _settingsForm.Activate();
            _trayIcon.Visible = false;
        }
        private void Logs_OnClick(object sender, EventArgs e)
        {
            try
            {
                string process = "explorer.exe";
                string arguments = Assembly.GetEntryAssembly().Location.Replace("SettingsUI.exe", "Logs");
                Process.Start(process, arguments);
            }
            catch (Exception)
            {
                var result = MessageBox.Show("Logs folder not found.", "Error opening logs", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                if(result == DialogResult.Retry)
                {
                    Logs_OnClick(sender, e);
                }
            }
        }
        private void Restart_OnClick(object sender, EventArgs e)
        {
            MessageBox.Show("Yet to be implemented.");
        }
        private void Stop_OnClick(object sender, EventArgs e)
        {
            _settingsForm.Shutdown();
        }
        #endregion

    }
}