namespace OpenPay.Data.Mappers;

public interface IMapper<TModel, TDTO>
{
    public TDTO MapToDataDTO(TModel model);
}
