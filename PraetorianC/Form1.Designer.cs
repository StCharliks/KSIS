namespace PraetorianC
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.push_button = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.reg_button = new System.Windows.Forms.Button();
            this.logOut = new System.Windows.Forms.Button();
            this.signIn = new System.Windows.Forms.Button();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.loginBox = new System.Windows.Forms.TextBox();
            this.FileNameBox = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // push_button
            // 
            this.push_button.Location = new System.Drawing.Point(701, 481);
            this.push_button.Name = "push_button";
            this.push_button.Size = new System.Drawing.Size(200, 56);
            this.push_button.TabIndex = 0;
            this.push_button.Text = "Загрузить на сервер";
            this.push_button.UseVisualStyleBackColor = true;
            this.push_button.Click += new System.EventHandler(this.push_button_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.reg_button);
            this.panel1.Controls.Add(this.logOut);
            this.panel1.Controls.Add(this.signIn);
            this.panel1.Controls.Add(this.passwordBox);
            this.panel1.Controls.Add(this.loginBox);
            this.panel1.Location = new System.Drawing.Point(693, 113);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(238, 216);
            this.panel1.TabIndex = 1;
            // 
            // reg_button
            // 
            this.reg_button.Location = new System.Drawing.Point(69, 127);
            this.reg_button.Name = "reg_button";
            this.reg_button.Size = new System.Drawing.Size(103, 23);
            this.reg_button.TabIndex = 4;
            this.reg_button.Text = "Регистрация";
            this.reg_button.UseVisualStyleBackColor = true;
            this.reg_button.Click += new System.EventHandler(this.reg_button_Click);
            // 
            // logOut
            // 
            this.logOut.Location = new System.Drawing.Point(133, 156);
            this.logOut.Name = "logOut";
            this.logOut.Size = new System.Drawing.Size(75, 23);
            this.logOut.TabIndex = 3;
            this.logOut.Text = "Выйти";
            this.logOut.UseVisualStyleBackColor = true;
            // 
            // signIn
            // 
            this.signIn.Location = new System.Drawing.Point(36, 156);
            this.signIn.Name = "signIn";
            this.signIn.Size = new System.Drawing.Size(75, 23);
            this.signIn.TabIndex = 2;
            this.signIn.Text = "Войти";
            this.signIn.UseVisualStyleBackColor = true;
            this.signIn.Click += new System.EventHandler(this.signIn_Click);
            // 
            // passwordBox
            // 
            this.passwordBox.Location = new System.Drawing.Point(36, 92);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.PasswordChar = '*';
            this.passwordBox.Size = new System.Drawing.Size(172, 22);
            this.passwordBox.TabIndex = 1;
            // 
            // loginBox
            // 
            this.loginBox.Location = new System.Drawing.Point(36, 34);
            this.loginBox.Name = "loginBox";
            this.loginBox.Size = new System.Drawing.Size(172, 22);
            this.loginBox.TabIndex = 0;
            // 
            // FileNameBox
            // 
            this.FileNameBox.FormattingEnabled = true;
            this.FileNameBox.ItemHeight = 16;
            this.FileNameBox.Location = new System.Drawing.Point(26, 113);
            this.FileNameBox.Name = "FileNameBox";
            this.FileNameBox.Size = new System.Drawing.Size(631, 436);
            this.FileNameBox.TabIndex = 2;
            this.FileNameBox.SelectedIndexChanged += new System.EventHandler(this.FileNameBox_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(701, 379);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(200, 56);
            this.button1.TabIndex = 3;
            this.button1.Text = "Получить с сервера";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label1.Location = new System.Drawing.Point(269, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(388, 73);
            this.label1.TabIndex = 4;
            this.label1.Text = "Praetorian";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 571);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.FileNameBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.push_button);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Praetorian Client";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button push_button;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button logOut;
        private System.Windows.Forms.Button signIn;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.TextBox loginBox;
        private System.Windows.Forms.Button reg_button;
        private System.Windows.Forms.ListBox FileNameBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}

