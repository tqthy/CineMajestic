using CineMajestic.Models.DTOs;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CineMajestic.Models.DataAccessLayer
{
    public class ShowTimeDA : DataAccess
    {
        public List<ShowTimeDTO> GetAllShowTimeByDate(DateTime date)
        {
            
            List<ShowTimeDTO> result = new List<ShowTimeDTO>();
            try
            {
                using (var connection = GetConnection())
                using (var command = new SqlCommand())
                {
                    connection.Open();
                    command.Connection = connection;
                    command.CommandText = "SELECT * FROM [SCREENING] WHERE YEAR(StartTime)=@year AND MONTH(StartTime)=@month AND DAY(StartTime)=@day";
                    command.Parameters.Add("@year", SqlDbType.Int).Value = date.Year;
                    command.Parameters.Add("@month", SqlDbType.Int).Value = date.Month;
                    command.Parameters.Add("@day", SqlDbType.Int).Value = date.Day;
                    using (var reader = command.ExecuteReader())
                    {
                        List<(string, string, string, DateTime)> lines = new();
                        while (reader.Read())
                        {
                            (string, string, string, DateTime) t = (reader[0].ToString(), reader[1].ToString(), reader[2].ToString() , Convert.ToDateTime(reader[3]));
                            lines.Add(t);
                        }
                        Dictionary < (string,string,string), List<DateTime> > myHash = new();
                        foreach (var line in lines)
                        {
                            var key = (line.Item1, line.Item2, line.Item3);
                            if (!myHash.ContainsKey((key)))
                            {
                                myHash.Add(key, new List<DateTime>());
                            }
                            myHash[key].Add(line.Item4);
                        }
                        foreach(KeyValuePair<(string, string, string), List<DateTime> > pair in myHash)
                        {
                            MovieDA movieDA = new MovieDA();
                            MovieDTO mv = movieDA.GetMovieById(pair.Key.Item2);
                            ShowTimeDTO showTime = new()
                            {
                                Id = pair.Key.Item1,
                                Movie_Id = pair.Key.Item2,
                                Title = mv.Title,
                                Length = mv.Length,
                                Auditorium_Id = pair.Key.Item3,
                                StartTime = pair.Value
                            };
                            result.Add(showTime);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
