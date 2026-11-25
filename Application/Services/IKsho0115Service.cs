using Application.DTOs;

namespace Application.Services;

public interface IKsho0115Service
{
    void CheckDatabaseConnection();
    Ksho0115ExportResult ExecuteExport(Ksho0115ExportRequest request);
}

