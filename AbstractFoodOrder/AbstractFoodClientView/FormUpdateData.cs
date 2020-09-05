using AbstractFoodOrderBusinessLogic.BindingModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AbstractFoodClientView
{
    public partial class FormUpdateData : Form
    {
        public FormUpdateData()
        {
            InitializeComponent();
            textBoxClientFIO.Text = Program.Client.FIO;
            textBoxEmail.Text = Program.Client.Email;
            textBoxPassword.Text = Program.Client.Password;
        }

        private void ButtonUpdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxEmail.Text) &&
           !string.IsNullOrEmpty(textBoxPassword.Text) &&
           !string.IsNullOrEmpty(textBoxClientFIO.Text))
            {
                try
                {
                    APIClient.PostRequest("api/client/updatedata", new ClientBindingModel
                    {
                        Id = Program.Client.Id,
                        FIO = textBoxClientFIO.Text,
                        Email = textBoxEmail.Text,
                        Password = textBoxPassword.Text
                    });
                    MessageBox.Show("Обновление прошло успешно", "Сообщение",
                   MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Program.Client.FIO = textBoxClientFIO.Text;
                    Program.Client.Email = textBoxEmail.Text;
                    Program.Client.Password = textBoxPassword.Text;
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Введите логин, пароль и ФИО", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
