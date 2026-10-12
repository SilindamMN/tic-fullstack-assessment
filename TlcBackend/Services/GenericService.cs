using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TlcBackend.Data;
using TlcBackend.Interface;

namespace TlcBackend.Services
{/// <summary>
 /// Represents a generic service for retrieving data from the database and mapping it to DTOs.
 /// </summary>
 /// <typeparam name="TEntity"></typeparam>
 /// <typeparam name="TDto"></typeparam>
    public class GenericService<TEntity, TDto> : IGenericService<TEntity, TDto>
      where TEntity : class
      where TDto : class
    {
        private readonly AppDbContext dataContext;
        private readonly IMapper mapper;

        public GenericService(AppDbContext dataContext, IMapper mapper)
        {
            this.dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Gets all entities of type TEntity from the database and maps them to a list of TDto objects.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TDto>> GetAllAsync()
        {
            try
            {
                var entities = dataContext.Set<TEntity>()
                    .Where(e => EF.Property<bool>(e, "IsActive") == true)
                    .ToList();

                var dtos = mapper.Map<List<TDto>>(entities);
                return dtos;
            }
            catch (Exception ex)
            {
                return new List<TDto>();
            }
        }
    }
}