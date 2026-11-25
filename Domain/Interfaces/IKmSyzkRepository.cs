using Domain.Entities;

namespace Domain.Interfaces;

public interface IKmSyzkRepository
{
    void TestConnection();
    KmSyzk? GetByCode(int kszCd);
    void Insert(KmSyzk entity);
    void Update(KmSyzk entity);
    void SoftDelete(int kszCd, DateTime deletedDate);
}
