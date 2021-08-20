using Microsoft.AspNetCore.Mvc;
using store.ProductModule.DTO;

namespace store.ProductModule.Interface
{
    public interface IProductController
    {
        public ObjectResult AddCategory(AddCategoryDto body);
    }
}