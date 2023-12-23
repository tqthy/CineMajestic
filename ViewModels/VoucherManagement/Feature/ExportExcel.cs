using Microsoft.WindowsAPICodePack.Dialogs;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineMajestic.ViewModels.VoucherManagement
{
    public partial class VoucherManagementViewModel
    {
        public ICommand ExportExcelCommand { get; set; }
        void exportExcel()
        {
            ExportExcelCommand = new ViewModelCommand(ExportExcel);
        }
        private void ExportExcel(object obj)
        {

            //chọn folder lưu file export
            string Path = "";
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Path = dialog.FileName;
            }


            string fileExcelName = "";//tên file export
            string pathFile = "";//đường dẫn tuyệt đối của file export

            while (true)
            {
                fileExcelName = "Voucher_" + MotSoPTBoTro.RandomFileName() + ".xlsx";
                pathFile = Path + @"\" + fileExcelName;
                if (!File.Exists(pathFile))
                {
                    break;
                }
            }
            //chuẩn bị  export

            //tạo datatable tương ứng
            DataTable table = new DataTable();
            table.Columns.Add("STT", typeof(int));
            table.Columns.Add("Tên voucher", typeof(string));
            table.Columns.Add("Mã voucher", typeof(string));
            table.Columns.Add("Thông tin voucher", typeof(string));
            table.Columns.Add("Loại", typeof(string));
            table.Columns.Add("Ngày bắt đầu", typeof(string));
            table.Columns.Add("Ngày kết thúc", typeof(string));
            //chuyển list vào table
            foreach (var item in FilterDSVC)
            {
                // table.Rows.Add(item.IdFormat, item.FullName, item.Birth, item.Gender, item.PhoneNumber, item.Email, item.Role, item.Salary);
                table.Rows.Add(item.Id, item.Name, item.Code, item.VoucherDetail, item.Type, item.StartDate, item.FinDate);
            }


            //tiến hành export
            try
            {
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage())
                {
                    var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                    worksheet.Cells.LoadFromDataTable(table, true);

                    worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                    File.WriteAllBytes(pathFile, package.GetAsByteArray());
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