using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecretSanta.Business;
using SecretSanta.Business.Services;
using System.Threading.Tasks;
using SecretSanta.Data;


namespace SecretSanta.Api.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public abstract class EntityController<TEntity> : ControllerBase where TEntity : class
    {
        private IEntityService<TEntity> EntityService { get;  } 

        public EntityController(IEntityService<TEntity> entityService)
        {
            EntityService = entityService ?? throw new System.ArgumentNullException(nameof(entityService));
        }
        [HttpGet]
        public async Task<IEnumerable<TEntity>> Get()
        {
            List<TEntity> entityServices = await EntityService.FetchAllAsync();
            return entityServices;
        }
        [HttpGet("Id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TEntity>> Get(int id)
        {
            if (await EntityService.FetchByIdAsync(id) is TEntity entity)
            {
                return Ok(entity);
            }
            return NotFound();
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TEntity>> Post(TEntity entity)
        {
            return Ok(await EntityService.InsertAsync(entity));
        }
        [HttpPut("Id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TEntity>> Put(int id, TEntity entity)
        {
            if (await EntityService.UpdateAsync(id, entity) is TEntity value)
            {
                return entity;
            }
            return NotFound();
        }
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete(int id)
        {
            if (await EntityService.DeleteAsync(id))
            {
                return Ok(true);
            }
            return NotFound();
        }
    }
}
