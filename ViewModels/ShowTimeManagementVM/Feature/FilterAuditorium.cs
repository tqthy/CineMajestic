using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CineMajestic.ViewModels.ShowTimeManagementVM
{
    public partial class ShowTimeManagementViewModel
    {
        public ICommand AuditoriumCommand { get; set; }//tất cả các phòng
        public ICommand AuditoriumCommand1 {  get; set; }
        public ICommand AuditoriumCommand2 {  get; set; }
        public ICommand AuditoriumCommand3 {  get; set; }
        public ICommand AuditoriumCommand4 {  get; set; }
        public ICommand AuditoriumCommand5 {  get; set; }
        public ICommand AuditoriumCommand6 {  get; set; }
        public ICommand AuditoriumCommand7 {  get; set; }



        private void Auditorium()
        {
            AuditoriumCommand = new ViewModelCommand(auditorium);
            AuditoriumCommand1 = new ViewModelCommand(auditorium1);
            AuditoriumCommand2 = new ViewModelCommand(auditorium2);
            AuditoriumCommand3 = new ViewModelCommand(auditorium3);
            AuditoriumCommand4 = new ViewModelCommand(auditorium4);
            AuditoriumCommand5 = new ViewModelCommand(auditorium5);
            AuditoriumCommand6 = new ViewModelCommand(auditorium6);
            AuditoriumCommand7 = new ViewModelCommand(auditorium7);
        }


        private void auditorium(object obj)//tất cả các phòng
        {
            loadData();
        }

        private void auditorium1(object obj)
        {
            loadData("Phòng 1");
        }
        private void auditorium2(object obj)
        {
            loadData("Phòng 2");
        }
        private void auditorium3(object obj)
        {
            loadData("Phòng 3");
        }
        private void auditorium4(object obj)
        {
            loadData("Phòng 4");
        }
        private void auditorium5(object obj)
        {
            loadData("Phòng 5");
        }
        private void auditorium6(object obj)
        {
            loadData("Phòng 6");
        }
        private void auditorium7(object obj)
        {
            loadData("Phòng 7");
        }
    }
}
