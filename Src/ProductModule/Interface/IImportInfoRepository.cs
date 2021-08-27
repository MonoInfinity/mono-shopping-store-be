using store.Src.ProductModule.Entity;

namespace store.Src.ProductModule.Interface
{
    public interface IImportInfoRepository
    {
        public bool saveImportInfo(ImportInfo importInfo);
        public bool deleteImportInfo(string importInfoId);
        public bool updateImportInfo(ImportInfo importInfo);
        public ImportInfo getImportInfoByImportInfoId(string importInfoId);
    }
}