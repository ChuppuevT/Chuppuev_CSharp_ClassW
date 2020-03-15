using AbstractFoodOrderBusinessLogic.Interfaces;
using AbstractFoodOrderBusinessLogic.ViewModels;
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

namespace AbstractFoodOrderView
{
    public partial class FormKitFood : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        public int Id
        {
            get { return Convert.ToInt32(comboBoxFood.SelectedValue); }
            set { comboBoxFood.SelectedValue = value; }
        }
        public string ComponentName { get { return comboBoxFood.Text; } }
        public int Count
        {
            get { return Convert.ToInt32(textBoxCount.Text); }
            set
            {
                textBoxCount.Text = value.ToString();
            }
        }
        public FormKitFood(IFoodLogic logic)
        {
            InitializeComponent();
            List<FoodViewModel> list = logic.Read(null);
            if (list != null)
            {
                comboBoxFood.DisplayMember = "FoodName";
                comboBoxFood.ValueMember = "Id";
                comboBoxFood.DataSource = list;
                comboBoxFood.SelectedItem = null;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxFood.SelectedValue == null)
            {
                MessageBox.Show("Выберите блюдо", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
