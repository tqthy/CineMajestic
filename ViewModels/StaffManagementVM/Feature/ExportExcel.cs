using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Data;

namespace CineMajestic.ViewModels.StaffManagementVM
{
    public partial class StaffManageVM
    {

        //phục vụ thông báo export thành công 
        private string notify;
        public string Notify
        {
            get { return notify; }
            set
            {
                notify = value;
                OnPropertyChanged(nameof(Notify));
            }
        }

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
                fileExcelName = "Staff_" + MotSoPTBoTro.RandomFileName() + ".xlsx";
                pathFile = Path + @"\" + fileExcelName;
                if (!File.Exists(pathFile))
                {
                    break;
                }
            }



            //chuẩn bị  export

            //tạo datatable tương ứng
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(string));
            table.Columns.Add("Họ và tên", typeof(string));
            table.Columns.Add("Ngày sinh", typeof(string));
            table.Columns.Add("Giới tính", typeof(string));
            table.Columns.Add("SĐT", typeof(string));
            table.Columns.Add("Email", typeof(string));
            table.Columns.Add("Chức vụ", typeof(string));
            table.Columns.Add("Lương", typeof(int));


            //chuyển list vào table
            foreach (var item in FilterDSNV)
            {
                table.Rows.Add(item.IdFormat, item.FullName, item.Birth, item.Gender, item.PhoneNumber, item.Email, item.Role, item.Salary);
            }


            //tiến hành export
            Task.Run(async () =>
            {
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
                    Notify = "Export DSNV ra file Excel thành công!";
                }
                catch
                {
                    Notify = "Có lỗi xảy ra trong quá trình Export!";
                }
                await Task.Delay(2000);
                Notify = "";
            });
        }
    }
}
