using Blacksmith_sWorkshopBusinessLogic.Intefaces;
using Blacksmith_sWorkshopView;
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
    public partial class FormMails : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly IMessageInfoLogic messageLogic;

        public FormMails(IMessageInfoLogic messageLogic)
        {
            InitializeComponent();
            this.messageLogic = messageLogic;
        }

        private void FormMails_Load(object sender, EventArgs e)
        {
            try
            {
                Program.ConfigGrid(messageLogic.Read(null), dataGridView);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
    }
}
