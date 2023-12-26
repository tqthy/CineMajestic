using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DTOs
{
    public class BillAddMovieDTO
    {
        public int Id { get; set; }
        public int Movie_Id {  get; set; }
        public string BillDate {  get; set; }
        public int Total {  get; set; }


        public BillAddMovieDTO(int id, int movie_Id, string billDate, int total)
        {
            Id = id;
            Movie_Id = movie_Id;
            BillDate = billDate;
            Total = total;
        }

        //hỗ trợ add
        public BillAddMovieDTO(int movie_Id, string billDate, int total)
        {
            Movie_Id = movie_Id;
            BillDate = billDate;
            Total = total;
        }
    }
}
