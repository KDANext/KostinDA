using Blacksmith_sWorkshopBusinessLogic.Intefaces;
using Blacksmith_sWorkshopBusinessLogic.ViewModels;
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

namespace Blacksmith_sWorkshopView
{
    public partial class FormProductBillet : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }

        public ProductBilletViewModel ModelView { get; set; }

        private readonly IBilletLogic logic;

        public FormProductBillet(IBilletLogic logic)
        {
            InitializeComponent(); this.logic = logic;
        }

        private void FormProductBillet_Load(object sender, EventArgs e)
        {
            try
            {
                List<BilletViewModel> list = logic.GetList();
                if (list != null)
                {
                    comboBoxBillet.DisplayMember = "BilletName";
                    comboBoxBillet.ValueMember = "Id";
                    comboBoxBillet.DataSource = list;
                    comboBoxBillet.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (ModelView != null)
            {
                comboBoxBillet.Enabled = false;
                comboBoxBillet.SelectedValue = ModelView.BilletId;
                textBoxCount.Text = ModelView.Count.ToString();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxBillet.SelectedValue == null) {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (ModelView == null)
                {
                    ModelView = new ProductBilletViewModel
                    {
                        BilletId = Convert.ToInt32(comboBoxBillet.SelectedValue),BilletName = comboBoxBillet.Text,Count = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else { ModelView.Count = Convert.ToInt32(textBoxCount.Text); }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information); DialogResult = DialogResult.OK; Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
