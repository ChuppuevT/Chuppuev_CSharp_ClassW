using AbstractFoodOrderBusinessLogic.BindingModels;
using AbstractFoodOrderBusinessLogic.BusinessLogics;
using System;
using Microsoft.Reporting.WinForms;
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
    public partial class FormReportOrders : Form
    {
       
        /* public new IUnityContainer Container { get; set; }
         private readonly ReportLogic logic;
         public FormReportOrders(ReportLogic logic)
         {
             InitializeComponent();
             this.logic = logic;
         }
         private void buttonMake_Click(object sender, EventArgs e)
         {
             if (dateTimePickerFrom.Value.Date >= dateTimePickerTo.Value.Date)
             {
                 MessageBox.Show("Дата начала должна быть меньше даты окончания",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return;
             }
             try
             {
                 ReportParameter parameter = new ReportParameter("ReportParameterPeriod",
                 "c " +
                dateTimePickerFrom.Value.ToShortDateString() +
                 " по " +
                dateTimePickerTo.Value.ToShortDateString());
                 reportViewer.LocalReport.SetParameters(parameter);
                 var dataSource = logic.GetOrders(new ReportBindingModel
                 {
                     DateFrom = dateTimePickerFrom.Value,
                     DateTo = dateTimePickerTo.Value
                 });
                 ReportDataSource source = new ReportDataSource("DataSetOrders",
                dataSource);
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
             if (dateTimePickerFrom.Value.Date >= dateTimePickerTo.Value.Date)
             {
                 MessageBox.Show("Дата начала должна быть меньше даты окончания",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 return;
             }
             using (var dialog = new SaveFileDialog { Filter = "pdf|*.pdf" })
             {
                 if (dialog.ShowDialog() == DialogResult.OK)
                 {
                     try
                     {
                         logic.SaveOrdersToPdfFile(new ReportBindingModel
                         {
                             FileName = dialog.FileName,
                             DateFrom = dateTimePickerFrom.Value,
                             DateTo = dateTimePickerTo.Value
                         });
                         MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK,
                         MessageBoxIcon.Information);
                     }
                     catch (Exception ex)
                     {
                         MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                     }
                 }
             }
         }

         private void FormReportOrders_Load(object sender, EventArgs e)
         {

         }*/

        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly ReportLogic logic;
        public FormReportOrders(ReportLogic logic)
        {
            InitializeComponent();
            this.logic = logic;
        }

        private void buttonMake_Click(object sender, EventArgs e)
        {
            if (dateTimePickerFrom.Value.Date >= dateTimePickerTo.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                var dict = logic.GetOrders(new ReportBindingModel { DateFrom = dateTimePickerFrom.Value.Date, DateTo = dateTimePickerTo.Value.Date });

                if (dict != null)
                {
                    dataGridView.Rows.Clear();

                    foreach (var date in dict)
                    {
                        decimal dateSum = 0;

                        dataGridView.Rows.Add(new object[] { date.Key, "", "" });

                        foreach (var order in date)
                        {
                            dataGridView.Rows.Add(new object[] { "", order.KitName, order.Sum });
                            dateSum += order.Sum;
                        }

                        dataGridView.Rows.Add(new object[] { "Итого", "", dateSum });
                        dataGridView.Rows.Add(new object[] { });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonToExcel_Click(object sender, EventArgs e)
        {
            if (dateTimePickerFrom.Value.Date >= dateTimePickerTo.Value.Date)
            {
                MessageBox.Show("Дата начала должна быть меньше даты окончания", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var dialog = new SaveFileDialog { Filter = "xlsx|*.xlsx" })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        logic.SaveOrdersToExcelFile(new ReportBindingModel { FileName = dialog.FileName, DateFrom = dateTimePickerFrom.Value.Date, DateTo = dateTimePickerTo.Value.Date });

                        MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
