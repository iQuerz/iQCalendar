
namespace SettingsUI
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Tabs = new System.Windows.Forms.TabControl();
            this.ServerTab = new System.Windows.Forms.TabPage();
            this.EmailPasswordEye = new System.Windows.Forms.PictureBox();
            this.NotificationTimeUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.EmailPasswordTextbox = new System.Windows.Forms.TextBox();
            this.EmailUsernameTextbox = new System.Windows.Forms.TextBox();
            this.PortTextbox = new System.Windows.Forms.TextBox();
            this.NameTextbox = new System.Windows.Forms.TextBox();
            this.NotificationTimeLabel = new System.Windows.Forms.Label();
            this.EmailPasswordLabel = new System.Windows.Forms.Label();
            this.EmailUsernameLabel = new System.Windows.Forms.Label();
            this.PortLabel = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.Label();
            this.AccountsTab = new System.Windows.Forms.TabPage();
            this.PasswordTab = new System.Windows.Forms.TabPage();
            this.ConfirmPasswordTextbox = new System.Windows.Forms.TextBox();
            this.NewPasswordTextbox = new System.Windows.Forms.TextBox();
            this.OldPasswordTextbox = new System.Windows.Forms.TextBox();
            this.ConfirmPasswordLabel = new System.Windows.Forms.Label();
            this.NewPasswordLabel = new System.Windows.Forms.Label();
            this.OldPasswordLabel = new System.Windows.Forms.Label();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.OldPasswordEye = new System.Windows.Forms.PictureBox();
            this.NewPasswordEye = new System.Windows.Forms.PictureBox();
            this.ConfirmPasswordEye = new System.Windows.Forms.PictureBox();
            this.Tabs.SuspendLayout();
            this.ServerTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EmailPasswordEye)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NotificationTimeUpDown)).BeginInit();
            this.PasswordTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OldPasswordEye)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewPasswordEye)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConfirmPasswordEye)).BeginInit();
            this.SuspendLayout();
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.ServerTab);
            this.Tabs.Controls.Add(this.AccountsTab);
            this.Tabs.Controls.Add(this.PasswordTab);
            this.Tabs.Location = new System.Drawing.Point(12, 12);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(393, 208);
            this.Tabs.TabIndex = 0;
            this.Tabs.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.Tabs_Selecting);
            this.Tabs.TabIndexChanged += new System.EventHandler(this.Tabs_TabIndexChanged);
            // 
            // ServerTab
            // 
            this.ServerTab.Controls.Add(this.EmailPasswordEye);
            this.ServerTab.Controls.Add(this.NotificationTimeUpDown);
            this.ServerTab.Controls.Add(this.label1);
            this.ServerTab.Controls.Add(this.EmailPasswordTextbox);
            this.ServerTab.Controls.Add(this.EmailUsernameTextbox);
            this.ServerTab.Controls.Add(this.PortTextbox);
            this.ServerTab.Controls.Add(this.NameTextbox);
            this.ServerTab.Controls.Add(this.NotificationTimeLabel);
            this.ServerTab.Controls.Add(this.EmailPasswordLabel);
            this.ServerTab.Controls.Add(this.EmailUsernameLabel);
            this.ServerTab.Controls.Add(this.PortLabel);
            this.ServerTab.Controls.Add(this.NameLabel);
            this.ServerTab.Location = new System.Drawing.Point(4, 22);
            this.ServerTab.Name = "ServerTab";
            this.ServerTab.Padding = new System.Windows.Forms.Padding(3);
            this.ServerTab.Size = new System.Drawing.Size(385, 182);
            this.ServerTab.TabIndex = 0;
            this.ServerTab.Text = "Server";
            this.ServerTab.UseVisualStyleBackColor = true;
            // 
            // EmailPasswordEye
            // 
            this.EmailPasswordEye.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.EmailPasswordEye.Cursor = System.Windows.Forms.Cursors.Hand;
            this.EmailPasswordEye.Location = new System.Drawing.Point(356, 91);
            this.EmailPasswordEye.Name = "EmailPasswordEye";
            this.EmailPasswordEye.Size = new System.Drawing.Size(25, 25);
            this.EmailPasswordEye.TabIndex = 11;
            this.EmailPasswordEye.TabStop = false;
            this.EmailPasswordEye.Click += new System.EventHandler(this.EmailPasswordEye_Click);
            this.EmailPasswordEye.DoubleClick += new System.EventHandler(this.EmailPasswordEye_DoubleClick);
            // 
            // NotificationTimeUpDown
            // 
            this.NotificationTimeUpDown.Location = new System.Drawing.Point(140, 119);
            this.NotificationTimeUpDown.Maximum = new decimal(new int[] {
            23,
            0,
            0,
            0});
            this.NotificationTimeUpDown.Name = "NotificationTimeUpDown";
            this.NotificationTimeUpDown.Size = new System.Drawing.Size(52, 20);
            this.NotificationTimeUpDown.TabIndex = 9;
            this.NotificationTimeUpDown.ValueChanged += new System.EventHandler(this.NotificationTimeUpDown_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(189, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = ":00 h";
            // 
            // EmailPasswordTextbox
            // 
            this.EmailPasswordTextbox.Location = new System.Drawing.Point(140, 93);
            this.EmailPasswordTextbox.Name = "EmailPasswordTextbox";
            this.EmailPasswordTextbox.Size = new System.Drawing.Size(210, 20);
            this.EmailPasswordTextbox.TabIndex = 8;
            this.EmailPasswordTextbox.UseSystemPasswordChar = true;
            this.EmailPasswordTextbox.TextChanged += new System.EventHandler(this.EmailPasswordTextbox_TextChanged);
            // 
            // EmailUsernameTextbox
            // 
            this.EmailUsernameTextbox.Location = new System.Drawing.Point(140, 67);
            this.EmailUsernameTextbox.Name = "EmailUsernameTextbox";
            this.EmailUsernameTextbox.Size = new System.Drawing.Size(210, 20);
            this.EmailUsernameTextbox.TabIndex = 7;
            this.EmailUsernameTextbox.TextChanged += new System.EventHandler(this.EmailUsernameTextbox_TextChanged);
            // 
            // PortTextbox
            // 
            this.PortTextbox.Location = new System.Drawing.Point(140, 41);
            this.PortTextbox.Name = "PortTextbox";
            this.PortTextbox.Size = new System.Drawing.Size(210, 20);
            this.PortTextbox.TabIndex = 6;
            this.PortTextbox.TextChanged += new System.EventHandler(this.PortTextbox_TextChanged);
            // 
            // NameTextbox
            // 
            this.NameTextbox.Location = new System.Drawing.Point(140, 15);
            this.NameTextbox.Name = "NameTextbox";
            this.NameTextbox.Size = new System.Drawing.Size(210, 20);
            this.NameTextbox.TabIndex = 5;
            this.NameTextbox.TextChanged += new System.EventHandler(this.NameTextbox_TextChanged);
            // 
            // NotificationTimeLabel
            // 
            this.NotificationTimeLabel.AutoSize = true;
            this.NotificationTimeLabel.Location = new System.Drawing.Point(23, 121);
            this.NotificationTimeLabel.Name = "NotificationTimeLabel";
            this.NotificationTimeLabel.Size = new System.Drawing.Size(111, 13);
            this.NotificationTimeLabel.TabIndex = 4;
            this.NotificationTimeLabel.Text = "Email notification time:";
            // 
            // EmailPasswordLabel
            // 
            this.EmailPasswordLabel.AutoSize = true;
            this.EmailPasswordLabel.Location = new System.Drawing.Point(26, 96);
            this.EmailPasswordLabel.Name = "EmailPasswordLabel";
            this.EmailPasswordLabel.Size = new System.Drawing.Size(108, 13);
            this.EmailPasswordLabel.TabIndex = 3;
            this.EmailPasswordLabel.Text = "Host email Password:";
            // 
            // EmailUsernameLabel
            // 
            this.EmailUsernameLabel.AutoSize = true;
            this.EmailUsernameLabel.Location = new System.Drawing.Point(24, 70);
            this.EmailUsernameLabel.Name = "EmailUsernameLabel";
            this.EmailUsernameLabel.Size = new System.Drawing.Size(110, 13);
            this.EmailUsernameLabel.TabIndex = 2;
            this.EmailUsernameLabel.Text = "Host email Username:";
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(80, 44);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.Size = new System.Drawing.Size(54, 13);
            this.PortLabel.TabIndex = 1;
            this.PortLabel.Text = "Host Port:";
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(62, 18);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(72, 13);
            this.NameLabel.TabIndex = 0;
            this.NameLabel.Text = "Server Name:";
            // 
            // AccountsTab
            // 
            this.AccountsTab.Location = new System.Drawing.Point(4, 22);
            this.AccountsTab.Name = "AccountsTab";
            this.AccountsTab.Padding = new System.Windows.Forms.Padding(3);
            this.AccountsTab.Size = new System.Drawing.Size(385, 182);
            this.AccountsTab.TabIndex = 1;
            this.AccountsTab.Text = "Accounts";
            this.AccountsTab.UseVisualStyleBackColor = true;
            // 
            // PasswordTab
            // 
            this.PasswordTab.Controls.Add(this.ConfirmPasswordEye);
            this.PasswordTab.Controls.Add(this.NewPasswordEye);
            this.PasswordTab.Controls.Add(this.OldPasswordEye);
            this.PasswordTab.Controls.Add(this.ConfirmPasswordTextbox);
            this.PasswordTab.Controls.Add(this.NewPasswordTextbox);
            this.PasswordTab.Controls.Add(this.OldPasswordTextbox);
            this.PasswordTab.Controls.Add(this.ConfirmPasswordLabel);
            this.PasswordTab.Controls.Add(this.NewPasswordLabel);
            this.PasswordTab.Controls.Add(this.OldPasswordLabel);
            this.PasswordTab.Location = new System.Drawing.Point(4, 22);
            this.PasswordTab.Name = "PasswordTab";
            this.PasswordTab.Padding = new System.Windows.Forms.Padding(3);
            this.PasswordTab.Size = new System.Drawing.Size(385, 182);
            this.PasswordTab.TabIndex = 2;
            this.PasswordTab.Text = "Change Password";
            this.PasswordTab.UseVisualStyleBackColor = true;
            // 
            // ConfirmPasswordTextbox
            // 
            this.ConfirmPasswordTextbox.Location = new System.Drawing.Point(139, 67);
            this.ConfirmPasswordTextbox.Name = "ConfirmPasswordTextbox";
            this.ConfirmPasswordTextbox.Size = new System.Drawing.Size(152, 20);
            this.ConfirmPasswordTextbox.TabIndex = 5;
            this.ConfirmPasswordTextbox.UseSystemPasswordChar = true;
            this.ConfirmPasswordTextbox.TextChanged += new System.EventHandler(this.ConfirmPasswordTextbox_TextChanged);
            // 
            // NewPasswordTextbox
            // 
            this.NewPasswordTextbox.Location = new System.Drawing.Point(139, 41);
            this.NewPasswordTextbox.Name = "NewPasswordTextbox";
            this.NewPasswordTextbox.Size = new System.Drawing.Size(152, 20);
            this.NewPasswordTextbox.TabIndex = 4;
            this.NewPasswordTextbox.UseSystemPasswordChar = true;
            this.NewPasswordTextbox.TextChanged += new System.EventHandler(this.NewPasswordTextbox_TextChanged);
            // 
            // OldPasswordTextbox
            // 
            this.OldPasswordTextbox.Location = new System.Drawing.Point(139, 15);
            this.OldPasswordTextbox.Name = "OldPasswordTextbox";
            this.OldPasswordTextbox.Size = new System.Drawing.Size(152, 20);
            this.OldPasswordTextbox.TabIndex = 3;
            this.OldPasswordTextbox.UseSystemPasswordChar = true;
            this.OldPasswordTextbox.TextChanged += new System.EventHandler(this.OldPasswordTextbox_TextChanged);
            // 
            // ConfirmPasswordLabel
            // 
            this.ConfirmPasswordLabel.AutoSize = true;
            this.ConfirmPasswordLabel.Location = new System.Drawing.Point(39, 70);
            this.ConfirmPasswordLabel.Name = "ConfirmPasswordLabel";
            this.ConfirmPasswordLabel.Size = new System.Drawing.Size(94, 13);
            this.ConfirmPasswordLabel.TabIndex = 2;
            this.ConfirmPasswordLabel.Text = "Confirm Password:";
            // 
            // NewPasswordLabel
            // 
            this.NewPasswordLabel.AutoSize = true;
            this.NewPasswordLabel.Location = new System.Drawing.Point(52, 44);
            this.NewPasswordLabel.Name = "NewPasswordLabel";
            this.NewPasswordLabel.Size = new System.Drawing.Size(81, 13);
            this.NewPasswordLabel.TabIndex = 1;
            this.NewPasswordLabel.Text = "New Password:";
            // 
            // OldPasswordLabel
            // 
            this.OldPasswordLabel.AutoSize = true;
            this.OldPasswordLabel.Location = new System.Drawing.Point(59, 18);
            this.OldPasswordLabel.Name = "OldPasswordLabel";
            this.OldPasswordLabel.Size = new System.Drawing.Size(75, 13);
            this.OldPasswordLabel.TabIndex = 0;
            this.OldPasswordLabel.Text = "Old Password:";
            // 
            // ApplyButton
            // 
            this.ApplyButton.Location = new System.Drawing.Point(215, 226);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(92, 37);
            this.ApplyButton.TabIndex = 1;
            this.ApplyButton.Text = "Apply";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(117, 226);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(92, 37);
            this.OkButton.TabIndex = 2;
            this.OkButton.Text = "Ok";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(313, 226);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(92, 37);
            this.CancelButton.TabIndex = 3;
            this.CancelButton.Text = "Revert";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OldPasswordEye
            // 
            this.OldPasswordEye.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.OldPasswordEye.Cursor = System.Windows.Forms.Cursors.Hand;
            this.OldPasswordEye.Location = new System.Drawing.Point(297, 13);
            this.OldPasswordEye.Name = "OldPasswordEye";
            this.OldPasswordEye.Size = new System.Drawing.Size(25, 25);
            this.OldPasswordEye.TabIndex = 6;
            this.OldPasswordEye.TabStop = false;
            this.OldPasswordEye.Click += new System.EventHandler(this.OldPasswordEye_Click);
            this.OldPasswordEye.DoubleClick += new System.EventHandler(this.OldPasswordEye_DoubleClick);
            // 
            // NewPasswordEye
            // 
            this.NewPasswordEye.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.NewPasswordEye.Cursor = System.Windows.Forms.Cursors.Hand;
            this.NewPasswordEye.Location = new System.Drawing.Point(297, 39);
            this.NewPasswordEye.Name = "NewPasswordEye";
            this.NewPasswordEye.Size = new System.Drawing.Size(25, 25);
            this.NewPasswordEye.TabIndex = 7;
            this.NewPasswordEye.TabStop = false;
            this.NewPasswordEye.Click += new System.EventHandler(this.NewPasswordEye_Click);
            this.NewPasswordEye.DoubleClick += new System.EventHandler(this.NewPasswordEye_DoubleClick);
            // 
            // ConfirmPasswordEye
            // 
            this.ConfirmPasswordEye.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ConfirmPasswordEye.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ConfirmPasswordEye.Location = new System.Drawing.Point(297, 65);
            this.ConfirmPasswordEye.Name = "ConfirmPasswordEye";
            this.ConfirmPasswordEye.Size = new System.Drawing.Size(25, 25);
            this.ConfirmPasswordEye.TabIndex = 8;
            this.ConfirmPasswordEye.TabStop = false;
            this.ConfirmPasswordEye.Click += new System.EventHandler(this.ConfirmPasswordEye_Click);
            this.ConfirmPasswordEye.DoubleClick += new System.EventHandler(this.ConfirmPasswordEye_DoubleClick);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 275);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.ApplyButton);
            this.Controls.Add(this.Tabs);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(433, 314);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(433, 314);
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "iQCalendar Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Tabs.ResumeLayout(false);
            this.ServerTab.ResumeLayout(false);
            this.ServerTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EmailPasswordEye)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NotificationTimeUpDown)).EndInit();
            this.PasswordTab.ResumeLayout(false);
            this.PasswordTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OldPasswordEye)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NewPasswordEye)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ConfirmPasswordEye)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage ServerTab;
        private System.Windows.Forms.TabPage AccountsTab;
        private System.Windows.Forms.Button ApplyButton;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.TabPage PasswordTab;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.NumericUpDown NotificationTimeUpDown;
        private System.Windows.Forms.TextBox EmailPasswordTextbox;
        private System.Windows.Forms.TextBox EmailUsernameTextbox;
        private System.Windows.Forms.TextBox PortTextbox;
        private System.Windows.Forms.TextBox NameTextbox;
        private System.Windows.Forms.Label NotificationTimeLabel;
        private System.Windows.Forms.Label EmailPasswordLabel;
        private System.Windows.Forms.Label EmailUsernameLabel;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ConfirmPasswordTextbox;
        private System.Windows.Forms.TextBox NewPasswordTextbox;
        private System.Windows.Forms.TextBox OldPasswordTextbox;
        private System.Windows.Forms.Label ConfirmPasswordLabel;
        private System.Windows.Forms.Label NewPasswordLabel;
        private System.Windows.Forms.Label OldPasswordLabel;
        private System.Windows.Forms.PictureBox EmailPasswordEye;
        private System.Windows.Forms.PictureBox OldPasswordEye;
        private System.Windows.Forms.PictureBox ConfirmPasswordEye;
        private System.Windows.Forms.PictureBox NewPasswordEye;
    }
}

