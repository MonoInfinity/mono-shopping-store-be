using Microsoft.AspNetCore.Mvc;
using store.Src.ProductModule.DTO;

namespace store.Src.ProductModule.Interface
{
    public interface IImportInfoController
    {
        public ObjectResult addImportInfo(AddImportInfoDto body);
        public ObjectResult updateImportInfo(UpdateImportInfoDto body);
        public ObjectResult deleteImportInfo(DeleteImportInfoDto body);
    }
}