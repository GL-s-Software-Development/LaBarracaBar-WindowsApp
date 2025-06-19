using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Diagnostics;

namespace LaBarracaBar.Models
{
    public class ProductModel
    {
        private int id;
        private string name;
        private double price;
        private int category;
        private decimal quantity;

        [DisplayName("ID")]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [DisplayName("PRODUCTO")]
        [Required(ErrorMessage = "Ingrese el nombre del producto")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre del producto debe contener entre 3-50 carácteres")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [DisplayName("PRECIO")]
        [Required(ErrorMessage = "Ingrese el precio del producto")]
        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        [DisplayName("CATEGORÍA")]
        [Required(ErrorMessage = "Ingrese la categoría del producto")]
        public int Category
        {
            get { return category; }
            set { category = value; }
        }

        public decimal Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public string CategoryName { get; set; } // Se usa para mostrar el nombre legible de la categoría
        public double Subtotal => Convert.ToDouble(Quantity) * Price;
    }
}
