namespace Blacksmith_sWorkshopView
{
    partial class FormOrderBillet
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxBillet = new System.Windows.Forms.ComboBox();
            this.comboBoxStorage = new System.Windows.Forms.ComboBox();
            this.textBoxCount = new System.Windows.Forms.TextBox();
            this.buttonOrd = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Заготовка";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Куда";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Сколько";
            // 
            // comboBoxBillet
            // 
            this.comboBoxBillet.FormattingEnabled = true;
            this.comboBoxBillet.Location = new System.Drawing.Point(106, 6);
            this.comboBoxBillet.Name = "comboBoxBillet";
            this.comboBoxBillet.Size = new System.Drawing.Size(121, 21);
            this.comboBoxBillet.TabIndex = 3;
            // 
            // comboBoxStorage
            // 
            this.comboBoxStorage.FormattingEnabled = true;
            this.comboBoxStorage.Location = new System.Drawing.Point(106, 33);
            this.comboBoxStorage.Name = "comboBoxStorage";
            this.comboBoxStorage.Size = new System.Drawing.Size(121, 21);
            this.comboBoxStorage.TabIndex = 4;
            // 
            // textBoxCount
            // 
            this.textBoxCount.Location = new System.Drawing.Point(106, 60);
            this.textBoxCount.Name = "textBoxCount";
            this.textBoxCount.Size = new System.Drawing.Size(121, 20);
            this.textBoxCount.TabIndex = 5;
            // 
            // buttonOrd
            // 
            this.buttonOrd.Location = new System.Drawing.Point(12, 86);
            this.buttonOrd.Name = "buttonOrd";
            this.buttonOrd.Size = new System.Drawing.Size(75, 23);
            this.buttonOrd.TabIndex = 6;
            this.buttonOrd.Text = "Заказать";
            this.buttonOrd.UseVisualStyleBackColor = true;
            this.buttonOrd.Click += new System.EventHandler(this.buttonOrd_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(152, 86);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 7;
            this.buttonCancel.Text = "Отмена";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // FormOrderBillet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(239, 109);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOrd);
            this.Controls.Add(this.textBoxCount);
            this.Controls.Add(this.comboBoxStorage);
            this.Controls.Add(this.comboBoxBillet);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormOrderBillet";
            this.Text = "Заказ на заготовки";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxBillet;
        private System.Windows.Forms.ComboBox comboBoxStorage;
        private System.Windows.Forms.TextBox textBoxCount;
        private System.Windows.Forms.Button buttonOrd;
        private System.Windows.Forms.Button buttonCancel;
    }
}