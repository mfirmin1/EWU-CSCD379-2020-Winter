using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using System.Threading.Tasks;
using SecretSanta.Data;

namespace SecretSanta.Business
{
    public abstract class EntityService<TEntity> : IEntityService<TEntity> where TEntity : EntityBase
    {
        protected ApplicationDbContext ApplicationDbContext { get; }
        protected IMapper Mapper { get; }

        public EntityService(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            ApplicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> DeleteAsync(int id)
        {
            bool check = false;
            TEntity entity = await FetchByIdAsync(id);
            var recieved = ApplicationDbContext.Set<TEntity>().Remove(entity);
            if (recieved.State == EntityState.Deleted) check = true;
            await ApplicationDbContext.SaveChangesAsync();
            return check;
        }

        public async Task<List<TEntity>> FetchAllAsync() => await ApplicationDbContext.Set<TEntity>().ToListAsync();

        virtual public async Task<TEntity> FetchByIdAsync(int id) => await ApplicationDbContext.FindAsync<TEntity>(id);
        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            await InsertAsync(new[] { entity });
            return entity;
        }

        public async Task<TEntity[]> InsertAsync(params TEntity[] entity)
        {
            foreach (TEntity ent in entity)
            {
                ApplicationDbContext.Set<TEntity>().Add(ent);
                await ApplicationDbContext.SaveChangesAsync();
            }
            return entity;
        }

        public async Task<TEntity> UpdateAsync(int id, TEntity entity)
        {
            TEntity result = await ApplicationDbContext.Set<TEntity>().SingleAsync(item => item.Id == id);
            Mapper.Map(entity, result);
            await ApplicationDbContext.SaveChangesAsync();
            return result;
        }

    }
}
