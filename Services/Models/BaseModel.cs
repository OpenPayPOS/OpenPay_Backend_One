namespace OpenPay.Services.Models;
public abstract class BaseModel<T, TDTO, TDataDTO> where T : BaseModel<T, TDTO, TDataDTO>
{
    public abstract TDataDTO ToDataDTO();
    public abstract Task<TDTO> ToDTOAsync();
}