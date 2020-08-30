namespace AbstractFoodOrderView
{
    partial class FormReportKitFoods
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.ReportKitFoodViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ButtonSaveToPDF = new System.Windows.Forms.Button();
            this.reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.ReportKitFoodViewModelBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ReportProductComponentViewModelBindingSource
            // 
            this.ReportKitFoodViewModelBindingSource.DataSource = typeof(AbstractFoodOrderBusinessLogic.ViewModels.ReportKitFoodViewModel);
            // 
            // ButtonSaveToPDF
            // 
            this.ButtonSaveToPDF.Location = new System.Drawing.Point(22, 12);
            this.ButtonSaveToPDF.Name = "ButtonSaveToPDF";
            this.ButtonSaveToPDF.Size = new System.Drawing.Size(146, 23);
            this.ButtonSaveToPDF.TabIndex = 0;
            this.ButtonSaveToPDF.Text = "Сохранить в Pdf";
            this.ButtonSaveToPDF.UseVisualStyleBackColor = true;
            this.ButtonSaveToPDF.Click += new System.EventHandler(this.ButtonSaveToPDF_Click);
            // 
            // reportViewer
            // 
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.ReportKitFoodViewModelBindingSource;
            this.reportViewer.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer.LocalReport.ReportEmbeddedResource = "AbstractFoodOrderView.ReportKitFood.rdlc";
            this.reportViewer.Location = new System.Drawing.Point(22, 58);
            this.reportViewer.Name = "reportViewer";
            this.reportViewer.ServerReport.BearerToken = null;
            this.reportViewer.Size = new System.Drawing.Size(659, 286);
            this.reportViewer.TabIndex = 1;
            this.reportViewer.Load += new System.EventHandler(this.reportViewer_Load);
            // 
            // FormReportKitFoods
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 450);
            this.Controls.Add(this.reportViewer);
            this.Controls.Add(this.ButtonSaveToPDF);
            this.Name = "FormReportKitFoods";
            this.Text = "Блюда по наборам";
            ((System.ComponentModel.ISupportInitialize)(this.ReportKitFoodViewModelBindingSource)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button ButtonSaveToPDF;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer;
        private System.Windows.Forms.BindingSource ReportKitFoodViewModelBindingSource;
    }
}