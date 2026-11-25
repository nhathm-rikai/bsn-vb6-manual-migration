namespace Domain.Interfaces;

public interface IKmLockRepository
{
    void TestConnection();
    long? GetKeiYm(int krecKbn);
}

