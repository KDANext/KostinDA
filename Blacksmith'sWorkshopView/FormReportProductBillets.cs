using Blacksmith_sWorkshopBusinessLogic.BindingModels;
using Blacksmith_sWorkshopBusinessLogic.BusinessLogics;
using Microsoft.Reporting.WinForms;
using System;
using System.Windows.Forms;
using Unity;

namespace BlacksmithsWorkshopView
{
    public partial class FormReportProductBillets : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ReportLogic logic;
        public FormReportProductBillets(ReportLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }
        private void ButtonMake_Click(object sender, EventArgs e)
        {
            try
            {
                var dataSource = logic.GetProductBillet();
                ReportDataSource source = new ReportDataSource("DataSetProductBillet", dataSource);
                reportViewer.LocalReport.DataSources.Add(source);
                reportViewer.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }

        private void buttonToPdf_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logic.SaveProductBilletToPdfFile(new ReportBindingModel
                        {
                            FileName = dialog.FileName,
                        });
                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void FormReportProductBillets_Load(object sender, EventArgs e)
        {

            this.reportViewer.RefreshReport();
        }
    }
}
