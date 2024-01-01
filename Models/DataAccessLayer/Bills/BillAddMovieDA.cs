using CineMajestic.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.Models.DataAccessLayer
{
    public class BillAddMovieDA : DataAccess
    {
        //add 1 bill
        public void addBill(BillAddMovieDTO billAddMovieDTO)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string insert =
                        "insert into Bill_AddMovie(Movie_Id,BillDate,Total)\n"
                        +
                        "values("
                        +
                        billAddMovieDTO.Movie_Id + ","
                        +
                        "'" + billAddMovieDTO.BillDate + "',"
                        +
                        billAddMovieDTO.Total + ")";
                    using (SqlCommand command = new SqlCommand(insert, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }



        //bổ trợ delete 1 movie
        public void updateMovie_IdNull(int Movie_Id)
        {
            try
            {
                using (SqlConnection connection = GetConnection())
                {
                    connection.Open();
                    string updateNull =
                        "update Bill_AddMovie\n"
                        +
                        "set Movie_Id=null\n"
                        +
                        "where Movie_Id=" + Movie_Id;
                    using (SqlCommand command = new SqlCommand(updateNull, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch { }
        }


    }
}
