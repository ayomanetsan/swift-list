using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class LabelRepository : Repository<Label>, ILabelRepository
    {
        public LabelRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
