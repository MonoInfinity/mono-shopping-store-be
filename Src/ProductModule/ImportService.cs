using store.Src.ProductModule.Interface;
using store.Src.ProductModule.Entity;


namespace store.Src.ProductModule
{
    public class ImportService : IImportService
    {

        private readonly IImportInfoRepository importInfoRepository;

        public ImportService(IImportInfoRepository importInfoRepository)
        {
            this.importInfoRepository = importInfoRepository;
        }

        public bool saveImportInfo(ImportInfo importInfo)
        {
            bool res = this.importInfoRepository.saveImportInfo(importInfo);
            return res;
        }

        public ImportInfo getImportInfoByImportInfoId(string importInfoId)
        {
            ImportInfo importInfo = this.importInfoRepository.getImportInfoByImportInfoId(importInfoId);
            return importInfo;
        }

        public bool updateImportInfo(ImportInfo importInfo)
        {
            bool res = this.importInfoRepository.updateImportInfo(importInfo);
            return res;
        }

        public bool deleteImportInfo(string importInfoId)
        {
            bool res = this.importInfoRepository.deleteImportInfo(importInfoId);
            return res;
        }
    }
}