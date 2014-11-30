using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using log4net;
using MassiveInteractiveGraph.Dal;

namespace MassiveInteractiveGraph.Services.Dal
{
    public interface ILinkDal
    {
        IQueryable<Link> GetAll();

        Link Find(int nodeId1, int nodeId2);

        void Add(Link link);
        void Delete(int nodeId1, int nodeId2);
        void Save();
    }

    public class LinkDal: ILinkDal
    {
        private readonly IMassiveInteractiveGraphDbEntities _db;
        private readonly ILog _log;

        public LinkDal(IMassiveInteractiveGraphDbEntities db, ILog log)
        {
            _db = db;
            _log = log;
        }

        public IQueryable<Link> GetAll()
        {
            return _db.Links;
        }

        public Link Find(int nodeId1, int nodeId2)
        {
            return _db.Links.SingleOrDefault(l => l.Node1Id == nodeId1 && l.Node2Id == nodeId2);
        }

        public void Add(Link link)
        {
            var existing = Find(link.Node1Id, link.Node2Id);
            if (existing != null)
            {
                throw new Exception("Link already exists");
            }
            else
            {
                _db.Links.Add(link);
            }
        }

        public void Delete(int nodeId1, int nodeId2)
        {
            var link = Find(nodeId1, nodeId2);

            if (link == null) throw new ArgumentException("Link can't be found", "id");

            _db.Links.Remove(link);
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