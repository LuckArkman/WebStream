using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Repositories
{
    public interface IUnityOfWork
    {
        public Task Commit(CancellationToken cancellationToken);
        public Task RollCack(CancellationToken _cancellationToken);
    }
}
