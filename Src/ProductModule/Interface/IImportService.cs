using store.Src.ProductModule.Entity;

namespace store.Src.ProductModule.Interface
{
    public interface IImportService
    {
        public bool saveImportInfo(ImportInfo importInfo);
        public ImportInfo getImportInfoByImportInfoId(string importInfoId);

        public bool updateImportInfo(ImportInfo importInfo);
        public bool deleteImportInfo(string importInfoId);
    }
}