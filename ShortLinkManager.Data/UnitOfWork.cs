using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortLinkManager.Data
{
    public class UnitOfWork : IDisposable
    {
        private DatabaseContext context = new DatabaseContext();
        private GenericRepository<Models.ShortLink> shortLinkRepository;
        private GenericRepository<Models.Guest> guestRepository;
        private GenericRepository<Models.Visits> visitsRepository;

        public GenericRepository<Models.ShortLink> ShortLinkRepository
        {
            get
            {

                if (this.shortLinkRepository == null)
                {
                    this.shortLinkRepository = new GenericRepository<Models.ShortLink>(context);
                }
                return shortLinkRepository;
            }
        }

        public GenericRepository<Models.Guest> GuestRepository
        {
            get
            {

                if (this.guestRepository == null)
                {
                    this.guestRepository = new GenericRepository<Models.Guest>(context);
                }
                return guestRepository;
            }
        }

        public GenericRepository<Models.Visits> VisitsRepository
        {
            get
            {

                if (this.visitsRepository == null)
                {
                    this.visitsRepository = new GenericRepository<Models.Visits>(context);
                }
                return visitsRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
