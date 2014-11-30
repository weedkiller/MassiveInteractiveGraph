using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace MassiveInteractiveGraph.Dal
{
    public interface IMassiveInteractiveGraphDbEntities : IDisposable
    {
        DbSet<Link> Links { get; set; }
        DbSet<Node> Nodes { get; set; }
        int SaveChanges();
        DbEntityEntry Entry(object entity);
    }

    public partial class MassiveInteractiveGraphDbEntities : IMassiveInteractiveGraphDbEntities
    {
    }
}
