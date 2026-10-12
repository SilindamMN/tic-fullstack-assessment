namespace TlcBackend.Interface
{
 /// <summary>
 /// Defines a generic service interface for retrieving data.
 /// </summary>
    public interface IGenericService<TEntity, TDto>
       where TEntity : class
       where TDto : class
    {
        Task<IEnumerable<TDto>> GetAllAsync();
    }
}