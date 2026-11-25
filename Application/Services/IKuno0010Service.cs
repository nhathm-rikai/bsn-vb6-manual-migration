using Application.DTOs;

namespace Application.Services;

public interface IKuno0010Service
{
    void CheckDatabaseConnection();
    bool TryCheckCategory(int daiCd, int tyuCd, out string dtName, out int shCdr);
    bool TryCheckRepresentativeProduct(long dhShnCd, out string shortName);
    bool TryGetMakerName(int mekaCd, out string makerName);
    bool TryGetUnitName(int tniCd, out string unitName);
    IReadOnlyList<MakerLookupItem> GetMakerLookupList(int daiCd);
    IReadOnlyList<UnitLookupItem> GetUnitLookupList();
    ProductDetailResult GetProductByCode(long kshncd);
    Kuno0010ActionResult RegisterProduct(ProductSaveRequest request);
    Kuno0010ActionResult UpdateProduct(ProductSaveRequest request);
    Kuno0010ActionResult DeleteProduct(long kshncd, string formattedScd);
}

