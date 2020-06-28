using Blacksmith_sWorkshopBusinessLogic.BindingModels;
using Blacksmith_sWorkshopBusinessLogic.BusinessLogics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;

namespace BlacksmithsWorkshopView
{
    public partial class FormStorageLoad : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ReportLogic reportLogic;
        public FormStorageLoad(ReportLogic logic)
        {
            InitializeComponent();
            this.reportLogic = logic;
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            var storages = reportLogic.GetStorageWithBillets();
            if (storages != null)
            {
                dataGridView.Rows.Clear();
                foreach (var storage in storages)
                {
                    dataGridView.Rows.Add(new object[] { storage.StorageName, "", "" });
                    foreach (var component in storage.Billets)
                    {
                        dataGridView.Rows.Add(new object[] { "", component.Item1, component.Item2 });
                    }
                    dataGridView.Rows.Add(new object[] { "", "Итого", storage.TotalCount });
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        reportLogic.SaveStoragesToExcelFile(new ReportBindingModel
                        {
                            FileName = dialog.FileName
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
    }
}
