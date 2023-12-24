using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CineMajestic.ViewModels
{
    public class PTChung
    {
        //kiểm tra 1 chuỗi có chứa kí tự unicode hay k
        public static bool ContainsUnicodeCharacter(string input)
        {
            foreach (char c in input)
            {
                if (c > 127)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
