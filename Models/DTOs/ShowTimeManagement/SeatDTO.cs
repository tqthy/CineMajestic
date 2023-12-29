using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DTOs.ShowTimeManagement
{
    public class SeatDTO:INotifyPropertyChanged
    {
        public int Id {  get; set; }
        public string Location {  get; set; }

        private bool condition;
        public bool Condition
        {
            get => condition;
            set
            {
                condition = value;
                OnPropertyChanged(nameof(Condition));
            }
        }
        public int Auditorium_Id {  get; set; }

        //constructor phục vụ lấy danh sách ghế

        public SeatDTO(int id, string location, bool condition, int auditorium_Id)
        {
            Id = id;
            Location = location;
            Condition = condition;
            Auditorium_Id = auditorium_Id;
        }



        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
