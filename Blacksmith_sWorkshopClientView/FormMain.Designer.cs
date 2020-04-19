namespace Blacksmith_sWorkshopClientView
{
    partial class FormMain
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.UpdateDataToolStripMenuItem = new
           System.Windows.Forms.ToolStripMenuItem();
            this.CreateOrderToolStripMenuItem = new
           System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.RefreshOrderListToolStripMenuItem = new
           System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            //
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
             this.UpdateDataToolStripMenuItem,
             this.CreateOrderToolStripMenuItem,
             this.RefreshOrderListToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(621, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            //
            // UpdateDataToolStripMenuItem
            //
            this.UpdateDataToolStripMenuItem.Name = "UpdateDataToolStripMenuItem";
            this.UpdateDataToolStripMenuItem.Size = new System.Drawing.Size(117, 20);
            this.UpdateDataToolStripMenuItem.Text = "Изменить данные";
            this.UpdateDataToolStripMenuItem.Click += new
           System.EventHandler(this.UpdateDataToolStripMenuItem_Click);
            //
            // CreateOrderToolStripMenuItem
            //
            this.CreateOrderToolStripMenuItem.Name = "CreateOrderToolStripMenuItem";
            this.CreateOrderToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.CreateOrderToolStripMenuItem.Text = "Создать заказ";
            this.CreateOrderToolStripMenuItem.Click += new
           System.EventHandler(this.CreateOrderToolStripMenuItem_Click);
            //
            // RefreshOrderListToolStripMenuItem
            //
            this.RefreshOrderListToolStripMenuItem.Name =
           "RefreshOrderListToolStripMenuItem";
            this.RefreshOrderListToolStripMenuItem.Size = new System.Drawing.Size(159,
           20);
            this.RefreshOrderListToolStripMenuItem.Text = "Обновить список заказов";
            this.RefreshOrderListToolStripMenuItem.Click += new
           System.EventHandler(this.RefreshOrderListToolStripMenuItem_Click);
            //
            // dataGridView
            //
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode =
           System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(0, 24);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(621, 323);
            this.dataGridView.TabIndex = 1;
            //
            // FormMain
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.components = new System.ComponentModel.Container();
            this.ClientSize = new System.Drawing.Size(900, 350);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Text = "Форма заказов";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem UpdateDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CreateOrderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RefreshOrderListToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView;
    }
}