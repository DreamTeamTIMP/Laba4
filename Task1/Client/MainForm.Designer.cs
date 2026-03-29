namespace Task1
{
    partial class MainForm
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
            comboBoxDirectory = new ComboBox();
            listBoxFolders = new ListBox();
            label1 = new Label();
            richTextBoxIPAdress = new RichTextBox();
            richTextBoxClient = new RichTextBox();
            buttonConnect = new Button();
            buttonDisconnect = new Button();
            buttonExit = new Button();
            buttonSendToServer = new Button();
            labelCLient = new Label();
            SuspendLayout();
            // 
            // comboBoxDirectory
            // 
            comboBoxDirectory.FormattingEnabled = true;
            comboBoxDirectory.Location = new Point(11, 12);
            comboBoxDirectory.Name = "comboBoxDirectory";
            comboBoxDirectory.Size = new Size(250, 23);
            comboBoxDirectory.TabIndex = 0;
            comboBoxDirectory.SelectedIndexChanged += comboBoxDirectory_SelectedIndexChanged;
            // 
            // listBoxFolders
            // 
            listBoxFolders.FormattingEnabled = true;
            listBoxFolders.ItemHeight = 15;
            listBoxFolders.Location = new Point(12, 40);
            listBoxFolders.Name = "listBoxFolders";
            listBoxFolders.Size = new Size(249, 229);
            listBoxFolders.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 286);
            label1.Name = "label1";
            label1.Size = new Size(53, 15);
            label1.TabIndex = 2;
            label1.Text = "IP-адрес";
            // 
            // richTextBoxIPAdress
            // 
            richTextBoxIPAdress.Location = new Point(70, 282);
            richTextBoxIPAdress.Name = "richTextBoxIPAdress";
            richTextBoxIPAdress.Size = new Size(191, 25);
            richTextBoxIPAdress.TabIndex = 3;
            richTextBoxIPAdress.Text = "";
            // 
            // richTextBoxClient
            // 
            richTextBoxClient.Location = new Point(276, 37);
            richTextBoxClient.Name = "richTextBoxClient";
            richTextBoxClient.Size = new Size(510, 332);
            richTextBoxClient.TabIndex = 5;
            richTextBoxClient.Text = "";
            // 
            // buttonConnect
            // 
            buttonConnect.Location = new Point(11, 319);
            buttonConnect.Name = "buttonConnect";
            buttonConnect.Size = new Size(131, 23);
            buttonConnect.TabIndex = 7;
            buttonConnect.Text = "Соединиться";
            buttonConnect.UseVisualStyleBackColor = true;
            buttonConnect.Click += buttonConnect_Click;
            // 
            // buttonDisconnect
            // 
            buttonDisconnect.Location = new Point(153, 319);
            buttonDisconnect.Name = "buttonDisconnect";
            buttonDisconnect.Size = new Size(108, 23);
            buttonDisconnect.TabIndex = 8;
            buttonDisconnect.Text = "Отключиться";
            buttonDisconnect.UseVisualStyleBackColor = true;
            buttonDisconnect.Click += buttonDisconnect_Click;
            // 
            // buttonExit
            // 
            buttonExit.Location = new Point(153, 346);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(108, 23);
            buttonExit.TabIndex = 9;
            buttonExit.Text = "Выход";
            buttonExit.UseVisualStyleBackColor = true;
            buttonExit.Click += buttonExit_Click;
            // 
            // buttonSendToServer
            // 
            buttonSendToServer.Location = new Point(11, 348);
            buttonSendToServer.Name = "buttonSendToServer";
            buttonSendToServer.Size = new Size(131, 23);
            buttonSendToServer.TabIndex = 11;
            buttonSendToServer.Text = "Передать сервер";
            buttonSendToServer.UseVisualStyleBackColor = true;
            buttonSendToServer.Click += buttonSendToServer_Click;
            // 
            // labelCLient
            // 
            labelCLient.AutoSize = true;
            labelCLient.Location = new Point(288, 15);
            labelCLient.Name = "labelCLient";
            labelCLient.Size = new Size(118, 15);
            labelCLient.TabIndex = 12;
            labelCLient.Text = "Клиентская сторона";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(798, 381);
            Controls.Add(labelCLient);
            Controls.Add(buttonSendToServer);
            Controls.Add(buttonExit);
            Controls.Add(buttonDisconnect);
            Controls.Add(buttonConnect);
            Controls.Add(richTextBoxClient);
            Controls.Add(richTextBoxIPAdress);
            Controls.Add(label1);
            Controls.Add(comboBoxDirectory);
            Controls.Add(listBoxFolders);
            Name = "MainForm";
            Text = "Клиент";
            FormClosing += MainForm_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ComboBox comboBoxDirectory;
        private ListBox listBoxFolders;
        private Label label1;
        private RichTextBox richTextBoxIPAdress;
        private RichTextBox richTextBoxClient;
        private Button buttonConnect;
        private Button buttonDisconnect;
        private Button buttonExit;
        private Button buttonSendToServer;
        private Label labelCLient;
    }
}