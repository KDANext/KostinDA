namespace BlacksmithsWorkshopView
{
    partial class FormReportProductBillets
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.buttonMake = new System.Windows.Forms.Button();
            this.buttonToPdf = new System.Windows.Forms.Button();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ReportProductBilletViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ReportProductBilletViewModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonMake
            // 
            this.buttonMake.Location = new System.Drawing.Point(12, 12);
            this.buttonMake.Name = "buttonMake";
            this.buttonMake.Size = new System.Drawing.Size(149, 23);
            this.buttonMake.TabIndex = 1;
            this.buttonMake.Text = "Сформировать";
            this.buttonMake.UseVisualStyleBackColor = true;
            this.buttonMake.Click += new System.EventHandler(this.ButtonMake_Click);
            // 
            // buttonToPdf
            // 
            this.buttonToPdf.Location = new System.Drawing.Point(167, 12);
            this.buttonToPdf.Name = "buttonToPdf";
            this.buttonToPdf.Size = new System.Drawing.Size(149, 23);
            this.buttonToPdf.TabIndex = 2;
            this.buttonToPdf.Text = "В PDF";
            this.buttonToPdf.UseVisualStyleBackColor = true;
            this.buttonToPdf.Click += new System.EventHandler(this.buttonToPdf_Click);
            // 
            // reportViewer
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.ReportProductBilletViewModelBindingSource;
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer.LocalReport.ReportEmbeddedResource = "BlacksmithsWorkshopView.Report.rdlc";
            this.reportViewer.Location = new System.Drawing.Point(12, 41);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.ServerReport.BearerToken = null;
            this.reportViewer.Size = new System.Drawing.Size(1053, 490);
            this.reportViewer.TabIndex = 3;
            // 
            // ReportProductBilletViewModelBindingSource
            // 
            this.ReportProductBilletViewModelBindingSource.DataSource = typeof(Blacksmith_sWorkshopBusinessLogic.ViewModels.ReportProductBilletViewModel);
            // 
            // FormReportProductBillets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1069, 535);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.buttonToPdf);
            this.Controls.Add(this.buttonMake);
            this.Name = "FormReportProductBillets";
            this.Text = "Рецепты";
            this.Load += new System.EventHandler(this.FormReportProductBillets_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ReportProductBilletViewModelBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonMake;
        private System.Windows.Forms.Button buttonToPdf;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.BindingSource ReportProductBilletViewModelBindingSource;
    }
}