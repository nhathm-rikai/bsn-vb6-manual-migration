using Domain.Entities;

namespace Domain.Interfaces;

public interface IKmTantRepository
{
    void TestConnection();
    KmTant? GetByCode(int ktantCd);
    IReadOnlyList<KmTant> GetAllActive();
    void Insert(KmTant entity);
    void Update(KmTant entity);
    void SoftDelete(int ktantCd, DateTime deletedDate);
}

