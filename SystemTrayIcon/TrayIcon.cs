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
        public TrayIcon(bool start)
        {
            _trayIcon = new NotifyIcon();

            if (start)
                Start();
        }

        public void Start()
        {
            _trayIcon.Visible = true;
            _trayIcon.Icon = Icon.ExtractAssociatedIcon("Resources/icon.ico");
        }
    }
}
