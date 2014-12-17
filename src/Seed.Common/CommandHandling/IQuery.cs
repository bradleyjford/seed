using System;
using System.Threading.Tasks;

namespace Seed.Common.CommandHandling
{
    public interface IQuery<TResult>
    {
        Task<TResult> Execute();
    }
}
