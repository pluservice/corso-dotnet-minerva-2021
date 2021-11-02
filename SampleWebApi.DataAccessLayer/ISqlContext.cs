using System;
using System.Data;

namespace SampleWebApi.DataAccessLayer
{
    public interface ISqlContext : IDisposable
    {
        IDbConnection Connection { get; }
    }
}