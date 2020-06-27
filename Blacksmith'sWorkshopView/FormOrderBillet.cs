using Blacksmith_sWorkshopBusinessLogic.BindingModels;
using Blacksmith_sWorkshopBusinessLogic.BusinessLogics;
using Blacksmith_sWorkshopBusinessLogic.Intefaces;
using Blacksmith_sWorkshopBusinessLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Blacksmith_sWorkshopView
{
    public partial class FormOrderBillet : Form
    {
        private readonly MainLogic mainLogic;
        private readonly IStorageLogic storageLogic;
        private readonly IBilletLogic BilletLogic;
        private List<StorageViewModel> storageViews;
        private List<BilletViewModel> BilletViews;
        public FormOrderBillet(MainLogic mainLogic, IStorageLogic storageLogic, IBilletLogic BilletLogic)
        {
            InitializeComponent();
            this.mainLogic = mainLogic;
            this.storageLogic = storageLogic;
            this.BilletLogic = BilletLogic;
            LoadData();
        }
        private void LoadData()
        {
            storageViews = storageLogic.Read(null);
            if (storageViews != null)
            {
                comboBoxStorage.DataSource = storageViews;
                comboBoxStorage.DisplayMember = "StorageName";
            }
            BilletViews = BilletLogic.Read(null);
            if (BilletViews != null)
            {
                comboBoxBillet.DataSource = BilletViews;
                comboBoxBillet.DisplayMember = "BilletName";
            }
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (comboBoxBillet.Text == string.Empty)
                throw new Exception("Введите количество заготовок");

            storageLogic.AddBilletToStorage(new StorageAddBilletBindingModel()
            {
                StorageId = (comboBoxStorage.SelectedItem as StorageViewModel).Id,
                BilletId = (comboBoxBillet.SelectedItem as BilletViewModel).Id,
                BilletCount = Convert.ToInt32(textBoxCount.Text)
            });
            DialogResult = DialogResult.OK;
            Close();
        }

        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void buttonOrd_Click(object sender, EventArgs e)
        {
            if (textBoxCount.Text == string.Empty)
                throw new Exception("Введите количество заготовок");

            storageLogic.AddBilletToStorage(new StorageAddBilletBindingModel()
            {
                StorageId = (comboBoxStorage.SelectedItem as StorageViewModel).Id,
                BilletId = (comboBoxBillet.SelectedItem as BilletViewModel).Id,
                BilletCount = Convert.ToInt32(textBoxCount.Text)
            });
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
