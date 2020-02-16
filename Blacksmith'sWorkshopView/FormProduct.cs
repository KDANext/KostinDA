using Blacksmith_sWorkshopBusinessLogic.BindingModels;
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
    public partial class FormProduct : Form
    {
        [Dependency] public new IUnityContainer Container { get; set; }

        public int Id { set { id = value; } }

        private readonly IProductLogic logic;

        private int? id;

        private List<ProductBilletViewModel> productBillets;

        public FormProduct(IProductLogic service) { 
            InitializeComponent(); 
            this.logic = service; 
        }

        private void FormProduct_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    ProductViewModel view = logic.GetElement(id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.ProductName;
                        textBoxPrice.Text = view.Price.ToString();
                        productBillets = view.ProductBillets;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else { productBillets = new List<ProductBilletViewModel>(); }
        }

        private void LoadData()
        {
            try
            {
                if (productBillets != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = productBillets;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = Container.Resolve<FormProductBillet>();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.ModelView != null)
                {
                    if (id.HasValue)
                    {
                        form.ModelView.ProductId = id.Value;
                    }
                    productBillets.Add(form.ModelView);
                } LoadData();
            }
        }

        private void buttonUpd_Click(object sender, EventArgs e)
        {

        }

        private void buttonDel_Click(object sender, EventArgs e)
        {

        }

        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (productBillets == null || productBillets.Count == 0)
            {
                MessageBox.Show("Заполните компоненты", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<ProductBilletBindingModel> productBilletBM = new List<ProductBilletBindingModel>();
                for (int i = 0; i < productBillets.Count; ++i)
                {
                    productBilletBM.Add(new ProductBilletBindingModel
                    {
                        Id = productBillets[i].Id,
                        ProductId = productBillets[i].ProductId,
                        BilletId = productBillets[i].BilletId,
                        Count = productBillets[i].Count
                    });
                }
                if (id.HasValue)
                {
                    logic.UpdElement(new ProductBindingModel
                    {
                        Id = id.Value,
                        ProductName = textBoxName.Text,
                        Price = Convert.ToDecimal(textBoxPrice.Text),
                        ProductBillets = productBilletBM
                    });
                } else {
                    logic.AddElement(new ProductBindingModel
                    {
                        ProductName = textBoxName.Text,
                        Price = Convert.ToDecimal(textBoxPrice.Text),
                        ProductBillets = productBilletBM
                    });
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK; Close();
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
