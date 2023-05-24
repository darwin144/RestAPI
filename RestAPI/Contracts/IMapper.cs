namespace RestAPI.Contracts
{
    public interface IMapper<TModel, TViewModel>
    {
        TViewModel Map(TModel tModel);
        TModel Map(TViewModel viewModel);
    }
}
