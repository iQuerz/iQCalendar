using System;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Http;
using System.Net;

using Newtonsoft.Json;
using SystemTrayIcon;

namespace SettingsUI
{
    public partial class SettingsForm : Form
    {
        TrayIcon _trayIcon;
        HttpClient httpClient;
        Settings ServerSettings;
        readonly string _version = "v1.0";
        bool _changes;
        string API_URL;

        public SettingsForm()
        {
            InitializeComponent();
            string s = File.ReadAllText("Resources/settings.json");
            ServerSettings = JsonConvert.DeserializeObject<Settings>(s);
            httpClient = new HttpClient();

            // accept certificates
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => true;
        }

        // ideja je da server pokrene ovu formicu sa sobom, a da ona njega ugasi kada korisnik trazi.
        private void Form1_Load(object sender, EventArgs e)
        {
            Text += " - " + _version;

            EmailPasswordEye.BackgroundImage = Image.FromFile("Resources/ClosedPasswordEye.png");
            OldPasswordEye.BackgroundImage = Image.FromFile("Resources/ClosedPasswordEye.png");
            NewPasswordEye.BackgroundImage = Image.FromFile("Resources/ClosedPasswordEye.png");
            ConfirmPasswordEye.BackgroundImage = Image.FromFile("Resources/ClosedPasswordEye.png");

            _trayIcon = new TrayIcon(true, this);
            _trayIcon.Start();
            LoadValues();

            Hide();
        }

        #region Helper Methods

        private void LoadValues()
        {
            NameTextbox.Text = ServerSettings.ServerName;
            PortTextbox.Text = ServerSettings.Port.ToString();
            EmailUsernameTextbox.Text = ServerSettings.HostEmailUsername;
            EmailPasswordTextbox.Text = ServerSettings.HostEmailPassword;
            NotificationTimeUpDown.Value = ServerSettings.NotificationTime;

            OldPasswordTextbox.Text = "";
            NewPasswordTextbox.Text = "";
            ConfirmPasswordTextbox.Text = "";

            UserMadeChanges = false;

            API_URL = "https://localhost:" + ServerSettings.Port + "/";
        }
        private Settings GetNewSettings()
        {
            Settings s = new Settings();
            switch(Tabs.SelectedIndex)
            {
                case 0:
                    s.ServerName = NameTextbox.Text;
                    s.ServerPassword = ServerSettings.ServerPassword; // stays the same
                    s.Port = Convert.ToInt32(PortTextbox.Text);
                    s.HostEmailUsername = EmailUsernameTextbox.Text;
                    s.HostEmailPassword = EmailPasswordTextbox.Text;
                    s.NotificationTime = Convert.ToInt32(NotificationTimeUpDown.Value);
                    break;

                case 2:
                    s.ServerName = ServerSettings.ServerName;
                    s.ServerPassword = NewPasswordTextbox.Text; // the only one changed
                    s.Port = ServerSettings.Port;
                    s.HostEmailUsername = ServerSettings.HostEmailUsername;
                    s.HostEmailPassword = ServerSettings.HostEmailPassword;
                    s.NotificationTime = ServerSettings.NotificationTime;
                    break;
            }

            return s;
        }
        private async Task<bool> UpdateServer()
        {
            if (Tabs.SelectedIndex == 2)
                if (!PasswordMatch())
                    return false;

            // getting the request body to a string;
            var newSettings = GetNewSettings();
            var body = JsonConvert.SerializeObject(newSettings);

            // creating a request
            var request = new HttpRequestMessage(HttpMethod.Post, $"{API_URL}api/Settings");
            request.Content = new StringContent(body, Encoding.UTF8, "application/json");

            // adding authorization to the request
            var auth = GetHashedAuth(ServerSettings.ServerName, ServerSettings.ServerPassword);
            request.Headers.Add("Authorization", auth);

            // getting the response
            var response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return false;

            var responseBody = await response.Content.ReadAsStringAsync();
            Settings updatedSetttings = JsonConvert.DeserializeObject<Settings>(responseBody);
            ServerSettings = updatedSetttings;

            LoadValues();

            return true;
        }
        public async void Shutdown()
        {
            using (HttpClient client = new HttpClient())
            {
                var auth = GetHashedAuth(ServerSettings.ServerName, ServerSettings.ServerPassword);
                client.DefaultRequestHeaders.Add("Authorization", auth);
                await client.GetAsync(API_URL + $"api/Settings/stop/{ServerSettings.ServerName}");
            }
            Dispose();
        }

        #endregion

        #region Authentication

        private bool PasswordMatch()
        {
            if (NewPasswordTextbox.Text != ConfirmPasswordTextbox.Text)
                return false;

            if (OldPasswordTextbox.Text != ServerSettings.ServerPassword)
                return false;

            return true;
        }
        private string GetHashedAuth(string username, string password)
        {
            string auth = $"{username}:{password}"; // international standard format for basic authentication. username:password

            Encoding encoding = Encoding.GetEncoding("iso-8859-1");
            auth = Convert.ToBase64String(encoding.GetBytes(auth));

            return $"Basic {auth}";
        }

        #endregion

        #region UserMade Changes boolean

        public bool UserMadeChanges
        {
            get { return _changes; }
            set
            {
                _changes = value;
                if (value)
                    switch (Tabs.SelectedIndex)
                    {
                        case 0:
                            Tabs.SelectedTab.Text = "Server*";
                            break;
                        case 1:
                            Tabs.SelectedTab.Text = "Accounts*";
                            break;
                        case 2:
                            Tabs.SelectedTab.Text = "Change Password*";
                            break;
                        default:
                            break;
                    }
                else
                    switch (Tabs.SelectedIndex)
                    {
                        case 0:
                            Tabs.SelectedTab.Text = "Server";
                            break;
                        case 1:
                            Tabs.SelectedTab.Text = "Accounts";
                            break;
                        case 2:
                            Tabs.SelectedTab.Text = "Change Password";
                            break;
                        default:
                            break;
                    }
            }
        }

        private void NameTextbox_TextChanged(object sender, EventArgs e)
        {
            UserMadeChanges = true;
        }
        private void PortTextbox_TextChanged(object sender, EventArgs e)
        {
            UserMadeChanges = true;
        }
        private void EmailUsernameTextbox_TextChanged(object sender, EventArgs e)
        {
            UserMadeChanges = true;
        }
        private void EmailPasswordTextbox_TextChanged(object sender, EventArgs e)
        {
            UserMadeChanges = true;
        }
        private void NotificationTimeUpDown_ValueChanged(object sender, EventArgs e)
        {
            UserMadeChanges = true;
        }
        private void Tabs_TabIndexChanged(object sender, EventArgs e)
        {
            UserMadeChanges = false;
        }
        private void Tabs_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (UserMadeChanges)
            {
                MessageBox.Show(this, "You have some unsaved changes!", "Unable to proceed.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Cancel = true;
            }
        }
        private void OldPasswordTextbox_TextChanged(object sender, EventArgs e)
        {
            UserMadeChanges = true;
        }
        private void NewPasswordTextbox_TextChanged(object sender, EventArgs e)
        {
            UserMadeChanges = true;
        }
        private void ConfirmPasswordTextbox_TextChanged(object sender, EventArgs e)
        {
            UserMadeChanges = true;
        }


        #endregion

        #region Buttons

        private async void OkButton_Click(object sender, EventArgs e)
        {
            if (UserMadeChanges)
            {
                bool response = await UpdateServer();
                if (response)
                    MessageBox.Show(this, "Changes saved successfully.", "Server update results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(this, "Error while updating changes.", "Server update results", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            Dispose();
        }
        private async void ApplyButton_Click(object sender, EventArgs e)
        {
            if (UserMadeChanges)
            {
                bool response = await UpdateServer();
                if (response)
                    MessageBox.Show(this, "Changes saved successfully.", "Server update results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show(this, "Error while updating changes.", "Server update results", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CancelButton_Click(object sender, EventArgs e)
        {
            if(UserMadeChanges)
                LoadValues();
        }

        #endregion

        #region Password Eyes
        private void EmailPasswordEye_Click(object sender, EventArgs e)
        {
            EmailPasswordTextbox.UseSystemPasswordChar = !EmailPasswordTextbox.UseSystemPasswordChar;
            if (!EmailPasswordTextbox.UseSystemPasswordChar)
                EmailPasswordEye.BackgroundImage = Image.FromFile("Resources/OpenPasswordEye.png");
            else
                EmailPasswordEye.BackgroundImage = Image.FromFile("Resources/ClosedPasswordEye.png");
        }
        private void EmailPasswordEye_DoubleClick(object sender, EventArgs e)
        {
            EmailPasswordTextbox.UseSystemPasswordChar = !EmailPasswordTextbox.UseSystemPasswordChar;
            if (!EmailPasswordTextbox.UseSystemPasswordChar)
                EmailPasswordEye.BackgroundImage = Image.FromFile("Resources/OpenPasswordEye.png");
            else
                EmailPasswordEye.BackgroundImage = Image.FromFile("Resources/ClosedPasswordEye.png");
        }

        private void OldPasswordEye_Click(object sender, EventArgs e)
        {
            OldPasswordTextbox.UseSystemPasswordChar = !OldPasswordTextbox.UseSystemPasswordChar;
            if (OldPasswordTextbox.UseSystemPasswordChar)
                OldPasswordEye.BackgroundImage = Image.FromFile("Resources/ClosedPasswordEye.png");
            else
                OldPasswordEye.BackgroundImage = Image.FromFile("Resources/OpenPasswordEye.png");
        }
        private void OldPasswordEye_DoubleClick(object sender, EventArgs e)
        {
            OldPasswordTextbox.UseSystemPasswordChar = !OldPasswordTextbox.UseSystemPasswordChar;
            if (OldPasswordTextbox.UseSystemPasswordChar)
                OldPasswordEye.BackgroundImage = Image.FromFile("Resources/ClosedPasswordEye.png");
            else
                OldPasswordEye.BackgroundImage = Image.FromFile("Resources/OpenPasswordEye.png");
        }

        private void NewPasswordEye_Click(object sender, EventArgs e)
        {
            NewPasswordTextbox.UseSystemPasswordChar = !NewPasswordTextbox.UseSystemPasswordChar;
            ConfirmPasswordTextbox.UseSystemPasswordChar = !ConfirmPasswordTextbox.UseSystemPasswordChar;

            if (NewPasswordTextbox.UseSystemPasswordChar)
            {
                NewPasswordEye.BackgroundImage = Image.FromFile("Resources/ClosedPasswordEye.png");
                ConfirmPasswordEye.BackgroundImage = Image.FromFile("Resources/ClosedPasswordEye.png");
            }
            else
            {
                NewPasswordEye.BackgroundImage = Image.FromFile("Resources/OpenPasswordEye.png");
                ConfirmPasswordEye.BackgroundImage = Image.FromFile("Resources/OpenPasswordEye.png");
            }
        }
        private void NewPasswordEye_DoubleClick(object sender, EventArgs e)
        {
            NewPasswordTextbox.UseSystemPasswordChar = !NewPasswordTextbox.UseSystemPasswordChar;
            ConfirmPasswordTextbox.UseSystemPasswordChar = !ConfirmPasswordTextbox.UseSystemPasswordChar;

            if (NewPasswordTextbox.UseSystemPasswordChar)
            {
                NewPasswordEye.BackgroundImage = Image.FromFile("Resources/ClosedPasswordEye.png");
                ConfirmPasswordEye.BackgroundImage = Image.FromFile("Resources/ClosedPasswordEye.png");
            }
            else
            {
                NewPasswordEye.BackgroundImage = Image.FromFile("Resources/OpenPasswordEye.png");
                ConfirmPasswordEye.BackgroundImage = Image.FromFile("Resources/OpenPasswordEye.png");
            }
        }

        private void ConfirmPasswordEye_Click(object sender, EventArgs e)
        {
            NewPasswordTextbox.UseSystemPasswordChar = !NewPasswordTextbox.UseSystemPasswordChar;
            ConfirmPasswordTextbox.UseSystemPasswordChar = !ConfirmPasswordTextbox.UseSystemPasswordChar;

            if (NewPasswordTextbox.UseSystemPasswordChar)
            {
                NewPasswordEye.BackgroundImage = Image.FromFile("Resources/ClosedPasswordEye.png");
                ConfirmPasswordEye.BackgroundImage = Image.FromFile("Resources/ClosedPasswordEye.png");
            }
            else
            {
                NewPasswordEye.BackgroundImage = Image.FromFile("Resources/OpenPasswordEye.png");
                ConfirmPasswordEye.BackgroundImage = Image.FromFile("Resources/OpenPasswordEye.png");
            }
        }
        private void ConfirmPasswordEye_DoubleClick(object sender, EventArgs e)
        {
            NewPasswordTextbox.UseSystemPasswordChar = !NewPasswordTextbox.UseSystemPasswordChar;
            ConfirmPasswordTextbox.UseSystemPasswordChar = !ConfirmPasswordTextbox.UseSystemPasswordChar;

            if (NewPasswordTextbox.UseSystemPasswordChar)
            {
                NewPasswordEye.BackgroundImage = Image.FromFile("Resources/ClosedPasswordEye.png");
                ConfirmPasswordEye.BackgroundImage = Image.FromFile("Resources/ClosedPasswordEye.png");
            }
            else
            {
                NewPasswordEye.BackgroundImage = Image.FromFile("Resources/OpenPasswordEye.png");
                ConfirmPasswordEye.BackgroundImage = Image.FromFile("Resources/OpenPasswordEye.png");
            }
        }

        #endregion

        private void SettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            string json = JsonConvert.SerializeObject(ServerSettings);
            File.WriteAllText("Resources/settings.json", json);
            //_trayIcon.Stop();

            _trayIcon.Start();
            Hide();
        }

    }
}
