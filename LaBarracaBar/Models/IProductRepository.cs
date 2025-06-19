using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaBarracaBar.Models
{
    public interface IProductRepository
    {
        //Products
        void Add(ProductModel product);
        void Edit(ProductModel product);
        void Remove(ProductModel product);
        List<ProductModel> GetByProduct(string product);
        IEnumerable<ProductModel> GetAll();

    }
}
