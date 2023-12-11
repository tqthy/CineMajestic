using CineMajestic.Models.DataAccessLayer;
using CineMajestic.Models.DTOs.ProductManagement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CineMajestic.ViewModels.ProductManagement
{
    public partial class ProductManagementViewModel
    {
        public ICommand deleteProductCommand { get; set; }

        void delete()
        {
            deleteProductCommand = new ViewModelCommand(deleteProduct);
            deleteImages();
        }
        private void deleteProduct(object obj)
        {
            ProductDA productDA = new ProductDA();
            if (obj is ProductDTO product)
            {
                productDA.deleteProduct(product);
                loadData();

                //lưu path ảnh để xóa

                using(FileStream fStream =new FileStream("deleteAnh.txt", FileMode.Append, FileAccess.Write))
                {
                    using(StreamWriter sw = new StreamWriter(fStream))
                    {
                        sw.WriteLine(product.ImageSource);
                    }
                }
            }
        }


        //phục vụ việc xóa ảnh của sản phẩm,khi sản phẩm này k còn
        private void deleteImages()
        {
            using (FileStream fStream = new FileStream("deleteAnh.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fStream))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        try
                        {
                            File.Delete(line);
                        }
                        catch { }
                    }
                }
            }


            try
            {
                File.Delete("deleteAnh.txt");
            }
            catch{ }
        }
    }
}
