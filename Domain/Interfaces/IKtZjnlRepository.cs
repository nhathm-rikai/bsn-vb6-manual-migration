using Domain.Entities;

namespace Domain.Interfaces;

public interface IKtZjnlRepository
{
    KtZjnl? GetByDenNo(long kdenno);
    long InsertWithSequence(KtZjnl entity);
    void Update(KtZjnl entity);
    void Delete(long kdenno);
}

