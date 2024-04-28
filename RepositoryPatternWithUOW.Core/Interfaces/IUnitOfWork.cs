﻿using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Author> Authors { get; }
        IGenericRepository<Book> Books { get; }
        Task<int> Commit();
    }
}