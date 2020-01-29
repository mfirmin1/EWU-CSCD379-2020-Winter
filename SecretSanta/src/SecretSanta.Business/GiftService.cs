﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SecretSanta.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace SecretSanta.Business
{
    public class GiftService : EntityService<Gift>, IEntityService<Gift>
    {
        public GiftService(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {

        }

        public override async Task<Gift> FetchByIdAsync(int id) => 
            await ApplicationDbContext.Set<Gift>().
            Include(nameof(Gift.User)).SingleAsync(item => item.Id == id);
    }
}
