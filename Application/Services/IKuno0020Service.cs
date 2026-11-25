using Application.DTOs;

namespace Application.Services;

public interface IKuno0020Service
{
    void CheckDatabaseConnection();
    CategoryLookupResult CheckCategory(int daiCd, int tyuCd);
    bool TryGetBumonName(int bmCd, out string bumonName);
    Kuno0020ActionResult SaveCategory(CategorySaveRequest request, bool isUpdate);
    Kuno0020ActionResult DeleteCategory(int daiCd, int tyuCd);
}

