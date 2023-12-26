using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs;
using CineMajestic.Views.MessageBox;
using CineMajestic.Views.VoucherManagement;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineMajestic.ViewModels.VoucherManagement
{
    public partial class VoucherManagementViewModel
    {
        public ICommand showWdEditVoucherCommand { get; set; }

        void editVoucher()
        {
            showWdEditVoucherCommand = new ViewModelCommand(showWdEditVoucher);
        }

        private void showWdEditVoucher(object obj)
        {
            EditVoucher  editVoucher=new EditVoucher((VoucherDTO)obj);
            editVoucher.ShowDialog();

            loadData();
        }
    }


    public class EditVoucherViewModel :MainBaseViewModel
    {
        private EditVoucher wd;
        public VoucherDTO voucher { get; set; }//voucher current
        public ICommand quitCommand { get; set; }
        public ICommand acceptCommand {  get; set; }
        //Tên voucher
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                ValidateName();
            }
        }

        private string nameError;
        public string NameError
        {
            get => nameError;
            set
            {
                nameError = value;
                OnPropertyChanged(nameof(NameError));
            }
        }
        //Thông tin voucher
        private string voucherDetail;
        public string VoucherDetail
        {
            get => voucherDetail;
            set
            {
                voucherDetail = value;
                ValidateVoucherDetail();
            }
        }
        private string voucherDetailError;
        public string VoucherDetailError
        {
            get => voucherDetailError;
            set
            {
                voucherDetailError = value;
                OnPropertyChanged(nameof(VoucherDetailError));
            }
        }
        public string Type { get; set; }
        //Ngày bắt đầu ra voucher
        private DateTime? startDate;
        public DateTime? StartDate
        {
            get => startDate;
            set
            {
                startDate = value;
                ValidateStartDate();
            }
        }

        private string startDateError;
        public string StartDateError
        {
            get => startDateError;
            set
            {
                startDateError = value;
                OnPropertyChanged(nameof(StartDateError));
            }
        }
        //Ngày voucher hết hạn
        private DateTime? finDate;
        public DateTime? FinDate
        {
            get => finDate;
            set
            {
                finDate = value;
                ValidateFinDate();
            }
        }

        private string finDateError;
        public string FinDateError
        {
            get => finDateError;
            set
            {
                finDateError = value;
                OnPropertyChanged(nameof(FinDateError));
            }
        }
        public EditVoucherViewModel(EditVoucher wd)
        {
            this.wd = wd;
            quitCommand=new ViewModelCommand(quit);
            acceptCommand = new ViewModelCommand(accpet,canAcceptEdit);
        }

        public void loadEditCurrent()
        {
            Name = voucher.Name;
            VoucherDetail = voucher.VoucherDetail;
            Type = voucher.Type;

            string[]startDate= voucher.StartDate.Split('/');
            StartDate = new DateTime(int.Parse(startDate[2]), int.Parse(startDate[1]), int.Parse(startDate[0]));

            string[] finDate = voucher.FinDate.Split('/');
            FinDate = new DateTime(int.Parse(finDate[2]), int.Parse(finDate[1]), int.Parse(finDate[0]));
        }

        private void quit(object obj)
        {
            wd.Close();
        }

        private void accpet(object obj)
        {
            VoucherDA voucherDA = new VoucherDA();

            //sau này nhớ xử lý lỗi người dùng
            voucherDA.editVoucher(new VoucherDTO(voucher.Id, Name, VoucherDetail, Type, StartDate.Value.ToString("yyyy-MM-dd"), FinDate.Value.ToString("yyyy-MM-dd")));
            YesMessageBox mb = new YesMessageBox("Thông báo", "Thêm voucher thành công");
            mb.ShowDialog();
            wd.Close();
        }
        private bool[] _canAccept = new bool[4];//phục vụ việc có cho nhấn button accept k
        private bool canAcceptEdit(object obj)
        {
            return _canAccept[0] && _canAccept[1] && _canAccept[2] && _canAccept[3];
        }

        //Các hàm Validate
        private void ValidateName()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                NameError = "Tên voucher không được để trống!";
                _canAccept[0] = false;
            }
            else
            {
                NameError = "";
                _canAccept[0] = true;
            }
        }
        private void ValidateVoucherDetail()
        {
            if (string.IsNullOrWhiteSpace(VoucherDetail))
            {
                VoucherDetailError = "Thông tin voucher không được để trống!";
                _canAccept[1] = false;
            }
            else
            {
                VoucherDetailError = "";
                _canAccept[1] = true;
            }
        }
        private void ValidateStartDate()
        {
            if (StartDate > FinDate || StartDate < DateTime.Now)
            {
                StartDateError = "Ngày ra voucher không hợp lệ";
                _canAccept[2] = false;

            }
            else
            {
                StartDateError = "";
                _canAccept[2] = true;
            }
        }
        private void ValidateFinDate()
        {
            if (FinDate < StartDate)
            {
                FinDateError = "Ngày hết hạn voucher phải lớn hơn ngày ra";
                _canAccept[3] = false;
            }
            else
            {
                FinDateError = "";
                _canAccept[3] = true;
            }
        }
    }
}
