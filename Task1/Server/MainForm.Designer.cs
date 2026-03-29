namespace Server
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
            richTextBoxServer = new RichTextBox();
            label3 = new Label();
            buttonOffnOnServer = new Button();
            SuspendLayout();
            // 
            // richTextBoxServer
            // 
            richTextBoxServer.Location = new Point(12, 37);
            richTextBoxServer.Name = "richTextBoxServer";
            richTextBoxServer.Size = new Size(778, 302);
            richTextBoxServer.TabIndex = 4;
            richTextBoxServer.Text = "";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 9);
            label3.Name = "label3";
            label3.Size = new Size(114, 15);
            label3.TabIndex = 13;
            label3.Text = "Серверная сторона";
            // 
            // buttonOffnOnServer
            // 
            buttonOffnOnServer.Location = new Point(667, 346);
            buttonOffnOnServer.Name = "buttonOffnOnServer";
            buttonOffnOnServer.Size = new Size(119, 23);
            buttonOffnOnServer.TabIndex = 14;
            buttonOffnOnServer.Text = "Включить сервер";
            buttonOffnOnServer.UseVisualStyleBackColor = true;
            buttonOffnOnServer.Click += buttonOffnOnServer_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(798, 381);
            Controls.Add(buttonOffnOnServer);
            Controls.Add(label3);
            Controls.Add(richTextBoxServer);
            Name = "MainForm";
            Text = "MainForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private RichTextBox richTextBoxServer;
        private Label label3;
        private Button buttonOffnOnServer;
    }
}