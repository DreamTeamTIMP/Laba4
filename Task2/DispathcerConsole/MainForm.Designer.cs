namespace DispathcerConsole
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            chartTemp = new System.Windows.Forms.DataVisualization.Charting.Chart();
            chartPress = new System.Windows.Forms.DataVisualization.Charting.Chart();
            buttonStart = new Button();
            ((System.ComponentModel.ISupportInitialize)chartTemp).BeginInit();
            ((System.ComponentModel.ISupportInitialize)chartPress).BeginInit();
            SuspendLayout();
            // 
            // chartTemp
            // 
            chartArea1.AxisX.Title = "Время, с.";
            chartArea1.AxisY.Maximum = 100D;
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.AxisY.Title = "градусы";
            chartArea1.Name = "ChartArea1";
            chartTemp.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            chartTemp.Legends.Add(legend1);
            chartTemp.Location = new Point(36, 12);
            chartTemp.Name = "chartTemp";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Legend = "Legend1";
            series1.Name = "Температура";
            chartTemp.Series.Add(series1);
            chartTemp.Size = new Size(346, 339);
            chartTemp.TabIndex = 0;
            chartTemp.Text = "chart1";
            // 
            // chartPress
            // 
            chartArea2.AxisX.Title = "Время, с.";
            chartArea2.AxisY.Maximum = 6D;
            chartArea2.AxisY.Minimum = 0D;
            chartArea2.AxisY.Title = "атм.";
            chartArea2.Name = "ChartArea1";
            chartPress.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            chartPress.Legends.Add(legend2);
            chartPress.Location = new Point(414, 12);
            chartPress.Name = "chartPress";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = Color.Red;
            series2.Legend = "Legend1";
            series2.Name = "Давление";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            series2.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            chartPress.Series.Add(series2);
            chartPress.Size = new Size(346, 339);
            chartPress.TabIndex = 1;
            chartPress.Text = "chart2";
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(685, 364);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(75, 23);
            buttonStart.TabIndex = 2;
            buttonStart.Text = "Старт";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += buttonStart_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonStart);
            Controls.Add(chartPress);
            Controls.Add(chartTemp);
            Name = "MainForm";
            Text = "Form1";
            FormClosing += MainForm_FormClosing;
            ((System.ComponentModel.ISupportInitialize)chartTemp).EndInit();
            ((System.ComponentModel.ISupportInitialize)chartPress).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartTemp;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPress;
        private Button buttonStart;
    }
}
