using System.Drawing;

namespace SkybetAccBot
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ui_btn_save = new Guna.UI2.WinForms.Guna2Button();
            this.ui_btn_clear = new Guna.UI2.WinForms.Guna2Button();
            this.ui_btn_randompassword = new Guna.UI2.WinForms.Guna2Button();
            this.ui_password = new Guna.UI2.WinForms.Guna2TextBox();
            this.ui_btn_open = new Guna.UI2.WinForms.Guna2Button();
            this.btn_ui_create = new Guna.UI2.WinForms.Guna2Button();
            this.ui_filepath = new Guna.UI2.WinForms.Guna2TextBox();
            this.groupBox2 = new Guna.UI2.WinForms.Guna2Panel();
            this.ui_proxypass = new Guna.UI2.WinForms.Guna2TextBox();
            this.ui_proxyURL = new Guna.UI2.WinForms.Guna2TextBox();
            this.ui_proxyuser = new Guna.UI2.WinForms.Guna2TextBox();
            this.ui_log = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2HtmlLabel1 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.panelTitleBar = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.btn_win_close = new Guna.UI2.WinForms.Guna2Button();
            this.btn_win_max = new Guna.UI2.WinForms.Guna2Button();
            this.btn_win_min = new Guna.UI2.WinForms.Guna2Button();
            this.guna2HtmlLabel2 = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.picIcon = new Guna.UI2.WinForms.Guna2PictureBox();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // ui_btn_save
            // 
            this.ui_btn_save.BorderRadius = 12;
            this.ui_btn_save.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.ui_btn_save.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.ui_btn_save.ForeColor = System.Drawing.Color.White;
            this.ui_btn_save.Location = new System.Drawing.Point(20, 230);
            this.ui_btn_save.Name = "ui_btn_save";
            this.ui_btn_save.Size = new System.Drawing.Size(167, 30);
            this.ui_btn_save.TabIndex = 2;
            this.ui_btn_save.Text = "Save";
            this.ui_btn_save.Click += new System.EventHandler(this.ui_btn_save_Click);
            // 
            // ui_btn_clear
            // 
            this.ui_btn_clear.BorderRadius = 12;
            this.ui_btn_clear.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.ui_btn_clear.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.ui_btn_clear.ForeColor = System.Drawing.Color.White;
            this.ui_btn_clear.Location = new System.Drawing.Point(207, 230);
            this.ui_btn_clear.Name = "ui_btn_clear";
            this.ui_btn_clear.Size = new System.Drawing.Size(179, 30);
            this.ui_btn_clear.TabIndex = 3;
            this.ui_btn_clear.Text = "Clear";
            this.ui_btn_clear.Click += new System.EventHandler(this.ui_btn_clear_Click);
            // 
            // ui_btn_randompassword
            // 
            this.ui_btn_randompassword.BorderRadius = 12;
            this.ui_btn_randompassword.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.ui_btn_randompassword.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.ui_btn_randompassword.ForeColor = System.Drawing.Color.White;
            this.ui_btn_randompassword.Location = new System.Drawing.Point(292, 152);
            this.ui_btn_randompassword.Name = "ui_btn_randompassword";
            this.ui_btn_randompassword.Size = new System.Drawing.Size(94, 27);
            this.ui_btn_randompassword.TabIndex = 62;
            this.ui_btn_randompassword.Text = "Generate";
            this.ui_btn_randompassword.Click += new System.EventHandler(this.ui_btn_randompassword_Click);
            // 
            // ui_password
            // 
            this.ui_password.BorderRadius = 10;
            this.ui_password.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ui_password.DefaultText = "";
            this.ui_password.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.ui_password.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.ui_password.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.ui_password.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.ui_password.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.ui_password.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ui_password.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ui_password.ForeColor = System.Drawing.Color.White;
            this.ui_password.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ui_password.Location = new System.Drawing.Point(20, 153);
            this.ui_password.Name = "ui_password";
            this.ui_password.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.ui_password.PlaceholderText = "Login PIN";
            this.ui_password.SelectedText = "";
            this.ui_password.Size = new System.Drawing.Size(266, 20);
            this.ui_password.TabIndex = 59;
            this.ui_password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ui_btn_open
            // 
            this.ui_btn_open.BorderRadius = 12;
            this.ui_btn_open.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.ui_btn_open.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.ui_btn_open.ForeColor = System.Drawing.Color.White;
            this.ui_btn_open.Location = new System.Drawing.Point(359, 189);
            this.ui_btn_open.Name = "ui_btn_open";
            this.ui_btn_open.Size = new System.Drawing.Size(27, 27);
            this.ui_btn_open.TabIndex = 61;
            this.ui_btn_open.Text = "...";
            this.ui_btn_open.Click += new System.EventHandler(this.ui_btn_open_Click);
            // 
            // btn_ui_create
            // 
            this.btn_ui_create.BorderRadius = 12;
            this.btn_ui_create.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btn_ui_create.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btn_ui_create.ForeColor = System.Drawing.Color.White;
            this.btn_ui_create.Location = new System.Drawing.Point(20, 277);
            this.btn_ui_create.Name = "btn_ui_create";
            this.btn_ui_create.Size = new System.Drawing.Size(366, 30);
            this.btn_ui_create.TabIndex = 55;
            this.btn_ui_create.Text = "Create Skybet Account";
            this.btn_ui_create.Click += new System.EventHandler(this.btn_ui_create_Click);
            // 
            // ui_filepath
            // 
            this.ui_filepath.BorderRadius = 10;
            this.ui_filepath.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ui_filepath.DefaultText = "";
            this.ui_filepath.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.ui_filepath.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.ui_filepath.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.ui_filepath.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.ui_filepath.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.ui_filepath.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ui_filepath.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ui_filepath.ForeColor = System.Drawing.Color.White;
            this.ui_filepath.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ui_filepath.Location = new System.Drawing.Point(20, 192);
            this.ui_filepath.Name = "ui_filepath";
            this.ui_filepath.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.ui_filepath.PlaceholderText = "File Path";
            this.ui_filepath.SelectedText = "";
            this.ui_filepath.Size = new System.Drawing.Size(328, 23);
            this.ui_filepath.TabIndex = 60;
            this.ui_filepath.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(24)))), ((int)(((byte)(39)))));
            this.groupBox2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(85)))), ((int)(((byte)(99)))));
            this.groupBox2.BorderRadius = 8;
            this.groupBox2.BorderThickness = 1;
            this.groupBox2.Controls.Add(this.ui_password);
            this.groupBox2.Controls.Add(this.btn_ui_create);
            this.groupBox2.Controls.Add(this.ui_proxypass);
            this.groupBox2.Controls.Add(this.ui_btn_open);
            this.groupBox2.Controls.Add(this.ui_proxyURL);
            this.groupBox2.Controls.Add(this.ui_btn_clear);
            this.groupBox2.Controls.Add(this.ui_btn_randompassword);
            this.groupBox2.Controls.Add(this.ui_proxyuser);
            this.groupBox2.Controls.Add(this.ui_btn_save);
            this.groupBox2.Controls.Add(this.ui_filepath);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(14, 44);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(404, 328);
            this.groupBox2.TabIndex = 56;
            // 
            // ui_proxypass
            // 
            this.ui_proxypass.BorderRadius = 10;
            this.ui_proxypass.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ui_proxypass.DefaultText = "";
            this.ui_proxypass.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.ui_proxypass.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.ui_proxypass.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.ui_proxypass.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.ui_proxypass.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.ui_proxypass.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ui_proxypass.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ui_proxypass.ForeColor = System.Drawing.Color.White;
            this.ui_proxypass.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ui_proxypass.Location = new System.Drawing.Point(20, 113);
            this.ui_proxypass.Name = "ui_proxypass";
            this.ui_proxypass.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.ui_proxypass.PlaceholderText = "Proxy Password";
            this.ui_proxypass.SelectedText = "";
            this.ui_proxypass.Size = new System.Drawing.Size(365, 20);
            this.ui_proxypass.TabIndex = 48;
            this.ui_proxypass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ui_proxyURL
            // 
            this.ui_proxyURL.BorderRadius = 10;
            this.ui_proxyURL.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ui_proxyURL.DefaultText = "";
            this.ui_proxyURL.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.ui_proxyURL.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.ui_proxyURL.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.ui_proxyURL.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.ui_proxyURL.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.ui_proxyURL.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ui_proxyURL.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ui_proxyURL.ForeColor = System.Drawing.Color.White;
            this.ui_proxyURL.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ui_proxyURL.Location = new System.Drawing.Point(20, 23);
            this.ui_proxyURL.Name = "ui_proxyURL";
            this.ui_proxyURL.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.ui_proxyURL.PlaceholderText = "Proxy URL";
            this.ui_proxyURL.SelectedText = "";
            this.ui_proxyURL.Size = new System.Drawing.Size(365, 20);
            this.ui_proxyURL.TabIndex = 46;
            this.ui_proxyURL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ui_proxyuser
            // 
            this.ui_proxyuser.BorderRadius = 10;
            this.ui_proxyuser.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ui_proxyuser.DefaultText = "";
            this.ui_proxyuser.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.ui_proxyuser.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.ui_proxyuser.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.ui_proxyuser.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.ui_proxyuser.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.ui_proxyuser.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ui_proxyuser.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ui_proxyuser.ForeColor = System.Drawing.Color.White;
            this.ui_proxyuser.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ui_proxyuser.Location = new System.Drawing.Point(20, 67);
            this.ui_proxyuser.Name = "ui_proxyuser";
            this.ui_proxyuser.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.ui_proxyuser.PlaceholderText = "Proxy Username";
            this.ui_proxyuser.SelectedText = "";
            this.ui_proxyuser.Size = new System.Drawing.Size(365, 20);
            this.ui_proxyuser.TabIndex = 46;
            this.ui_proxyuser.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ui_log
            // 
            this.ui_log.AcceptsTab = true;
            this.ui_log.BorderRadius = 10;
            this.ui_log.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ui_log.DefaultText = "";
            this.ui_log.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.ui_log.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.ui_log.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.ui_log.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.ui_log.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(41)))), ((int)(((byte)(55)))));
            this.ui_log.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ui_log.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ui_log.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.ui_log.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.ui_log.Location = new System.Drawing.Point(424, 44);
            this.ui_log.Multiline = true;
            this.ui_log.Name = "ui_log";
            this.ui_log.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.ui_log.PlaceholderText = "Logs will appear here...";
            this.ui_log.ReadOnly = true;
            this.ui_log.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ui_log.SelectedText = "";
            this.ui_log.Size = new System.Drawing.Size(428, 328);
            this.ui_log.TabIndex = 54;
            this.ui_log.TextChanged += new System.EventHandler(this.ui_log_TextChanged);
            // 
            // guna2HtmlLabel1
            // 
            this.guna2HtmlLabel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2HtmlLabel1.Location = new System.Drawing.Point(0, 0);
            this.guna2HtmlLabel1.Name = "guna2HtmlLabel1";
            this.guna2HtmlLabel1.Size = new System.Drawing.Size(86, 15);
            this.guna2HtmlLabel1.TabIndex = 50;
            this.guna2HtmlLabel1.Text = "guna2HtmlLabel1";
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.AutoSize = false;
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(15)))), ((int)(((byte)(50)))));
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Font = new System.Drawing.Font("Segoe Script", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelTitleBar.ForeColor = System.Drawing.Color.White;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(862, 40);
            this.panelTitleBar.TabIndex = 58;
            this.panelTitleBar.Text = null;
            // 
            // btn_win_close
            // 
            this.btn_win_close.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_win_close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(15)))), ((int)(((byte)(50)))));
            this.btn_win_close.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_win_close.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_win_close.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_win_close.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_win_close.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(15)))), ((int)(((byte)(50)))));
            this.btn_win_close.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btn_win_close.ForeColor = System.Drawing.Color.White;
            this.btn_win_close.Location = new System.Drawing.Point(822, 0);
            this.btn_win_close.Name = "btn_win_close";
            this.btn_win_close.Size = new System.Drawing.Size(40, 40);
            this.btn_win_close.TabIndex = 59;
            this.btn_win_close.Text = "X";
            this.btn_win_close.Click += new System.EventHandler(this.btn_win_close_Click);
            // 
            // btn_win_max
            // 
            this.btn_win_max.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_win_max.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(15)))), ((int)(((byte)(50)))));
            this.btn_win_max.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_win_max.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_win_max.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_win_max.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_win_max.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(15)))), ((int)(((byte)(50)))));
            this.btn_win_max.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.btn_win_max.ForeColor = System.Drawing.Color.White;
            this.btn_win_max.Location = new System.Drawing.Point(776, 0);
            this.btn_win_max.Name = "btn_win_max";
            this.btn_win_max.Size = new System.Drawing.Size(40, 40);
            this.btn_win_max.TabIndex = 60;
            this.btn_win_max.Text = "□";
            this.btn_win_max.Click += new System.EventHandler(this.btn_win_max_Click);
            // 
            // btn_win_min
            // 
            this.btn_win_min.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btn_win_min.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(15)))), ((int)(((byte)(50)))));
            this.btn_win_min.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btn_win_min.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btn_win_min.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btn_win_min.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btn_win_min.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(15)))), ((int)(((byte)(50)))));
            this.btn_win_min.Font = new System.Drawing.Font("Segoe UI", 15F);
            this.btn_win_min.ForeColor = System.Drawing.Color.White;
            this.btn_win_min.Location = new System.Drawing.Point(730, 0);
            this.btn_win_min.Name = "btn_win_min";
            this.btn_win_min.Size = new System.Drawing.Size(40, 40);
            this.btn_win_min.TabIndex = 61;
            this.btn_win_min.Text = "_";
            this.btn_win_min.Click += new System.EventHandler(this.btn_win_min_Click);
            // 
            // guna2HtmlLabel2
            // 
            this.guna2HtmlLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(15)))), ((int)(((byte)(50)))));
            this.guna2HtmlLabel2.Enabled = false;
            this.guna2HtmlLabel2.Font = new System.Drawing.Font("Segoe Script", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2HtmlLabel2.ForeColor = System.Drawing.Color.White;
            this.guna2HtmlLabel2.IsContextMenuEnabled = false;
            this.guna2HtmlLabel2.IsSelectionEnabled = false;
            this.guna2HtmlLabel2.Location = new System.Drawing.Point(71, 3);
            this.guna2HtmlLabel2.Name = "guna2HtmlLabel2";
            this.guna2HtmlLabel2.Size = new System.Drawing.Size(162, 35);
            this.guna2HtmlLabel2.TabIndex = 62;
            this.guna2HtmlLabel2.Text = "Harry_Bot_Pro";
            // 
            // picIcon
            // 
            this.picIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(15)))), ((int)(((byte)(50)))));
            this.picIcon.Enabled = false;
            this.picIcon.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(15)))), ((int)(((byte)(50)))));
            this.picIcon.Image = global::SkybetAccBot.Properties.Resources.ChatGPT_Image_Jul_16__2026__05_44_18_PM1;
            this.picIcon.ImageRotate = 0F;
            this.picIcon.Location = new System.Drawing.Point(14, 3);
            this.picIcon.Name = "picIcon";
            this.picIcon.ShadowDecoration.Color = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(15)))), ((int)(((byte)(50)))));
            this.picIcon.Size = new System.Drawing.Size(32, 32);
            this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picIcon.TabIndex = 63;
            this.picIcon.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(15)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(862, 384);
            this.Controls.Add(this.picIcon);
            this.Controls.Add(this.guna2HtmlLabel2);
            this.Controls.Add(this.btn_win_min);
            this.Controls.Add(this.btn_win_max);
            this.Controls.Add(this.btn_win_close);
            this.Controls.Add(this.panelTitleBar);
            this.Controls.Add(this.guna2HtmlLabel1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.ui_log);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "SuperAccount-Manager";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Guna.UI2.WinForms.Guna2Button ui_btn_save;
        private Guna.UI2.WinForms.Guna2Button ui_btn_clear;
        private Guna.UI2.WinForms.Guna2Button ui_btn_randompassword;
        private Guna.UI2.WinForms.Guna2TextBox ui_password;
        private Guna.UI2.WinForms.Guna2Button ui_btn_open;
        private Guna.UI2.WinForms.Guna2Button btn_ui_create;
        private Guna.UI2.WinForms.Guna2TextBox ui_filepath;
        private Guna.UI2.WinForms.Guna2Panel groupBox2;
        private Guna.UI2.WinForms.Guna2TextBox ui_proxypass;
        private Guna.UI2.WinForms.Guna2TextBox ui_proxyURL;
        private Guna.UI2.WinForms.Guna2TextBox ui_proxyuser;
        private Guna.UI2.WinForms.Guna2TextBox ui_log;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel1;
        private Guna.UI2.WinForms.Guna2HtmlLabel panelTitleBar;
        private Guna.UI2.WinForms.Guna2Button btn_win_close;
        private Guna.UI2.WinForms.Guna2Button btn_win_max;
        private Guna.UI2.WinForms.Guna2Button btn_win_min;
        private Guna.UI2.WinForms.Guna2HtmlLabel guna2HtmlLabel2;
        private Guna.UI2.WinForms.Guna2PictureBox picIcon;
    }
}
