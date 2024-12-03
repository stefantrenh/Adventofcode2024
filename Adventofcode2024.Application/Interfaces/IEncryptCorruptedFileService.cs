using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adventofcode2024.Application.Interfaces
{
    public interface IEncryptCorruptedFileService
    {
        Task<int> CalculateEncryptedFile();
    }
}
