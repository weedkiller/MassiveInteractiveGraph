using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using log4net;
using MassiveInteractiveGraph.Dal;

namespace MassiveInteractiveGraph.Services.Dal
{
    public interface INodeDal
    {
        IQueryable<Node> GetAll();

        Node Find(int id);

        void AddOrEdit(Node node);
        void Delete(int id);
        void Save();
    }

    public class NodeDal : INodeDal
    {
        private readonly IMassiveInteractiveGraphDbEntities _db;
        private readonly ILog _log;

        public NodeDal(IMassiveInteractiveGraphDbEntities db, ILog log)
        {
            _db = db;
            _log = log;
        }

        public IQueryable<Node> GetAll()
        {
            return _db.Nodes;
        }


        public Node Find(int id)
        {
            return _db.Nodes.Find(id);
        }

        public void AddOrEdit(Node node)
        {
            var existing = _db.Nodes.Find(node.Id);
            if (existing != null)
            {
                Node.CopyAllValues(node, existing);

                _db.Entry(existing).State = EntityState.Modified;
            }
            else
            {
                _db.Nodes.Add(node);
            }
        }

        public void Delete(int id)
        {
            var node = _db.Nodes.Find(id);

            if (node == null) throw new ArgumentException("Node can't be found", "id");

            if (node.AdjacentNodes != null && node.AdjacentNodes.Any())
            {
                node.AdjacentNodes.Clear();
            }

            _db.Nodes.Remove(node);
        }

        public void Save()
        {
            try
            {
                _db.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var aggregatedExceptionMessage = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        aggregatedExceptionMessage += string.Format("Property: {0} Error: {1}",
                            validationError.PropertyName,
                            validationError.ErrorMessage);
                        throw new Exception(aggregatedExceptionMessage);
                    }
                }
            }
        }
    }
}