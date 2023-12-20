using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineMajestic.ViewModels.StaffManagementVM
{
    public partial class StaffManageVM
    {
        public ICommand ExportExcelCommand {  get; set; }
        void exportExcel()
        {
            ExportExcelCommand=new ViewModelCommand(ExportExcel);
        }
        private void ExportExcel(object obj)
        {
            string fileExcelName = "Custom.xlsx";

            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                    worksheet.Cells.LoadFromCollection(DSNV, true);

                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    File.WriteAllBytes(fileExcelName, package.GetAsByteArray());
                }
                MessageBox.Show("Thành công");
            }
            catch
            {
                MessageBox.Show("Có lỗi xảy ra, vui lòng thử lại!");
            }
        }
    }
}
