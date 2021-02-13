using InstaGama.Domain.Entities;
using InstaGama.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InstaGama.Repositories
{
    public class RelationshipRepository : IRelationshipRepository
    {
        public Task<int> InsertAsync(Relationship relationship)
        {
            throw new NotImplementedException();
        }
    }
}
