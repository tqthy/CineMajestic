using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;
using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs;
using CineMajestic.Views;

namespace CineMajestic.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _userName;
        //private SecureString _password;
        private string _errorMessage;
        private bool _isViewVisible = true;
        private UserDA userDA;


        public string Username
        {
            get => _userName; set { _userName = value; OnPropertyChanged(nameof(Username)); }
        }
        //public SecureString Password
        //{
        //    private get => _password; set { _password = value; OnPropertyChanged(nameof(Password)); }
        //}


        public string ErrorMessage
        {
            get => _errorMessage; set { _errorMessage = value; OnPropertyChanged(nameof(ErrorMessage)); }
        }
        public bool IsViewVisible
        {
            get => _isViewVisible; set { _isViewVisible = value; OnPropertyChanged(nameof(IsViewVisible)); }
        }

        // -> Commands
        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }

        // Constructors

        //public LoginViewModel()
        //{
        //    userDA = new UserDA();
        //    LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
        //    RecoverPasswordCommand = new ViewModelCommand(p => ExecuteRecoverPasswordCommand("", ""));
        //}

        private LoginView wd;
        public LoginViewModel(LoginView wd)
        {
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            RecoverPasswordCommand = new ViewModelCommand(p => ExecuteRecoverPasswordCommand("", ""));
            this.wd = wd;
        }
        private void ExecuteRecoverPasswordCommand(string username, string email)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            return ValidAccount();
        }

        //private void ExecuteLoginCommand(object obj)
        //{
        //    var isValidUser = userDA.AuthenticateUser(new NetworkCredential(Username, Password));

        //    if (isValidUser)
        //    {
        //        Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(Username), null);
        //        IsViewVisible = false;
        //    }
        //    else
        //    {
        //        ErrorMessage = "* Invalid username or password";
        //    }
        //}


        private void ExecuteLoginCommand(object obj)
        {
            UserDA userDA = new UserDA();
            List<UserDTO> getAccounts = userDA.getAccounts();
            bool check = false;
            foreach (UserDTO user in getAccounts)
            {
                if (user.Username == Username && user.Password == PTChung.EncryptMD5(wd.txtPassword.Password))
                {
                    check = true;
                    MainView mainView = new MainView(user.Staff_Id);
                    wd.Hide();
                    mainView.ShowDialog();
                    //dùng app.shutdow nên k phải thoát login khi thoát mainview
                    break;
                }
            }

            if (!check)
            {
                ErrorMessage = "* Tên đăng nhập hoặc mật khẩu không đúng";
            }

        }


        // Utility methods
        //private bool ValidAccount()
        //{
        //    if (string.IsNullOrWhiteSpace(Username) || Password == null) return false;
        //    return true;
        //}
        private bool ValidAccount()
        {
            if (string.IsNullOrWhiteSpace(Username) || wd.txtPassword.Password == "") return false;
            return true;
        }
    }
}
