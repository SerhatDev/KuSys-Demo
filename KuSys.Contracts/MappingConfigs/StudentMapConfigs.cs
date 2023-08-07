using KuSys.Contracts.ResponseModels;
using KuSys.Core.Types;
using KuSys.Entities;
using Mapster;

namespace KuSys.Contracts.MappingConfigs;

public class StudentMapConfigs : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PagedResponse<Student>, StudentListResponse>()
            .Map(dest => dest.Students, source => source.Data);
    }
}