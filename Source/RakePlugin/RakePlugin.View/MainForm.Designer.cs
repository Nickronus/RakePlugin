namespace RakePlugin.View
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.OptionsPanel = new System.Windows.Forms.Panel();
            this.ParameterLimitations = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ParameterValues = new System.Windows.Forms.Panel();
            this.WorkingSurfaceWidthComboBox = new System.Windows.Forms.ComboBox();
            this.NumberOfTeethComboBox = new System.Windows.Forms.ComboBox();
            this.HandleLengthTextBox = new System.Windows.Forms.TextBox();
            this.HandleDiameterTextBox = new System.Windows.Forms.TextBox();
            this.LengthOfTeethTextBox = new System.Windows.Forms.TextBox();
            this.NameOfParameters = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BuildFigure = new System.Windows.Forms.Button();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.ErrorsToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.LightweightWorkSurfaceComboBox = new System.Windows.Forms.ComboBox();
            this.ToothShapeComboBox = new System.Windows.Forms.ComboBox();
            this.WorkingSurfaceLengthComboBox = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.OptionsPanel.SuspendLayout();
            this.ParameterLimitations.SuspendLayout();
            this.ParameterValues.SuspendLayout();
            this.NameOfParameters.SuspendLayout();
            this.MainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // OptionsPanel
            // 
            this.OptionsPanel.Controls.Add(this.ParameterLimitations);
            this.OptionsPanel.Controls.Add(this.ParameterValues);
            this.OptionsPanel.Controls.Add(this.NameOfParameters);
            this.OptionsPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.OptionsPanel.Location = new System.Drawing.Point(0, 0);
            this.OptionsPanel.Name = "OptionsPanel";
            this.OptionsPanel.Size = new System.Drawing.Size(732, 258);
            this.OptionsPanel.TabIndex = 3;
            // 
            // ParameterLimitations
            // 
            this.ParameterLimitations.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ParameterLimitations.Controls.Add(this.label14);
            this.ParameterLimitations.Controls.Add(this.label16);
            this.ParameterLimitations.Controls.Add(this.label15);
            this.ParameterLimitations.Controls.Add(this.label10);
            this.ParameterLimitations.Controls.Add(this.label8);
            this.ParameterLimitations.Controls.Add(this.label6);
            this.ParameterLimitations.Controls.Add(this.label4);
            this.ParameterLimitations.Controls.Add(this.label2);
            this.ParameterLimitations.Location = new System.Drawing.Point(502, 12);
            this.ParameterLimitations.Name = "ParameterLimitations";
            this.ParameterLimitations.Size = new System.Drawing.Size(218, 243);
            this.ParameterLimitations.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.label10.Location = new System.Drawing.Point(3, 105);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(128, 22);
            this.label10.TabIndex = 6;
            this.label10.Text = "1000-2000 мм";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.label8.Location = new System.Drawing.Point(3, 79);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 22);
            this.label8.TabIndex = 5;
            this.label8.Text = "20-30 мм";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.label6.Location = new System.Drawing.Point(3, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 22);
            this.label6.TabIndex = 4;
            this.label6.Text = "50-200 мм";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.label4.Location = new System.Drawing.Point(3, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(209, 22);
            this.label4.TabIndex = 3;
            this.label4.Text = "Выберите вариант (шт)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(212, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "Выберите вариант (мм)";
            // 
            // ParameterValues
            // 
            this.ParameterValues.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.ParameterValues.Controls.Add(this.WorkingSurfaceLengthComboBox);
            this.ParameterValues.Controls.Add(this.ToothShapeComboBox);
            this.ParameterValues.Controls.Add(this.LightweightWorkSurfaceComboBox);
            this.ParameterValues.Controls.Add(this.WorkingSurfaceWidthComboBox);
            this.ParameterValues.Controls.Add(this.NumberOfTeethComboBox);
            this.ParameterValues.Controls.Add(this.HandleLengthTextBox);
            this.ParameterValues.Controls.Add(this.HandleDiameterTextBox);
            this.ParameterValues.Controls.Add(this.LengthOfTeethTextBox);
            this.ParameterValues.Location = new System.Drawing.Point(347, 12);
            this.ParameterValues.Name = "ParameterValues";
            this.ParameterValues.Size = new System.Drawing.Size(149, 243);
            this.ParameterValues.TabIndex = 1;
            // 
            // WorkingSurfaceWidthComboBox
            // 
            this.WorkingSurfaceWidthComboBox.FormattingEnabled = true;
            this.WorkingSurfaceWidthComboBox.Items.AddRange(new object[] {
            "90",
            "330",
            "1290"});
            this.WorkingSurfaceWidthComboBox.Location = new System.Drawing.Point(4, 5);
            this.WorkingSurfaceWidthComboBox.Name = "WorkingSurfaceWidthComboBox";
            this.WorkingSurfaceWidthComboBox.Size = new System.Drawing.Size(142, 24);
            this.WorkingSurfaceWidthComboBox.TabIndex = 8;
            this.WorkingSurfaceWidthComboBox.Text = "330";
            this.WorkingSurfaceWidthComboBox.TextChanged += new System.EventHandler(this.WorkingSurfaceWidthComboBox_TextChanged);
            // 
            // NumberOfTeethComboBox
            // 
            this.NumberOfTeethComboBox.FormattingEnabled = true;
            this.NumberOfTeethComboBox.Items.AddRange(new object[] {
            "3",
            "5",
            "9",
            "17",
            "33"});
            this.NumberOfTeethComboBox.Location = new System.Drawing.Point(3, 29);
            this.NumberOfTeethComboBox.Name = "NumberOfTeethComboBox";
            this.NumberOfTeethComboBox.Size = new System.Drawing.Size(143, 24);
            this.NumberOfTeethComboBox.TabIndex = 7;
            this.NumberOfTeethComboBox.Text = "5";
            this.NumberOfTeethComboBox.TextChanged += new System.EventHandler(this.NumberOfTeethComboBox_TextChanged);
            // 
            // HandleLengthTextBox
            // 
            this.HandleLengthTextBox.BackColor = System.Drawing.Color.White;
            this.HandleLengthTextBox.Location = new System.Drawing.Point(3, 106);
            this.HandleLengthTextBox.Name = "HandleLengthTextBox";
            this.HandleLengthTextBox.Size = new System.Drawing.Size(143, 22);
            this.HandleLengthTextBox.TabIndex = 6;
            this.HandleLengthTextBox.Text = "1000";
            this.HandleLengthTextBox.TextChanged += new System.EventHandler(this.HandleLengthTextBox_TextChanged);
            this.HandleLengthTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HandleLengthTextBox_KeyPress);
            // 
            // HandleDiameterTextBox
            // 
            this.HandleDiameterTextBox.BackColor = System.Drawing.Color.White;
            this.HandleDiameterTextBox.Location = new System.Drawing.Point(3, 80);
            this.HandleDiameterTextBox.Name = "HandleDiameterTextBox";
            this.HandleDiameterTextBox.Size = new System.Drawing.Size(143, 22);
            this.HandleDiameterTextBox.TabIndex = 5;
            this.HandleDiameterTextBox.Text = "30";
            this.HandleDiameterTextBox.TextChanged += new System.EventHandler(this.HandleDiametertTextBox_TextChanged);
            this.HandleDiameterTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HandleDiameterTextBox_KeyPress);
            // 
            // LengthOfTeethTextBox
            // 
            this.LengthOfTeethTextBox.BackColor = System.Drawing.Color.White;
            this.LengthOfTeethTextBox.Location = new System.Drawing.Point(3, 54);
            this.LengthOfTeethTextBox.Name = "LengthOfTeethTextBox";
            this.LengthOfTeethTextBox.Size = new System.Drawing.Size(143, 22);
            this.LengthOfTeethTextBox.TabIndex = 4;
            this.LengthOfTeethTextBox.Text = "100";
            this.LengthOfTeethTextBox.TextChanged += new System.EventHandler(this.LengthOfTeethTextBox_TextChanged);
            this.LengthOfTeethTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LengthOfTeethTextBox_KeyPress);
            // 
            // NameOfParameters
            // 
            this.NameOfParameters.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.NameOfParameters.Controls.Add(this.label12);
            this.NameOfParameters.Controls.Add(this.label13);
            this.NameOfParameters.Controls.Add(this.label9);
            this.NameOfParameters.Controls.Add(this.label7);
            this.NameOfParameters.Controls.Add(this.label11);
            this.NameOfParameters.Controls.Add(this.label5);
            this.NameOfParameters.Controls.Add(this.label3);
            this.NameOfParameters.Controls.Add(this.label1);
            this.NameOfParameters.Location = new System.Drawing.Point(12, 12);
            this.NameOfParameters.Name = "NameOfParameters";
            this.NameOfParameters.Size = new System.Drawing.Size(329, 243);
            this.NameOfParameters.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(3, 105);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(125, 22);
            this.label9.TabIndex = 5;
            this.label9.Text = "Длина ручки ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(3, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(145, 22);
            this.label7.TabIndex = 4;
            this.label7.Text = "Диаметр ручки ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(3, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 22);
            this.label5.TabIndex = 3;
            this.label5.Text = "Длина зубцов ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(3, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(176, 22);
            this.label3.TabIndex = 2;
            this.label3.Text = "Количество зубцов ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(273, 22);
            this.label1.TabIndex = 1;
            this.label1.Text = "Ширина рабочей поверхности ";
            // 
            // BuildFigure
            // 
            this.BuildFigure.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.BuildFigure.BackColor = System.Drawing.Color.LightCyan;
            this.BuildFigure.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BuildFigure.Location = new System.Drawing.Point(502, 264);
            this.BuildFigure.Name = "BuildFigure";
            this.BuildFigure.Size = new System.Drawing.Size(218, 77);
            this.BuildFigure.TabIndex = 0;
            this.BuildFigure.Text = "Построить";
            this.BuildFigure.UseVisualStyleBackColor = false;
            this.BuildFigure.Click += new System.EventHandler(this.BuildFigureClick);
            // 
            // MainPanel
            // 
            this.MainPanel.BackColor = System.Drawing.Color.White;
            this.MainPanel.Controls.Add(this.BuildFigure);
            this.MainPanel.Controls.Add(this.OptionsPanel);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(732, 353);
            this.MainPanel.TabIndex = 1;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(3, 155);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(126, 22);
            this.label11.TabIndex = 6;
            this.label11.Text = "Форма зубца:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(3, 182);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(257, 22);
            this.label12.TabIndex = 7;
            this.label12.Text = "Длина рабочей поверхности:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label13.Location = new System.Drawing.Point(3, 211);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(308, 22);
            this.label13.TabIndex = 8;
            this.label13.Text = "Облегченная рабочая поверхность:";
            // 
            // LightweightWorkSurfaceComboBox
            // 
            this.LightweightWorkSurfaceComboBox.FormattingEnabled = true;
            this.LightweightWorkSurfaceComboBox.Items.AddRange(new object[] {
            "Нет",
            "Да"});
            this.LightweightWorkSurfaceComboBox.Location = new System.Drawing.Point(3, 212);
            this.LightweightWorkSurfaceComboBox.Name = "LightweightWorkSurfaceComboBox";
            this.LightweightWorkSurfaceComboBox.Size = new System.Drawing.Size(143, 24);
            this.LightweightWorkSurfaceComboBox.TabIndex = 9;
            this.LightweightWorkSurfaceComboBox.Text = "Нет";
            this.LightweightWorkSurfaceComboBox.TextChanged += new System.EventHandler(this.LightweightWorkSurfaceComboBox_TextChanged);
            // 
            // ToothShapeComboBox
            // 
            this.ToothShapeComboBox.FormattingEnabled = true;
            this.ToothShapeComboBox.Items.AddRange(new object[] {
            "Квадрат",
            "Круг"});
            this.ToothShapeComboBox.Location = new System.Drawing.Point(3, 156);
            this.ToothShapeComboBox.Name = "ToothShapeComboBox";
            this.ToothShapeComboBox.Size = new System.Drawing.Size(143, 24);
            this.ToothShapeComboBox.TabIndex = 10;
            this.ToothShapeComboBox.Text = "Квадрат";
            this.ToothShapeComboBox.TextChanged += new System.EventHandler(this.ToothShapeComboBox_TextChanged);
            // 
            // WorkingSurfaceLengthComboBox
            // 
            this.WorkingSurfaceLengthComboBox.FormattingEnabled = true;
            this.WorkingSurfaceLengthComboBox.Items.AddRange(new object[] {
            "100",
            "50"});
            this.WorkingSurfaceLengthComboBox.Location = new System.Drawing.Point(3, 183);
            this.WorkingSurfaceLengthComboBox.Name = "WorkingSurfaceLengthComboBox";
            this.WorkingSurfaceLengthComboBox.Size = new System.Drawing.Size(143, 24);
            this.WorkingSurfaceLengthComboBox.TabIndex = 11;
            this.WorkingSurfaceLengthComboBox.Text = "100";
            this.WorkingSurfaceLengthComboBox.TextChanged += new System.EventHandler(this.WorkingSurfaceLengthComboBox_TextChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.label15.Location = new System.Drawing.Point(3, 155);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(167, 22);
            this.label15.TabIndex = 8;
            this.label15.Text = "Выберите вариант";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.label16.Location = new System.Drawing.Point(3, 211);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(167, 22);
            this.label16.TabIndex = 9;
            this.label16.Text = "Выберите вариант";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.label14.Location = new System.Drawing.Point(3, 183);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(167, 22);
            this.label14.TabIndex = 10;
            this.label14.Text = "Выберите вариант";
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(732, 353);
            this.Controls.Add(this.MainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(750, 400);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(750, 400);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rake Plagin";
            this.OptionsPanel.ResumeLayout(false);
            this.ParameterLimitations.ResumeLayout(false);
            this.ParameterLimitations.PerformLayout();
            this.ParameterValues.ResumeLayout(false);
            this.ParameterValues.PerformLayout();
            this.NameOfParameters.ResumeLayout(false);
            this.NameOfParameters.PerformLayout();
            this.MainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel OptionsPanel;
        private System.Windows.Forms.Panel ParameterValues;
        private System.Windows.Forms.TextBox HandleLengthTextBox;
        private System.Windows.Forms.TextBox HandleDiameterTextBox;
        private System.Windows.Forms.TextBox LengthOfTeethTextBox;
        private System.Windows.Forms.Panel NameOfParameters;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BuildFigure;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.ToolTip ErrorsToolTip;
        private System.Windows.Forms.Panel ParameterLimitations;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox WorkingSurfaceWidthComboBox;
        private System.Windows.Forms.ComboBox NumberOfTeethComboBox;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox WorkingSurfaceLengthComboBox;
        private System.Windows.Forms.ComboBox ToothShapeComboBox;
        private System.Windows.Forms.ComboBox LightweightWorkSurfaceComboBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
    }
}
