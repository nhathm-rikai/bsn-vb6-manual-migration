using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class Kuno0110Service : IKuno0110Service
{
    private readonly IKmMeisRepository _meisRepo;

    public Kuno0110Service(IKmMeisRepository meisRepo)
    {
        _meisRepo = meisRepo;
    }

    public void CheckDatabaseConnection()
    {
        _meisRepo.TestConnection();
    }

    public KmMeis? GetByKey(int recKb, int meisCd)
    {
        return _meisRepo.GetByKey(recKb, meisCd);
    }

    public void Insert(KmMeis entity)
    {
        _meisRepo.Insert(entity);
    }

    public void Update(KmMeis entity)
    {
        _meisRepo.Update(entity);
    }

    public void SoftDelete(int recKb, int meisCd, DateTime deletedDate)
    {
        _meisRepo.SoftDelete(recKb, meisCd, deletedDate);
    }
}

