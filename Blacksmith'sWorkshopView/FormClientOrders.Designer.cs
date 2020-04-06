namespace BlacksmithsWorkshopView
{
    partial class FormReportOrders
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
            this.dateTimePickerFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerTo = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonMake = new System.Windows.Forms.Button();
            this.buttonToExel = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.ColumnDateCreate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Columnproduct = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dateTimePickerFrom
            // 
            this.dateTimePickerFrom.Location = new System.Drawing.Point(32, 12);
            this.dateTimePickerFrom.Name = "dateTimePickerFrom";
            this.dateTimePickerFrom.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerFrom.TabIndex = 0;
            // 
            // dateTimePickerTo
            // 
            this.dateTimePickerTo.Location = new System.Drawing.Point(263, 12);
            this.dateTimePickerTo.Name = "dateTimePickerTo";
            this.dateTimePickerTo.Size = new System.Drawing.Size(200, 20);
            this.dateTimePickerTo.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "C";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(238, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "по";
            // 
            // buttonMake
            // 
            this.buttonMake.Location = new System.Drawing.Point(469, 9);
            this.buttonMake.Name = "buttonMake";
            this.buttonMake.Size = new System.Drawing.Size(133, 23);
            this.buttonMake.TabIndex = 4;
            this.buttonMake.Text = "Сформировать";
            this.buttonMake.UseVisualStyleBackColor = true;
            this.buttonMake.Click += new System.EventHandler(this.ButtonMake_Click);
            // 
            // buttonToExel
            // 
            this.buttonToExel.Location = new System.Drawing.Point(469, 38);
            this.buttonToExel.Name = "buttonToExel";
            this.buttonToExel.Size = new System.Drawing.Size(133, 28);
            this.buttonToExel.TabIndex = 5;
            this.buttonToExel.Text = "В exel";
            this.buttonToExel.UseVisualStyleBackColor = true;
            this.buttonToExel.Click += new System.EventHandler(this.ButtonSaveToExel_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.HighlightText;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnDateCreate,
            this.Columnproduct,
            this.ColumnSum});
            this.dataGridView.Location = new System.Drawing.Point(12, 38);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(451, 400);
            this.dataGridView.TabIndex = 6;
            // 
            // ColumnDateCreate
            // 
            this.ColumnDateCreate.HeaderText = "Дата создания";
            this.ColumnDateCreate.Name = "ColumnDateCreate";
            this.ColumnDateCreate.Width = 120;
            // 
            // Columnproduct
            // 
            this.Columnproduct.HeaderText = "Продукт";
            this.Columnproduct.Name = "Columnproduct";
            // 
            // ColumnSum
            // 
            this.ColumnSum.HeaderText = "Стоимость";
            this.ColumnSum.Name = "ColumnSum";
            // 
            // FormReportOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(605, 450);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.buttonToExel);
            this.Controls.Add(this.buttonMake);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dateTimePickerTo);
            this.Controls.Add(this.dateTimePickerFrom);
            this.Name = "FormReportOrders";
            this.Text = "Заказы по датам";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePickerFrom;
        private System.Windows.Forms.DateTimePicker dateTimePickerTo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonMake;
        private System.Windows.Forms.Button buttonToExel;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDateCreate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Columnproduct;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSum;
    }
}