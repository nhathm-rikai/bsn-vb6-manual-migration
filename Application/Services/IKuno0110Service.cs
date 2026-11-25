using Domain.Entities;

namespace Application.Services;

public interface IKuno0110Service
{
    void CheckDatabaseConnection();
    KmMeis? GetByKey(int recKb, int meisCd);
    void Insert(KmMeis entity);
    void Update(KmMeis entity);
    void SoftDelete(int recKb, int meisCd, DateTime deletedDate);
}

